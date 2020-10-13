using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public GameObject target;
    public float camFloat = 1f;
    public float camLeadVertical = .5f;
    public float camLeadHorizontal = 1.5f;

    Rigidbody2D targetBody;

    private Vector3 velocity = Vector3.zero;

    // Start is called before the first frame update
    void Start()
    {
        targetBody = target.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 targetCamPos = targetBody.position + targetBody.velocity * (new Vector2(camLeadHorizontal, camLeadVertical) / 10);
        Vector2 camPos = transform.position;
        Vector2 camDelta = 30 * Time.deltaTime * (targetCamPos - camPos) / (Mathf.Abs(camFloat) + 1);
        transform.position += (Vector3)camDelta;
    }
}
