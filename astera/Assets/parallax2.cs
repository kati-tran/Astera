using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class parallax2 : MonoBehaviour
{
    public GameObject Camera;
    public int numPanels = 1;
    public float parallaxDist = 1;

    float width;
    Vector3 originalPos;

    // Start is called before the first frame update
    void Start()
    {
        width = (GetComponent<SpriteRenderer>().bounds.extents.x - .1f) * 2 * numPanels ;
        originalPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 camPos = Camera.transform.position;
        Vector2 newPos = originalPos + camPos * parallaxDist;
        int n = Mathf.RoundToInt((newPos.x - camPos.x) / width);
        transform.position = new Vector3(newPos.x - (n * width), newPos.y, originalPos.z);
    }
}
