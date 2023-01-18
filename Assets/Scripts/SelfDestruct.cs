using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelfDestruct : MonoBehaviour
{

    float time = 0;
    [SerializeField] float timelimit;
    void Update()
    {

        time += Time.deltaTime;

        if (time > timelimit)
            Destroy(this.gameObject);
    }
}
