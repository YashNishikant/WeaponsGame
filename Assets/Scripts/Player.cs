using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private CharacterController controller;
    [SerializeField] private float jumpHeight;
    [SerializeField] private float distToGround;
    [SerializeField] private float gravity;
    private Vector3 velocity;
    private bool hitGround;
    private bool holding;
    private bool hasWeapon;

    void Start()
    {

    }

    void FixedUpdate()
    {
        playerMovement();
        playergravity();
        
    }
    public string itemTag()
    {
        try {
            return transform.GetChild(0).GetChild(0).tag;
        }
        catch(System.Exception e)
        {
            return "N/A";
        }
    }

    void playerMovement()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;


        if (hitGround && Input.GetKey(KeyCode.Space))
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }
        if (Input.GetKey(KeyCode.LeftShift)){
            controller.Move(move * speed/3 * Time.deltaTime);
        }
        else
        {
            controller.Move(move * speed * Time.deltaTime);
        }
        hitGround = Physics.Raycast(transform.position, Vector3.down, distToGround + 0.1f);
    }

    void playergravity()
    {
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);

        if (hitGround)
        {
            velocity.y = 0;
        }
    }

    public void setHolding(bool x) {
        holding = x;
    }
    public bool isHolding()
    {
        return holding;
    }
}