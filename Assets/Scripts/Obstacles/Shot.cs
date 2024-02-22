using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shot : Obstacle
{
    public Rigidbody2D rb;
    public float bulletVelocity = -2f;
    void FixedUpdate()
    {
        var force = new Vector2(bulletVelocity, 0);
        rb.velocity=force;
    }

    public override void GetDestroyed()
    {
        //you can't deflect bullets with your sword, silly...
    }
}
