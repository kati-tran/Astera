﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingLeaf : MonoBehaviour
{
    public float lifetime = 50f;
    public float current = 0f;
    public float swaytime = 4f;
    AudioSource leaf;

    bool swayRight = true;
    float left = 0f;
    float right = 0f;

    public float standTime = 3f;

    Rigidbody2D rb;
    Transform tf;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        tf = GetComponent<Transform>();
        leaf = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (current >= lifetime){            
            Object.Destroy(this.gameObject);
        }
        rb.velocity = new Vector2(0, -2);
        if (swayRight){
                right += 0.1f;
                tf.Translate(Vector3.right * Time.deltaTime);
                if (right >= swaytime){
                    right = 0f;
                    swayRight = false;
                }
        } else {
            left += 0.1f;
                tf.Translate(Vector3.left * Time.deltaTime);
                if (left >= swaytime){
                    left = 0f;
                    swayRight = true;
                }
        }
        current += 0.1f;
    }

    void OnTriggerEnter2D(Collider2D col){
        if (col.tag == "Player"){
            Object.Destroy(this.gameObject);
        }   
    }
    
    void OnCollisionStay2D(Collision2D col){
        if (standTime <= 0f){
            Object.Destroy(this.gameObject);
        }
        standTime -= 0.1f;
    }
    void OnCollisionEnter2D(Collision2D col){
        if (col.gameObject.tag == "Player"){
            leaf.Play(0);
        }   
    }
}
