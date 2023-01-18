using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopulateLand : MonoBehaviour
{

    [SerializeField] private List<GameObject> items;
    [SerializeField] private float number;
    private RaycastHit hit;

    private float rangeX;
    private float rangeZ;

    void Start()
    {
        
        for (int i = 0; i < number; i++)
        {

            rangeX = Random.Range(0, FindObjectOfType<GenerateWorld>().getWidthX());
            rangeZ = Random.Range(0, FindObjectOfType<GenerateWorld>().getLengthZ());

            if (Physics.Raycast(new Vector3(rangeX , 100, rangeZ), Vector3.down, out hit)){

                if ((Vector3.Angle(Vector3.up, hit.normal) < 25f) && !hit.transform.tag.Equals("Resource") && !hit.transform.tag.Equals("Player") && !hit.transform.tag.Equals("Water") && hit.point.y > 11) { 
                    Instantiate(items[Random.Range(0, items.Count)], hit.point, Quaternion.FromToRotation(Vector3.up, hit.normal));
                }
            }
        }
    }
}