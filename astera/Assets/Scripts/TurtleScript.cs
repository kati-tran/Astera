using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurtleScript : MonoBehaviour
{

    public ParticleSystem beam;
    public ParticleSystem center;
    public ParticleSystem circle;
    public GameObject turtle;

    bool playedAudio;

    // Start is called before the first frame update
    void Start()
    {
        playedAudio = false;
    }


    void OnTriggerEnter2D (Collider2D col)
    {
    	if (col.gameObject.tag == "Player"){
    		if (gameObject.name != "lastTurtle")
    		{	
                beam.Stop();
                center.Stop();
                circle.Stop();
                if (!playedAudio)
                { 
                    FindObjectOfType<AudioManager>().Play("turtle");
                    playedAudio = true;
                }
    			Destroy(turtle);
    		}
    		else
    		{
    			GetComponent<FollowPlayer>().enabled = true;
                if (!playedAudio)
                {
                    FindObjectOfType<AudioManager>().Play("lastTurtle");
                    playedAudio = true;
                }
                    
    		}

    	}

    }

}
