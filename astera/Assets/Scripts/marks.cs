﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class marks : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D col)
    {

        if (col.gameObject.tag == "Player")
        {
            Destroy(transform.gameObject);

        }
    }
     
}
