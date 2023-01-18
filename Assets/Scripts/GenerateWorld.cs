
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(MeshFilter))]
public class GenerateWorld : MonoBehaviour
{

    Vector3[] verticies;
    int[] triangles;
    Mesh mesh;

    [SerializeField]
    private int xSize;
    [SerializeField]
    private int zSize;
    [SerializeField]
    private float roughness = 0.3f;
    [SerializeField]
    private float spacedout = 2f;

    private MeshCollider mcollider;

    [SerializeField]
    private float noiseScale = 5;
    [SerializeField]
    private float perlinNoiseY;
    [SerializeField]
    private float offsetX;
    [SerializeField]
    private float offsetY;
    [SerializeField]
    private float fallOffRange;    
    [SerializeField]
    private float fallOffPower;

    [SerializeField]
    private GameObject manager;

    void Start()
    { 

        offsetX = Random.Range(0, 100);
        offsetY = Random.Range(0, 100);

        mesh = new Mesh();
        GetComponent<MeshFilter>().mesh = mesh;

        mcollider = GetComponent<MeshCollider>();

        CreateShape();
        updateMesh();

        mcollider.convex = false;

        manager.SetActive(true);

    }

    private void Update()
    {
        updateMesh();
    }

    public void CreateShape()
    {

        verticies = new Vector3[(xSize + 1) * (zSize + 1)];

        for (int index = 0, i = 0; i <= xSize; i++)
        {
            for (int j = 0; j <= zSize; j++)
            {

                float n1 = (Mathf.PerlinNoise(offsetX + i * roughness * 0.3f, j * roughness * 0.3f + offsetY) * noiseScale) * 2 - 1;
                float n2 = n1 * 0.5f * 2 - 1;
                float n3 = n2 * 0.25f * 2 - 1;

                perlinNoiseY = (n1 * n2 * n3) + 20;
                
                perlinNoiseY /= fallOffValue(i,j);

                verticies[index] = new Vector3(j * spacedout + Random.Range(0, 10), perlinNoiseY, i * spacedout + Random.Range(0, 10));

                index++;
            }
        }

        triangles = new int[xSize * zSize * 6];

        int tri = 0;
        int v = 0;
        for (int a = 0; a < zSize; a++)
        {
            for (int x = 0; x < xSize; x++)
            {
                triangles[tri + 0] = v + 0;
                triangles[tri + 1] = v + xSize + 1;
                triangles[tri + 2] = v + 1;
                triangles[tri + 3] = v + 1;
                triangles[tri + 4] = v + xSize + 1;
                triangles[tri + 5] = v + xSize + 2;

                tri += 6;
                v++;
            }
            v++;
        }

    }
    void updateMesh()
    {

        mcollider.sharedMesh = mesh;

        mesh.Clear();

        mesh.vertices = verticies;
        mesh.triangles = triangles;

        mesh.RecalculateNormals();
    }

    float fallOffValue(int pointX, int pointY) {

        float centerX = (float)xSize / 2;
        float centerZ = (float)zSize / 2;

        float dist = distance(pointX, pointY, centerX, centerZ);

        float falloffval = Mathf.Pow(dist / fallOffRange, fallOffPower) + 1;

        return falloffval;

    }

    float distance(float aX, float aY, float bX, float bY) {
        return Mathf.Sqrt(Mathf.Pow(aX - bX,2) + Mathf.Pow(aY - bY, 2));
    }

    public float getWidthX()
    {
        return spacedout * xSize;
    }
    public float getLengthZ()
    {
        return spacedout * zSize;
    }
}