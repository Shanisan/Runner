using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        other.TryGetComponent(out Obstacle obstacle);
        obstacle.GetDestroyed();
    }
}
