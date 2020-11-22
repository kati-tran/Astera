using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class leafSpawner : MonoBehaviour
{
    Transform tf;
    Vector3 pos;
    public float spawnDelay = 5f;
    public float currentTime = 0f;
    public float offset = 1f;
    public float leaflifetime = 12f;
    public GameObject leaf;
    // Start is called before the first frame update
    void Start()
    {
        tf = GetComponent<Transform>();
        pos = tf.position;
    }

    void FixedUpdate()
    {
        if (offset > 0f){
            offset -= 0.1f;
        }
        else if (currentTime >= spawnDelay)
        {
            GameObject obj = Instantiate(leaf) as GameObject;
            obj.transform.position = pos;
            obj.transform.parent = tf;
            FallingLeaf fallingleaf = obj.GetComponent<FallingLeaf>();
            fallingleaf.lifetime = leaflifetime;
            pos.z = 0;
            currentTime = 0f;
        } else { currentTime += 0.1f; }
    }
}
