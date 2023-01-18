using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        Destroy(this.gameObject.transform.GetChild(0).gameObject, 0.01f);
        Destroy(transform.GetComponent<Rigidbody>(), 0.01f);
    }
}
