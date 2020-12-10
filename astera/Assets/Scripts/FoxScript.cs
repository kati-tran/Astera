using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoxScript : MonoBehaviour
{
    bool active = false;
    bool initialAnimation = false;
    bool initAnimTargetReached = false;
    Vector3 initAnimTargetPos;
    float initAnimWaitStartTime;

    public GameObject player;
    public float followDistance = 1;
    public float followHeight = 0;
    public float followSpeed = 1;
    public float initAnimWalkSpeed = 1;
    public float initAnimWaitTime = 1;

    void Start()
    {
        if(player == null)
            throw new System.NullReferenceException("Player GameObject reference not set or invalid on Fox Script");
        if (followDistance <= 0 || followSpeed <= 0)
            throw new System.Exception("FoxScript fields followDistance and followSpeed must be positive and nonzero");
    }

    public void Activate()
    {
        if(!active)
        {
            active = true;
            initialAnimation = true;
            initAnimTargetPos = transform.position + new Vector3(-7, 0);
        }
    }

    void FixedUpdate()
    {
        if(active)
        {
            Vector2 dist;
            if(initialAnimation)
            {
                if(!initAnimTargetReached)
                {
                    dist = initAnimTargetPos - transform.position;
                    if (dist.magnitude < followSpeed * Time.fixedDeltaTime)
                    {
                        initAnimTargetReached = true;
                        initAnimWaitStartTime = Time.fixedTime;
                        transform.position = new Vector3(initAnimTargetPos.x, initAnimTargetPos.y, -6);
                    }
                    else
                    {
                        transform.position += (Vector3)dist.normalized * (initAnimWalkSpeed * Time.fixedDeltaTime);
                    }
                }
                else if(Time.fixedTime - initAnimWaitStartTime >= initAnimWaitTime)
                {
                    initialAnimation = false;
                }
            }
            else
            {
                dist = player.transform.position + new Vector3(0, followHeight) - transform.position;

                if (Mathf.Abs(dist.x) <= followDistance)
                    dist.x = 0;
                else
                    dist.x -= followDistance * Mathf.Sign(dist.x);

                if(dist.magnitude < 0.1)
                    return;

                if (dist.x > 0)
                    transform.rotation = Quaternion.Euler(0, 90, 0);
                else
                    transform.rotation = Quaternion.Euler(0, -90, 0);

                if (dist.magnitude <= followSpeed * Time.fixedDeltaTime)
                    transform.position += (Vector3)dist;
                else
                    transform.position += (Vector3)dist.normalized * (followSpeed * Time.fixedDeltaTime * (dist.magnitude / followDistance));
            }
        }
    }
}
