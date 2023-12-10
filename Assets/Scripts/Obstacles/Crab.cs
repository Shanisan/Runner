using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crab : Obstacle
{
    private bool isAlive = true;
    private static readonly int DIE_TRIGGER = Animator.StringToHash("DieTrigger");

    public override void GetDestroyed()
    {
        InvokeObstacleDestroyedEvent(this);
        isAlive = false;
        GetComponentInChildren<Animator>().SetTrigger(DIE_TRIGGER);
        GetComponent<Collider2D>().excludeLayers=LayerMask.GetMask("Player");
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (isAlive && other.gameObject.CompareTag("Player"))
        {
            other.gameObject.TryGetComponent(out CharacterActions player);
            player.IsAlive=false;
        }
    }
}
