
using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

public class Gun: MonoBehaviour
{
    [SerializeField] private Camera maincamera;
    [SerializeField] private float animationDelay = 0;
    [SerializeField] private ParticleSystem muzzleFlash;
    [SerializeField] private GameObject impactEffect;
    [SerializeField] private TrailRenderer trail;
    [SerializeField] private GameObject bullet;
    private RaycastHit hitGun;

    void Update()
    {
        try {
            if (FindObjectOfType<Player>().itemTag().Equals("Pistol"))
                shooting();
            }
        catch(System.Exception e)
        {

        }
    }

    void shooting()
    {
        
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 startposgun = transform.position + new Vector3(0, 0.1f, 0) - transform.forward / 1.6f;
            ParticleSystem e = Instantiate(muzzleFlash, startposgun, maincamera.transform.rotation);
            e.transform.parent = transform;

            //if (Physics.Raycast(maincamera.transform.position, maincamera.transform.TransformDirection(Vector3.forward), out hitGun, 200))
            //{

                GameObject b = Instantiate(bullet, startposgun, transform.rotation);

                if (!b.GetComponent<Rigidbody>())
                    b.AddComponent<Rigidbody>();

                b.GetComponent<Rigidbody>().AddForce(-transform.forward.normalized * 150, ForceMode.Impulse);
                transform.GetComponent<Rigidbody>().collisionDetectionMode = CollisionDetectionMode.Continuous;

                //TrailRenderer t = Instantiate(trail, startposgun, Quaternion.identity);
                //t.transform.position = startposgun;
                //StartCoroutine(SpawnTrail(t, hitGun));
            //}

        }
    }

        //IEnumerator SpawnTrail(TrailRenderer trail, RaycastHit hit) {
        //    float time = 0;
        //    Vector3 startpos = trail.transform.position;

        //    while (time < 1) {
        //        startpos = transform.position + new Vector3(0, 0.1f, 0) - transform.forward / 1.6f;
        //        trail.transform.position = Vector3.Lerp(startpos, hit.point, time);
        //        time += Time.deltaTime / trail.time;
        //        yield return null;
        //    }
        //    trail.transform.position = hit.point;

        //    Instantiate(impactEffect, hitGun.point, Quaternion.Euler(hitGun.normal));

        //    Destroy(trail.gameObject, trail.time);

        //}

}