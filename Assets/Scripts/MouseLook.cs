using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook : MonoBehaviour
{
    public float MouseSens;
    public Transform player;
    public float xRotation = 0;
    public Camera maincamera;
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        MouseSens = 10000f * Time.deltaTime;
    }

    RaycastHit hit;

    void Update()
    {
            float mouseX = Input.GetAxis("Mouse X") * MouseSens * Time.deltaTime;
            float mouseY = Input.GetAxis("Mouse Y") * MouseSens * Time.deltaTime;

            xRotation -= mouseY;
            xRotation = Mathf.Clamp(xRotation, -70f, 90f);

            transform.localRotation = Quaternion.Euler(xRotation, 0, 0);
            player.Rotate(Vector3.up * mouseX);

    }
}
