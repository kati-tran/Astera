using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove3D : MonoBehaviour
{
    //Configurable properties
    public float moveSpeed = 150f;
    public float jumpStrength = 30f;
    public float horizontalDrag = 2f;
    public float verticalDrag = .2f;
    public int maxAirJumps = 0;
    public Collider groundContactCollider;

    //Set up during Start()
    Rigidbody rigidBody;
    Animator animator;

    //Variables
    int jumpCount = 0;
    bool onGround;
    bool jumpInput = false;
    List<Collider> Colliders = new List<Collider>(); //For keeping track of what colliders groundContactCollider is in contact with

    // Start is called before the first frame update
    void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
    }

    // Frame update
    void Update()
    {
        // sets animation speed for walking/running/idling
        animator.SetFloat("Speed", Mathf.Abs(rigidBody.velocity.x));

        // changes 3d model rotation if player is moving left or right
        if (Input.GetAxis("Horizontal") < 0) 
        {
            transform.rotation = Quaternion.Euler(0, -91, 0);
        }
        else if (Input.GetAxis("Horizontal") > 0)
        {
            transform.rotation = Quaternion.Euler(0, 91, 0);
        }


        if (Input.GetButtonDown("Jump"))
        {
            jumpInput = true;
        }
    }

    // Physics update
    void FixedUpdate()
    {
        //Apply horizontal movement force
        Vector3 moveForce = new Vector3(Input.GetAxis("Horizontal") * moveSpeed, 0);
        rigidBody.AddForce(moveForce);

        //Update jumping related variables and apply jump impulse (if applicable)
        onGround = Colliders.Count > 0;
        if (onGround && jumpCount > 0)
            jumpCount = 0;
        if (jumpInput)
        {
            if (canJump())
            {
                rigidBody.AddForce(new Vector3(0f, jumpStrength), ForceMode.Impulse);
                ++jumpCount;
            }
            jumpInput = false;
        }

        //Apply drag force (implemented manually to allow for different amounts of drag in different axes)
        Vector3 dragForce = Vector3.Scale(Vector3.Scale(rigidBody.velocity, abs(rigidBody.velocity)), -new Vector3(horizontalDrag, verticalDrag));
        rigidBody.AddForce(dragForce);
    }

    private bool canJump()
    {
        return onGround || jumpCount < maxAirJumps;
    }

    private Vector3 abs(Vector3 v)
    {
        return new Vector3(Mathf.Abs(v.x), Mathf.Abs(v.y), Mathf.Abs(v.z));
    }

    void OnTriggerEnter(Collider other)
    {
        Debug.Log("OnTriggerEnter: " + other.name);
        Colliders.Add(other);
    }

    void OnTriggerExit(Collider other)
    {
        Debug.Log("OnTriggerExit: " + other.name);
        if (!Colliders.Remove(other))
            Debug.Log("Failed to remove!!!!!!");
    }
}
