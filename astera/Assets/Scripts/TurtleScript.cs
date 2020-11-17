using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurtleScript : MonoBehaviour
{
	public int found = 0;	// how many times the player has found the turtle
	public int maxEscapes;	// how many times the turtle can escape
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
    		found++;
    		if (found <= maxEscapes)
    		{
    			Destroy(col.gameObject);
    		}
    		else
    		{

    		}

    	}

    }

}
