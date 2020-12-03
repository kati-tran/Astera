using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fish : MonoBehaviour
{


    void FixedUpdate()
    {
    	transform.Translate(Vector3.forward * Time.deltaTime);
    }


    void OnTriggerExit2D (Collider2D col)
    {
        if (col.tag == "Water"){
            transform.rotation *= Quaternion.Euler(0,180f,0);
        }
        
    }


}
