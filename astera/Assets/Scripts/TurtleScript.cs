using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurtleScript : MonoBehaviour
{

    public ParticleSystem beam;
    public ParticleSystem center;
    public ParticleSystem circle;
    public GameObject turtle;

    // Start is called before the first frame update
    void Start()
    {
    }


    void OnTriggerEnter2D (Collider2D col)
    {
    	if (col.gameObject.tag == "Player"){
    		if (gameObject.name != "lastTurtle")
    		{	
                beam.Stop();
                center.Stop();
                circle.Stop();
    			// add some kind of animation here
    			// and a sound
    			Destroy(turtle);
    		}
    		else
    		{
    			GetComponent<FollowPlayer>().enabled = true;
    		}

    	}

    }

}
