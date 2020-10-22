using System.Collections;
using System.Collections.Generic;
using UnityEngine;
	
	

public class waterFloat : MonoBehaviour
{
	public float waterForce;
	public float floatForce;
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
	 	if (col.gameObject.tag == "Water"){
	      	rb.AddForce(transform.up * waterForce);
	    }
	 }

	 void OnTriggerStay2D (Collider2D col)
	 {	
	      var hitObject = col.gameObject.name;
	      if (col.gameObject.tag == "Water"){
	      	rb.AddForce(transform.up * waterForce);
	      }

	      else if (col.gameObject.tag == "WaterTop"){
	      	rb.AddForce(transform.up * floatForce);
	      }
	  		//Debug.Log("I collided with the " + hitObject + " !");
	  		
	  }
}
