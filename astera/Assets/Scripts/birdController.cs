﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class birdController : MonoBehaviour
{
    public GameObject player;
    PlayerMove playerScript; 
    Animator anim;

    Vector3 playerPos;
    Vector3 birdPos;
    public float xOffset = 2;
    public float yOffset = 5;
    public float birdSpeed = 0.2f;
    System.Random rnd = new System.Random();

    void Start(){
        playerScript = player.GetComponent<PlayerMove>();
        anim = player.GetComponent<Animator>();
    }

    void Awake(){
        playerPos = player.transform.position;
        // Facing left
        if (Input.GetAxis("Horizontal") < 0)
        {
            birdPos = new Vector3(playerPos.x + xOffset,playerPos.y + yOffset,playerPos.z);
        }
        // Facing right
        else if (Input.GetAxis("Horizontal") > 0)
        {
            birdPos = new Vector3(playerPos.x - xOffset,playerPos.y + yOffset,playerPos.z);
        }
        transform.position = Vector3.MoveTowards(transform.position,birdPos, 1000);

    }

    
    void Update(){
        // changes 3d model rotation if player is moving left or right
        // facing Left
        if (Input.GetAxis("Horizontal") < 0)
        {
            transform.rotation = Quaternion.Euler(0, 270, 0);
        }
        // facing Right
        else if (Input.GetAxis("Horizontal") > 0)
        {
            transform.rotation = Quaternion.Euler(0, 90, 0);
        }
        

        
    }

    void FixedUpdate(){
        playerPos = player.transform.position;
        if (playerScript.isFlying)
        {
            anim.Play("Run", -1, 0f);
            birdPos = new Vector3(playerPos.x,playerPos.y + 1.3f ,playerPos.z);
            // birdPos = new Vector3(playerPos.x + 1f,playerPos.y + 1.3f ,playerPos.z);
            transform.position = Vector3.MoveTowards(transform.position,birdPos, birdSpeed * 2);
        }
        else{
            if (Input.GetAxis("Horizontal") < 0)
                birdPos = new Vector3(playerPos.x + xOffset,playerPos.y + yOffset,playerPos.z);
            else if (Input.GetAxis("Horizontal") > 0)
                birdPos = new Vector3(playerPos.x - xOffset,playerPos.y + yOffset,playerPos.z);
            // else {
            //     birdPos = new Vector3(playerPos.x,playerPos.y + 1.3f ,playerPos.z);
            // }
            transform.position = Vector3.MoveTowards(transform.position,birdPos, birdSpeed);
        }
    }
}