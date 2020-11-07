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
    public GameObject leaf;
    // Start is called before the first frame update
    void Start()
    {
        tf = GetComponent<Transform>();
        pos = tf.position;
    }

    void FixedUpdate()
    {
        if (currentTime >= spawnDelay+offset)
        {
            GameObject obj = Instantiate(leaf) as GameObject;
            obj.transform.position = pos;
            currentTime = 0f;
        } else { currentTime += 0.1f; }
    }
}
