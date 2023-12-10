using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
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
}
