using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndSceneGoToPlayer : MonoBehaviour
{
    public GameObject Player;
    public float moveSpeed = 30;
    public float horizontalDrag = .3f;
    public float verticalDrag = .1f;

    Rigidbody2D rigidBody;

    void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        //Move toward player
        Vector2 dist = Player.transform.position - transform.position;
        if (dist.x > 0)
        {
            rigidBody.AddForce(new Vector2(moveSpeed, 0));
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
        else if (dist.x < 0)
        {
            rigidBody.AddForce(new Vector2(-moveSpeed, 0));
            transform.rotation = Quaternion.Euler(0, 180, 0);
        }

        //Apply drag force (implemented manually to allow for different amounts of drag in different axes)
        Vector2 dragForce = rigidBody.velocity * abs(rigidBody.velocity) * -new Vector2(horizontalDrag, verticalDrag);
        rigidBody.AddForce(dragForce);
    }

    private Vector2 abs(Vector2 v)
    {
        return new Vector2(System.Math.Abs(v.x), System.Math.Abs(v.y));
    }
}
