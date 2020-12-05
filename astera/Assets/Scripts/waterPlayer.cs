using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class waterPlayer : MonoBehaviour
{
    
	Rigidbody2D rb;
    float floatPosition;            // where the cat should start floating
    public int floatForce;          //  how hard the cat bobs up and down
    public float floatDifference;   // the length from the original floatPosition the cat is allowed to vibe in

    public bool turtle;
    public int swimForce;
    bool underwater;

    // Start is called before the first frame update
    void Start()
    {   
        rb = GetComponent<Rigidbody2D>();
        underwater = false;
        turtle = false;
    }

    // Update is called once per frame
    void Update()
    {

        
    }

    void FixedUpdate()
    {
        
        // when the cat sinks below this point, push it up
        if (rb.transform.position.y < floatPosition - floatDifference) {
            rb.AddForce(transform.up * floatForce);
        }

        // when the cat goes above this point, push it down
        else if(rb.transform.position.y < floatPosition - floatDifference) {
            rb.AddForce(transform.up * -floatForce);
        }

        if (turtle && underwater)
        {

            if (Input.GetAxis("Vertical") > 0)
            {
                rb.AddForce(transform.up * swimForce);
                floatPosition = rb.transform.position.y;
                floatDifference = .2f;
            }
            else if (Input.GetAxis("Vertical") < 0)
            {
                rb.AddForce(transform.up * -swimForce);
                floatPosition = rb.transform.position.y;
                floatDifference = .2f;
            }
        }
    	
    }

    void OnTriggerEnter2D (Collider2D col)
         {
            if (col.gameObject.tag == "Water"){

                FindObjectOfType<AudioManager>().Play("splash");
                //Debug.Log("Entered at" + rb.transform.position.y);
                floatPosition = rb.transform.position.y;
                rb.gravityScale = 0.1f;
                rb.mass = 0.5f;
                underwater = true;

               
            }

            if (col.gameObject.name == "lastTurtle"){
                //Debug.Log("TORTLE");
                turtle = true;
            }
         }

    void OnTriggerExit2D (Collider2D col)
    {
         if (col.gameObject.tag == "Water"){
           // FindObjectOfType<AudioManager>().Play("splash");

           rb.gravityScale = 4;
           rb.mass = 1;
           underwater = false;
         }

    }
}
