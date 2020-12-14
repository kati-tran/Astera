using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class leadTrigger : MonoBehaviour
{
    FoxScript fox;

    void Start()
    {
        fox = FindObjectOfType<FoxScript>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        PlayerMove move = other.gameObject.GetComponent<PlayerMove>();
        if(move != null && other == move.groundContactCollider)
        {
            fox.Lead();
            Destroy(gameObject);
        }
    }
}
