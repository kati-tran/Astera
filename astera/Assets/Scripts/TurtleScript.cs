using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurtleScript : MonoBehaviour
{
    bool playedAudio;
    public GameObject turtle;

    // Start is called before the first frame update
    void Start()
    {
        playedAudio = false;
    }

    void Update()
    {
        if (turtle.GetComponent<FollowPlayer>().enabled)
        {
            // changes 3d model rotation if player is moving left or right
            if (Input.GetAxis("Horizontal") < 0) 
            {
                turtle.transform.rotation = Quaternion.Euler(0, 90, 0);
            }
            else if (Input.GetAxis("Horizontal") > 0)
            {
                turtle.transform.rotation = Quaternion.Euler(0, -90, 0);
            }

        }

    }


    void OnTriggerEnter2D (Collider2D col)
    {
        if (col.gameObject.tag == "Player"){

            if (gameObject.name == "turtleStop")
            {
                turtle.GetComponent<butterflyFollow>().enabled = false;
            }
            
            else if (gameObject.name == "lastTurtle")
            {
                turtle.GetComponent<FollowPlayer>().enabled = true;
                turtle.transform.rotation = Quaternion.Euler(0, -90, 0);
                if (!playedAudio)
                {
                    FindObjectOfType<AudioManager>().Play("lastTurtle");
                }
             }

            else
            {   
                if (!playedAudio)
                { 
                    FindObjectOfType<AudioManager>().Play("turtle");
                    playedAudio = true;
                }
                //Destroy(turtle);
            }
        }   

    }
}
