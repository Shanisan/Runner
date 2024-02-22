using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crab : Obstacle
{
    private static readonly int DIE_TRIGGER = Animator.StringToHash("DieTrigger");

    public override void GetDestroyed()
    {
        InvokeObstacleDestroyedEvent(this);
        isAlive = false;
        GetComponentInChildren<Animator>().SetTrigger(DIE_TRIGGER);
        GetComponent<Collider2D>().enabled = false;
        GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
    }
    
}
