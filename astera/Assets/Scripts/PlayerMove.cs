using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    //Configurable properties
    public float moveSpeed = 150f;
    public float jumpStrength = 30f;
    public float horizontalDrag = 2f;
    public float verticalDrag = .2f;
    public int maxAirJumps = 1;
    public Collider2D groundContactCollider;

    //Set up during Start()
    Rigidbody2D rigidBody;
    ContactFilter2D groundFilter;

    //Variables
    int jumpCount = 0;
    bool onGround;
    bool jumpInput = false;

    // Start is called before the first frame update
    void Start()
    {

        rigidBody = GetComponent<Rigidbody2D>();
        groundFilter = new ContactFilter2D { layerMask = LayerMask.GetMask("Ground") };
    }

    // Frame update
    void Update()
    {   
        // sets animation speed for walking/running/idling
        GetComponent<Animator>().SetFloat("Speed", Mathf.Abs(rigidBody.velocity.x));

        // changes 3d model rotation if player is moving left or right
        if (Input.GetAxis("Horizontal") < 0)
        {
            transform.rotation = Quaternion.Euler(0, -91, 0);
        }
        else if (Input.GetAxis("Horizontal") > 0)
        {
            transform.rotation = Quaternion.Euler(0, 91, 0);
        }


        if(Input.GetButtonDown("Jump"))
        {
            jumpInput = true;
        }
    }

    // Physics update
    void FixedUpdate()
    {
        //Apply horizontal movement force
        Vector2 moveForce = new Vector2(Input.GetAxis("Horizontal") * moveSpeed, 0);
        rigidBody.AddForce(moveForce);

        //Update jumping related variables and apply jump impulse (if applicable)
        onGround = groundContactCollider.IsTouching(groundFilter);
        if (onGround && jumpCount > 0)
            jumpCount = 0;
        if(jumpInput)
        {
            if(canJump())
            {
                rigidBody.AddForce(new Vector2(0f, jumpStrength), ForceMode2D.Impulse);
                ++jumpCount;
            }
            jumpInput = false;
        }

        //Apply drag force (implemented manually to allow for different amounts of drag in different axes)
        Vector2 dragForce = rigidBody.velocity * abs(rigidBody.velocity) * -new Vector2(horizontalDrag, verticalDrag);
        rigidBody.AddForce(dragForce);
    }

    private bool canJump()
    {
        return onGround || jumpCount < maxAirJumps;
    }

    private Vector2 abs(Vector2 v)
    {
        return new Vector2(System.Math.Abs(v.x), System.Math.Abs(v.y));
    }
}
