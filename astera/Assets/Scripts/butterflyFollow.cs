﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class butterflyFollow : MonoBehaviour
{
    private GameObject[] objects1;
    private GameObject nearestTarget;
    private float distance;
    private float curDistance;
    private Vector3 pos;
    public float butterflySpeed = 0.1f;

    // Start is called before the first frame update
    void Start(){
        nearestTarget = null;
        distance = Mathf.Infinity;
        pos = transform.position;
        objects1 = GameObject.FindGameObjectsWithTag("butterMark");
        FindClosest();
    }

    void FixedUpdate(){
        FindClosest();
        transform.position = Vector3.MoveTowards(pos,nearestTarget.transform.position, butterflySpeed);
        nearestTarget = null;
        distance = Mathf.Infinity;
    }


    void FindClosest(){
        objects1 = GameObject.FindGameObjectsWithTag("butterMark");
        pos = transform.position;
        foreach (GameObject obj in objects1){
            if (!obj.active){
                continue;
            }
            curDistance = (obj.transform.position - pos).magnitude;
            if (curDistance < distance){
                nearestTarget = obj;
                distance = curDistance;
            }
        }
    }
}
