using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fish : MonoBehaviour
{
	Rigidbody2D rb;
    Vector3 moveTo;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
    
        transform.Translate(Vector3.forward * Time.deltaTime);
        
        
    }

    void FixedUpdate()
    {
    	
    }


    void OnTriggerExit2D (Collider2D col)
    {
        if (col.tag == "Water"){
            Debug.Log("WATERERE");
            transform.rotation *= Quaternion.Euler(0,180f,0);
        }
        
    }



}
