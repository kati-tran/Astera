using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class FoxScript : MonoBehaviour
{
    bool leading = false;
    bool active = false;
    bool initialAnimation = false;
    bool initAnimTargetReached = false;
    Vector3 initAnimTargetPos;
    float initAnimWaitStartTime;
    bool facingLeft = true;
    Vector3 initialPos;

    public GameObject player;
    public float followDistance = 1;
    public float followHeight = 0;
    public float followSpeed = 1;
    public float initAnimWalkSpeed = 1;
    public float initAnimWaitTime = 1;
    public float leadMoveSpeed = 15;
    public float leadStartDist = 30;
    public float leadApproachDist = 10;
    public List<TargetPos> targetPos;
    Animator anim;

    [System.Serializable]
    public struct TargetPos
    {
        public TargetPos(Vector3 position, bool waitForPlayer)
        {
            pos = position;
            wait = waitForPlayer;
        }

        public Vector3 pos;
        public bool wait;
    }

    void Start()
    {
        if(player == null)
            throw new System.NullReferenceException("Player GameObject reference not set or invalid on Fox Script");
        if (followDistance <= 0 || followSpeed <= 0)
            throw new System.Exception("FoxScript fields followDistance and followSpeed must be positive and nonzero");
        initialPos = transform.position;
        anim = GetComponent<Animator>();
    }

    public void Activate()
    {
        if(!active)
        {
            active = true;
            initialAnimation = true;
            initAnimTargetPos = transform.position + new Vector3(-7, 0);
            FindObjectOfType<AudioManager>().Play("FoxCollect");
        }
    }

    public void Lead()
    {
        if(!active && !leading)
        {
            leading = true;
            List<TargetPos> t = new List<TargetPos>
            {
                new TargetPos(new Vector3(player.transform.position.x - leadStartDist, targetPos[0].pos.y, targetPos[0].pos.z), true)
            };
            t.AddRange(targetPos);
            StartCoroutine(_lead(t));
        }
    }

    private IEnumerator _lead(List<TargetPos> targets)
    {
        transform.position = targetPos[0].pos;
        foreach(TargetPos t in targets)
        {
            if (t.pos.x < transform.position.x)
                transform.rotation = Quaternion.Euler(0, -90, 0);
            else if (t.pos.x > transform.position.x)
                transform.rotation = Quaternion.Euler(0, 90, 0);
            while(transform.position != t.pos)
            {
                Vector3 dist = t.pos - transform.position;
                Vector2 dist2 = dist;
                float moveAmount = Time.deltaTime * leadMoveSpeed;
                if (dist2.magnitude <= moveAmount)
                {
                    transform.position = t.pos;
                    anim.SetFloat("speed", 0);
                }
                else
                {
                    Vector3 movement = dist2.normalized * moveAmount;
                    movement.z = dist.z * moveAmount / dist2.magnitude;
                    transform.position += movement;
                    anim.SetFloat("speed", leadMoveSpeed);
                }
                yield return null;
            }
            if(t.wait)
            {
                transform.rotation = Quaternion.Euler(0, 90, 0);
                while (((Vector2)(player.transform.position - transform.position)).magnitude > leadApproachDist)
                    yield return null;
            }
        }

        transform.position = initialPos;
        leading = false;
    }

    void Update()
    {
        if(active)
        {
            Vector2 dist;

            // sets animation speed for walking/running/idling
            float speed = 0;

            if (initialAnimation)
            {
                if(!initAnimTargetReached)
                {
                    dist = initAnimTargetPos - transform.position;
                    if (dist.magnitude < followSpeed * Time.deltaTime)
                    {
                        initAnimTargetReached = true;
                        initAnimWaitStartTime = Time.time;
                        transform.position = new Vector3(initAnimTargetPos.x, initAnimTargetPos.y, -6);
                    }
                    else
                    {
                        speed = initAnimWalkSpeed;
                        transform.position += (Vector3)dist.normalized * (initAnimWalkSpeed * Time.deltaTime);
                    }
                }
                else if(Time.time - initAnimWaitStartTime >= initAnimWaitTime)
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

                if (dist.magnitude >= 0.1)
                {
                    if (dist.x > 0)
                    {
                        if (facingLeft)
                        {
                            transform.rotation = Quaternion.Euler(0, 90, 0);
                            facingLeft = false;
                        }
                    }
                    else
                    {
                        if (!facingLeft)
                        {
                            transform.rotation = Quaternion.Euler(0, -90, 0);
                            facingLeft = true;
                        }
                    }

                    if (dist.magnitude <= followSpeed * Time.deltaTime)
                    {
                        speed = dist.magnitude / Time.deltaTime;
                        transform.position += (Vector3)dist;
                    }
                    else
                    {
                        speed = followSpeed * (dist.magnitude / followDistance);
                        transform.position += (Vector3)dist.normalized * (speed * Time.deltaTime);
                    }
                }
            }
            anim.SetFloat("speed", speed);
        }
    }
}
