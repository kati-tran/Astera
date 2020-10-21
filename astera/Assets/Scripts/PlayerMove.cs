using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    //Configurable properties
    public float moveSpeed = 55f;
    public float jumpStrength = 33f;
    public float stopSpeed = 0.5f; //Player will come to a compete stop when speed falls below this value
    public float horizontalDrag = 0.3f;
    public float verticalDrag = 0.1f;
    public int maxAirJumps = 0;
    public int jumpLeniency = 5;
    public Collider2D groundContactCollider;
    public float flightDuration = 5f;
    public float flightStrength = 20f;
    public float flyDrag = 1f;

    

    //Set up during Start()
    Rigidbody2D rigidBody;
    ContactFilter2D groundFilter;

    //Variables
    int jumpCount = 0;
    int offGroundCount = 0;
    bool onGround;
    bool jumpInput = false;
    float flightTime = 5f; 
    float OGhdrag;
    


    // Start is called before the first frame update
    void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        groundFilter = new ContactFilter2D(); //{ layerMask = LayerMask.GetMask("Ground"), useLayerMask = true }; //not currently filtering
        OGhdrag = horizontalDrag;

    }

    // Frame update
    void Update()
    {   
        // sets animation speed for walking/running/idling
        GetComponent<Animator>().SetFloat("Speed", Mathf.Abs(rigidBody.velocity.x));

        // changes 3d model rotation if player is moving left or right
        // Left
        if (Input.GetAxis("Horizontal") < 0)
        {
            transform.rotation = Quaternion.Euler(0, 180, 0);
        }
        // right
        else if (Input.GetAxis("Horizontal") > 0)
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }


        if(Input.GetButtonDown("Jump"))
        {
            jumpInput = true;
        }
    }

    // Physics update
    void FixedUpdate()
    {
        //Update jumping related variables and apply jump impulse (if applicable)
        onGround = groundContactCollider.IsTouching(groundFilter);
        if (!onGround && offGroundCount < jumpLeniency)
            ++offGroundCount;
        if (onGround)
        {
            if ((float)horizontalDrag == (float)(OGhdrag + flyDrag))
            {
                horizontalDrag -= flyDrag;
            }
                
            if (jumpCount > 0)
                jumpCount = 0;
            if (offGroundCount > 0)
                offGroundCount = 0;
        }
        if(jumpInput)
        {
            if(canJump())
            {
                rigidBody.AddForce(new Vector2(0f, jumpStrength), ForceMode2D.Impulse);
            }
            bird();
            // if(canAirJump() && flightTime > 0 && Input.GetKey("space"))
            // {
            //     // rigidBody.AddForce(new Vector2(0f, jumpStrength), ForceMode2D.Impulse);
            //     // ++jumpCount;
            //     rigidBody.AddForce(new Vector2(0f,flightStrength), ForceMode2D.Force);
            //     flightTime -= 0.1f;
            // }
            // else {
            //     jumpInput = false;
            // }
            // Reset the flight timer if on ground
            // if (onGround){
            //     flightTime = flightDuration;
            // }
            // jumpInput = false;
        }

        //Apply horizontal movement force
        if (Input.GetAxis("Horizontal") == 0)   //If no movement input
        {
            if(Mathf.Abs(rigidBody.velocity.x) > stopSpeed)                                         //If speed is above stop threshold
                rigidBody.AddForce(new Vector2(moveSpeed * -Mathf.Sign(rigidBody.velocity.x), 0));      //Apply force in the direction opposite to horizontal movement in order to bring player to a stop
            else                                                                                    //If speed is at or below stop threshold
                rigidBody.velocity = new Vector2(0, rigidBody.velocity.y);                              //Bring player to a stop in horizontal direction
        }
        else                                    //If there is movement input
        {
            rigidBody.AddForce(new Vector2(Input.GetAxis("Horizontal") * moveSpeed, 0)); //Apply force in direction of input
        }

        //Apply drag force (implemented manually to allow for different amounts of drag in different axes)
        Vector2 dragForce = rigidBody.velocity * abs(rigidBody.velocity) * -new Vector2(horizontalDrag, verticalDrag);
        rigidBody.AddForce(dragForce);
    }

    private bool canJump()
    {
        return onGround || offGroundCount < jumpLeniency;
    }

    private bool canAirJump()
    {
        return !canJump() && jumpCount < maxAirJumps;
    }

    private Vector2 abs(Vector2 v)
    {
        return new Vector2(System.Math.Abs(v.x), System.Math.Abs(v.y));
    }

    private void bird(){
        if(canAirJump() && flightTime > 0 && Input.GetKey("space"))
            {
                horizontalDrag = OGhdrag + flyDrag;
                rigidBody.AddForce(new Vector2(0f,flightStrength), ForceMode2D.Force);
                flightTime -= 0.1f;
            }
            else {
                jumpInput = false;
                
            }
        if (onGround){
            flightTime = flightDuration;
        }
    }
}
