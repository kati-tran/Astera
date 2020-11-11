using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fishSwim : MonoBehaviour
{
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

    void OnCollision2D (Collider2D col)
    {
    	if (col.gameObject.tag == "Ground"){
    		
    	}
    }

    void OnTriggerStay2D (Collider2D col)
    {
    	
    }
}
