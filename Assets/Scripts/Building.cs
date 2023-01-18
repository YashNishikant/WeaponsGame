
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Building : MonoBehaviour
{

    private RaycastHit hit;
    private bool building;
    private bool firsttime;
    private bool buildselect;
    private int blockChoice;
    private int matChoice;
    private Vector3 translatepos;
    private Vector3 objrotation;
    List<GameObject> holoList = new List<GameObject>();
    GameObject h;
    [SerializeField]
    private float dist;
    [SerializeField]
    private List<GameObject> buildList = new List<GameObject>();
    [SerializeField]
    private List<Material> MatList = new List<Material>();
    [SerializeField]
    private List<GameObject> holographList = new List<GameObject>();
    [SerializeField]
    private Image crosshair;
    [SerializeField]
    private Image b_image;
    [SerializeField]
    private Image selectionBuild;
    [SerializeField]
    private Image arrow1;
    [SerializeField]
    private Image selectionTex;
    [SerializeField]
    private Image arrow2;
    [SerializeField]
    private ParticleSystem destroyeffect;

    void Update()
    {

        if (Input.GetKeyDown(KeyCode.B))
        {
            building = !building;
        }

        if (building)
        {
            raycasting();
            changeBuild();
            b_image.enabled = true;
            selectionBuild.transform.gameObject.SetActive(true);
            selectionTex.transform.gameObject.SetActive(true);
        }
        else
        {
            b_image.enabled = false;
            selectionBuild.transform.gameObject.SetActive(false);
            selectionTex.transform.gameObject.SetActive(false);
            if (h != null)
                Destroy(h.gameObject);
            holoList.Clear();
            destroy();
            crosshair.enabled = true;
        }
    }

    void destroy()
    {
        if (Input.GetMouseButton(0))
        {
            if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, dist))
            {
                if (hit.collider.tag == "placed")
                {
                    Destroy(hit.transform.gameObject);
                    ParticleSystem de = Instantiate(destroyeffect, hit.point, Quaternion.identity);
                    de.GetComponent<ParticleSystemRenderer>().material = hit.transform.gameObject.GetComponent<MeshRenderer>().material;
                }
            }
        }
    }

    void changeBuild()
    {

        if (Input.GetKeyDown(KeyCode.Alpha1)) 
        {
            buildselect = false;
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            buildselect = true;
        }

        if (Input.GetAxis("Mouse ScrollWheel") > 0f)
        {
            if (buildselect)
                blockChoice++;
            else
                matChoice++;
        }
        if (Input.GetAxis("Mouse ScrollWheel") < 0f)
        {
            if (buildselect)
                blockChoice--;
            else
                matChoice--;
        }

        blockChoice = clampvalues<GameObject>(blockChoice, buildList);
        matChoice = clampvalues<Material>(matChoice, MatList);


        if (buildselect)
            arrow1.transform.position = new Vector3(arrow1.transform.position.x, selectionBuild.transform.position.y + (blockChoice-1)*70, arrow1.transform.position.z);
        else
            arrow2.transform.position = new Vector3(arrow2.transform.position.x, selectionTex.transform.position.y + (matChoice - 1) * 70, arrow2.transform.position.z);
    }

    void raycasting()
    {

        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, dist))
        {
            firsttime = true;
            crosshair.enabled = true;

            h = Instantiate(holographList[blockChoice], transform.TransformDirection(Vector3.forward), Quaternion.identity);
            holoList.Add(h);
            h.transform.Rotate(objrotation);

            if (holoList.Count > 1) {
                Destroy(holoList[holoList.Count - 2].gameObject);
                holoList.RemoveAt(holoList.Count - 2);
            }

            h.SetActive(true);

            if (blockChoice == 0)
                translatepos = hit.transform.localScale;

            if (blockChoice == 1)
                translatepos = hit.transform.localScale * 2;

            if (blockChoice == 2)
                translatepos = hit.transform.localScale * 2;

            determineplacement(h, hit);
        }
        else if(firsttime)
        {
            crosshair.enabled = false;
            if(holoList.Count > 0)
            holoList[holoList.Count - 1].gameObject.SetActive(false);
        }

        if (Input.GetMouseButtonDown(0))
        {

            if (blockChoice == 2) {
                objrotation += new Vector3(0, 90, 0);
            }

        }

        if (Input.GetMouseButtonDown(1))
        {

            if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, dist))
            {

                if (blockChoice == 0)
                    translatepos = hit.transform.localScale;

                if (blockChoice == 1)
                    translatepos = hit.transform.localScale * 2;

                if (blockChoice == 2)
                    translatepos = hit.transform.localScale * 2;

                GameObject item = Instantiate(buildList[blockChoice]) as GameObject;
                item.transform.Rotate(objrotation);

                for (int i = 0; i < item.transform.childCount; i++)
                {
                    item.transform.GetChild(i).GetComponent<MeshRenderer>().material = MatList[matChoice];
                }

                determineplacement(item, hit);
            }
        }
    }

    void determineplacement(GameObject item, RaycastHit hit2)
    {

        item.transform.position = hit2.point + hit2.normal/2;

            if (Physics.Raycast(item.transform.position, Vector3.down, out hit, item.transform.localScale.y))
            {
                if (hit.collider.tag.Equals("placed")){
                    item.transform.position = hit.transform.position + new Vector3(0, translatepos.y, 0);
                    return;
                }
            }

            if (Physics.Raycast(item.transform.position, Vector3.up, out hit, item.transform.localScale.y))
            {
                if (hit.collider.tag.Equals("placed")){
                    item.transform.position = hit.transform.position + new Vector3(0, -translatepos.y, 0);
                    return;
                }
            }

        if (Physics.Raycast(item.transform.position, Vector3.left, out hit, item.transform.localScale.x))
            {
                if (hit.collider.tag.Equals("placed")){
                    item.transform.position = hit.transform.position + new Vector3(translatepos.x, 0, 0);
                    return;
                }
            }

            if (Physics.Raycast(item.transform.position, Vector3.right, out hit, item.transform.localScale.x))
            {
                if (hit.collider.tag.Equals("placed")){
                    item.transform.position = hit.transform.position + new Vector3(-translatepos.x, 0, 0);
                    return;
                }
            }

            if (Physics.Raycast(item.transform.position, Vector3.forward, out hit, item.transform.localScale.z))
            {
                if (hit.collider.tag.Equals("placed")){
                    item.transform.position = hit.transform.position + new Vector3(0, 0, -translatepos.z);
                    return;
                }
            }

            if (Physics.Raycast(item.transform.position, Vector3.back, out hit, item.transform.localScale.z))
            {
                if (hit.collider.tag.Equals("placed")){
                    item.transform.position = hit.transform.position + new Vector3(0, 0, translatepos.z);
                    return;
                }
            }

        }

    int clampvalues<T>(int index, List<T> list) {

        if (index > list.Count - 1)
        {
            index = 0;
        }
        if (index < 0)
        {
            index = list.Count - 1;
        }

        return index;

    }

}