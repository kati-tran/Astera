using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class travelTo : MonoBehaviour
{
	public GameObject[] places;
	public float speed;
	private int at;
	//bool done;
    // Start is called before the first frame update
    void Start()
    {
    	at = 0;
        
    }


    // Update is called once per frame
    void FixedUpdate()
    {

    	if (at < places.Length){
    		float step = speed * Time.deltaTime;
	        transform.position = Vector3.MoveTowards(transform.position, places[at].transform.position, step);
	        if (transform.position == places[at].transform.position)
	        {
	        	at++;
	        }

    	}
		
    }

    void OnTriggerEnter2D(Collider2D col)
    {
    	transform.rotation *= Quaternion.Euler(0,180f,0);
    }

}
