
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class Item : MonoBehaviour
{

    [SerializeField] private Camera maincamera;
    private RaycastHit hit;

    void Start()
    {
        if (transform.parent == null) {

            if (!transform.GetComponent<Rigidbody>()) { 
                transform.AddComponent<Rigidbody>();
                transform.GetComponent<Rigidbody>().collisionDetectionMode = CollisionDetectionMode.Continuous;
            }

            if (!transform.GetComponent<BoxCollider>())
                transform.AddComponent<BoxCollider>();

            if (transform.tag.Equals("Pistol"))
            {
                transform.GetComponent<BoxCollider>().size = new Vector3(0.05f, 0.15f, 0.24f);
                transform.GetComponent<BoxCollider>().center = new Vector3(0, -0.025f, -0.03f);
            }
        }
    }

    void Update()
    {
        dropping();
    }

    bool selected() {
        if (Physics.Raycast(maincamera.transform.position, maincamera.transform.forward, out hit, 5)) {
            if (hit.transform == transform)
            {
                return true;
            }
        }
        return false;
    }

    void dropping()
    {

        if (Input.GetKeyDown(KeyCode.Q) && FindObjectOfType<Player>().itemTag().Equals(transform.tag))
        {

            if (!transform.GetComponent<Rigidbody>())
                transform.AddComponent<Rigidbody>();

            if (!transform.GetComponent<BoxCollider>())
                transform.AddComponent<BoxCollider>();

            transform.GetComponent<Rigidbody>().AddForce(-transform.forward * 500 * Time.deltaTime);

            if (Random.Range(1, 3) == 1)
                transform.GetComponent<Rigidbody>().AddTorque(transform.forward * 500);
            else
                transform.GetComponent<Rigidbody>().AddTorque(transform.forward * -500);

            if (transform.tag.Equals("Pistol")) { 
                transform.GetComponent<BoxCollider>().size = new Vector3(0.05f, 0.15f, 0.24f);
                transform.GetComponent<BoxCollider>().center = new Vector3(0, -0.025f, -0.03f);
            }
            transform.parent = null;

            FindObjectOfType<Player>().setHolding(false);
        }

        if (Input.GetKeyDown(KeyCode.E) && !FindObjectOfType<Player>().isHolding() && selected())
        {
            Destroy(transform.GetComponent<Rigidbody>());
            Destroy(transform.GetComponent<BoxCollider>());
            transform.parent = maincamera.transform;
            transform.position = maincamera.transform.position + maincamera.transform.TransformDirection(new Vector3(0.6f, -0.40f, 1));
            transform.rotation = maincamera.transform.rotation * Quaternion.Euler(0f, 180f, 0f);
            FindObjectOfType<Player>().setHolding(true);
        }

    }

}
