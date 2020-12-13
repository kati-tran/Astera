using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class catEndScene : MonoBehaviour
{
	Animator anim;
	public GameObject mainCat;
	bool ran;
    // Start is called before the first frame update


    void Start()
    {
    	anim = GetComponent<Animator>();
        anim.SetBool("run", true);
        ran = false;
    	//transform.rotation = Quaternion.Euler(0, 91, 0);
    }

    // Update is called once per frame
    void Update()
    {
    	if(ran)
    	{
	    		        // changes 3d model rotation if player is moving left or right
	        if (Input.GetAxis("Horizontal") < 0) 
	        {
	            transform.rotation = Quaternion.Euler(0, -180, 0);
	        }
	        else if (Input.GetAxis("Horizontal") > 0)
	        {
	            transform.rotation = Quaternion.Euler(0, 0, 0);
	        }
        anim.SetFloat("Speed", Mathf.Abs(mainCat.GetComponent<Rigidbody2D>().velocity.x));
    	}
        	
    }

    void FixedUpdate()
    {

    }

    void OnTriggerEnter2D(Collider2D col)
    {
    	Debug.Log(col.gameObject.name);
    	if (col.gameObject.tag == "Player" && !ran)
    	{
    		anim.SetBool("run", false);
    		GetComponent<PlayerMove>().enabled = true;
    		GetComponent<travelTo>().enabled = false;
    		ran = true;
    	}
    }
}
