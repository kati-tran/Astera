using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class triggerPoint : MonoBehaviour
{
    public GameObject bird;
    private void OnTriggerEnter2D(Collider2D col){
        bird.SetActive(true);
        Object.Destroy(this.gameObject);
    }
}
