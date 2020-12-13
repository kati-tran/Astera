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
    int waterColliderCount = 0;
    bool underwater;
    bool swimming;
    bool swimmingLoop;

    public float lilipadForce;
    AudioManager AudioManager;

    // Start is called before the first frame update
    void Start()
    {   
        rb = GetComponent<Rigidbody2D>();
        underwater = false;
        turtle = false;
        AudioManager = FindObjectOfType<AudioManager>();
        swimmingLoop = false;
    }

    void Update()
    {
        if(!swimmingLoop && swimming && !AudioManager.isPlaying("swim")){
            AudioManager.Play("swim"); 
            swimmingLoop = true;
        }
        else if (swimmingLoop && !swimming)
        {
            AudioManager.fadeOut("swim", .5f);
            swimmingLoop = false;
        }


    }

    void FixedUpdate()
    {
        if (underwater && (rb.velocity.x > .1 || rb.velocity.x < -.1) && (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.RightArrow)))
        {
            swimming = true;
        }
        else
        {
            swimming = false;
        }

        if (underwater)
        {
            // when the cat sinks below this point, push it up
            if (rb.transform.position.y < floatPosition - floatDifference)
            {
                rb.AddForce(transform.up * floatForce);
            }

            // when the cat goes above this point, push it down
            else if (rb.transform.position.y > floatPosition - floatDifference)
            {
                rb.AddForce(transform.up * -floatForce);
            }
        }

        if (turtle && underwater)
        {

            if (Input.GetAxis("Vertical") > 0)
            {
                rb.AddForce(transform.up * (swimForce * 2));
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

                AudioManager.Play("splash");
                //Debug.Log("Entered at" + rb.transform.position.y);
                
                //if (!turtle)
                    floatPosition = rb.transform.position.y;

                rb.gravityScale = 0.1f;
                rb.mass = 0.5f;
                underwater = true;
                ++waterColliderCount;
               
            }

            if (col.gameObject.name == "lastTurtle"){
                //Debug.Log("TORTLE");
                turtle = true;
            }

            if (col.gameObject.name.StartsWith("SNature_CupedLil") && underwater)
            {
                transform.position = col.gameObject.transform.position;
            }
         }

    void OnTriggerExit2D (Collider2D col)
    {
         if (col.gameObject.tag == "Water"){
            // FindObjectOfType<AudioManager>().Play("splash");
            --waterColliderCount;
            if(waterColliderCount <= 0)
            {
                waterColliderCount = 0;
                rb.gravityScale = 4;
                rb.mass = 1;
                underwater = false;
            }
         }

    }
}
