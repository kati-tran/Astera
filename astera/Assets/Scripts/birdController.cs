using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class birdController : MonoBehaviour
{
    Vector3 playerPos;
    Vector3 birdPos;
    public float xOffset = 2;
    public float yOffset = 5;
    public float birdSpeed = 0.2f;
    void Awake(){
        playerPos = player.transform.position;
        if (Input.GetAxis("Horizontal") < 0)
        {
            birdPos = new Vector3(playerPos.x + xOffset,playerPos.y + yOffset,playerPos.z);
        }
        else if (Input.GetAxis("Horizontal") > 0)
        {
            birdPos = new Vector3(playerPos.x - xOffset,playerPos.y + yOffset,playerPos.z);
        }
    }

    public GameObject player;
    void Update(){
        // changes 3d model rotation if player is moving left or right
        if (Input.GetAxis("Horizontal") < 0)
        {
            transform.rotation = Quaternion.Euler(0, 270, 0);
        }
        else if (Input.GetAxis("Horizontal") > 0)
        {
            transform.rotation = Quaternion.Euler(0, 90, 0);
        }
        

        
    }

    void FixedUpdate(){
        playerPos = player.transform.position;
        if (Input.GetAxis("Horizontal") < 0)
        {
            birdPos = new Vector3(playerPos.x + xOffset,playerPos.y + yOffset,playerPos.z);
        }
        else if (Input.GetAxis("Horizontal") > 0)
        {
            birdPos = new Vector3(playerPos.x - xOffset,playerPos.y + yOffset,playerPos.z);
        }
        else 
        {
            birdPos = new Vector3(playerPos.x,playerPos.y + 1.3f ,playerPos.z);
        }
        transform.position = Vector3.MoveTowards(transform.position,birdPos, birdSpeed);
    }
}
