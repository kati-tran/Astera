using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public GameObject target;
    public float camFloat = 2f;
    public float camLeadVertical = 1f;
    public float camLeadHorizontal = 2f;
    public float camElevation = 3f;
    public float camDistance = 10f;

    Rigidbody2D targetBody2D;
    Rigidbody targetBody3D;
    bool use3D = false;
    Vector3 camOffset;

    // Start is called before the first frame update
    void Start()
    {
        targetBody2D = target.GetComponent<Rigidbody2D>();
        if(targetBody2D == null)
        {
            targetBody3D = target.GetComponent<Rigidbody>();
            use3D = true;
        }
        Vector3 pos = use3D ? targetBody3D.position : (Vector3)targetBody2D.position;
        camOffset = new Vector3(0, camElevation, -camDistance);
        transform.position = pos + camOffset;
        transform.Rotate(Mathf.Atan2(camElevation, camDistance) * Mathf.Rad2Deg, 0, 0);
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 pos = use3D ? targetBody3D.position : (Vector3)targetBody2D.position,
        velocity = use3D ? targetBody3D.velocity : (Vector3)targetBody2D.velocity;
        Vector3 targetCamPos = pos + camOffset + Vector3.Scale(velocity, new Vector3(camLeadHorizontal, camLeadVertical, 0) / 10);
        Vector3 camDelta = 30 * Time.deltaTime * (targetCamPos - transform.position) / (Mathf.Abs(camFloat) + 1);
        transform.position += camDelta;
    }
}
