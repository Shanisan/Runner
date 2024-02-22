using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    protected bool isAlive = true;
    public static event Action<Obstacle> OnObstacleDestroyedEvent;
    public virtual void GetDestroyed()
    {
        InvokeObstacleDestroyedEvent(this);
        Destroy(gameObject);
    }

    protected void InvokeObstacleDestroyedEvent(Obstacle obstacle)
    {
        OnObstacleDestroyedEvent?.Invoke(obstacle);
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
