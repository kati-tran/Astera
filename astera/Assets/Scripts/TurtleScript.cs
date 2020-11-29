using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurtleScript : MonoBehaviour
{
	public int found = 0;	// how many times the player has found the turtle
	Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate()
    {
    	
    	
    }

    void OnTriggerEnter2D (Collider2D col)
    {
    	if (col.gameObject.tag == "Turtle"){
    		if (col.gameObject.name != "lastTurtle")
    		{	
    			// add some kind of animation here
    			// and a sound
    			Destroy(col.transform.parent.gameObject);
    		}
    		else
    		{
    			col.transform.parent.GetComponent<FollowPlayer>().enabled = true;
    		}

    	}

    }

}
