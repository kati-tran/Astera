﻿﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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

    //Animal Scripts
    public GameObject birdObj;
    public float dashDrag = .1f;
    public float dashStrength;
    public int maxDashes = 1;
    public float dashLength = .5f;
    public float dashCooldown = 1f;

    //Set up during Start()
    public Rigidbody2D rigidBody;
    ContactFilter2D groundFilter;
    Animator anim;

    //Variables
    int jumpCount = 0;
    int dashCount = 0;
    int offGroundCount = 0;
    bool onGround;
    bool jumpInput = false;

    //Bird Variables
    float OGhdrag;
    float flightDuration;
    float flightStrength;
    float flyDrag;
    float flightTime;

    public bool isFlying = false;

    //Fox variables
    float lastDashTime;
    bool dashInput = false;

    public Spirit activeSpirit = Spirit.None;
    Direction facing = Direction.Right;

    public enum Spirit { None, Bird, Turtle, Fox };
    public enum Direction { Left, Right }

    // Start is called before the first frame update
    void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        groundFilter = new ContactFilter2D(); //{ layerMask = LayerMask.GetMask("Ground"), useLayerMask = true }; //not currently filtering
        
        OGhdrag = horizontalDrag;
        flightDuration = birdObj.GetComponent<birdController>().flightDuration;
        flightStrength = birdObj.GetComponent<birdController>().flightStrength;
        flyDrag = birdObj.GetComponent<birdController>().flyDrag;
        flightTime = birdObj.GetComponent<birdController>().flightTime;

        anim = GetComponent<Animator>();


    }

    // Frame update
    void Update()
    {   
        // sets animation speed for walking/running/idling
        anim.SetFloat("Speed", Mathf.Abs(rigidBody.velocity.x));

        if(Input.GetButtonDown("Jump"))
        {
            jumpInput = true;
        }

        if(Input.GetButtonDown("Fire3"))
        {
            dashInput = true;
        }
    }

    // Physics update
    void FixedUpdate()
    {
        if (Input.GetKey("r")){
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
        // changes 3d model rotation if player is moving left or right
        // Left
        float horizInput = Input.GetAxis("Horizontal");
        if (horizInput < 0 && facing == Direction.Right)
        {
            facing = Direction.Left;
            transform.rotation = Quaternion.Euler(0, 180, 0);
        }
        // right
        else if (horizInput > 0 && facing == Direction.Left)
        {
            facing = Direction.Right;
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }

        //Update jumping related variables and apply jump impulse (if applicable)
        onGround = groundContactCollider.IsTouching(groundFilter);
        if (!onGround && offGroundCount < jumpLeniency)
            ++offGroundCount;
        if (onGround)
        {
            isFlying = false;
            if ((float)horizontalDrag == (float)(OGhdrag + flyDrag))
            {
                horizontalDrag -= flyDrag;
            }
                
            if (jumpCount > 0)
                jumpCount = 0;
            if (offGroundCount > 0)
                offGroundCount = 0;
            if (dashCount > 0)
                dashCount = 0;
        }
        if(jumpInput)
        {
            if(canJump())
            {
                rigidBody.AddForce(new Vector2(0f, jumpStrength), ForceMode2D.Impulse);
            }
            if (activeSpirit == Spirit.Bird){
                bird();
            }
            else{
                if(canAirJump())
                {
                    rigidBody.AddForce(new Vector2(0f, jumpStrength/2), ForceMode2D.Impulse);
                    ++jumpCount;
                }
                jumpInput = false;
            }
        }

        if (activeSpirit == Spirit.Fox)
            fox();
        else if (dashInput)
            dashInput = false;

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

    private bool canAirDash()
    {
        return !canJump() && dashCount < maxDashes;
    }

    private Vector2 abs(Vector2 v)
    {
        return new Vector2(System.Math.Abs(v.x), System.Math.Abs(v.y));
    }

    private void bird(){
        if(jumpInput && birdObj.GetComponent<birdController>().flightTime > 0 && Input.GetKey("space") && !canJump())
            {
                isFlying = true;
                horizontalDrag = OGhdrag + flyDrag;
                rigidBody.AddForce(new Vector2(0f,flightStrength), ForceMode2D.Force);
                birdObj.GetComponent<birdController>().flightTime -= 0.1f;
            }
            else {
                jumpInput = false;
                
            }
        if (onGround){
            birdObj.GetComponent<birdController>().flightTime = flightDuration;
        }
    }

    private void fox()
    {
        void dash(float direction)
        {
            horizontalDrag = dashDrag;
            rigidBody.AddForce(new Vector2(direction * dashStrength * 100, 0f));
            FindObjectOfType<AudioManager>().Play("Dash");
            ParticleSystem[] particleSystems = gameObject.GetComponentsInChildren<ParticleSystem>();
            foreach(ParticleSystem particle in particleSystems)
                particle.Play();
            lastDashTime = Time.fixedTime;
        }
        float now = Time.fixedTime;
        if (horizontalDrag != OGhdrag && now - lastDashTime >= .5)
        {
            horizontalDrag = OGhdrag;
        }
        if (dashInput)
        {
            dashInput = false;
            if(now - lastDashTime > dashCooldown)
            {
                float direction = facing == Direction.Right ? 1 : -1;
                if (canJump())
                {
                    dash(direction);
                }
                else if (canAirDash())
                {
                    dash(direction);
                    ++dashCount;
                }
            }
        }
    }

    void OnCollisionStay2D(Collision2D col){
        if (col.gameObject.tag == "Slippery"){
            // Facing Left
            Vector3 force = transform.position - col.transform.position;
            rigidBody.AddForce(new Vector2(force.x, -500f), ForceMode2D.Force);
            
        }
    }
}
