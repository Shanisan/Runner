using System;
using UnityEngine;

public abstract class Resettable : MonoBehaviour
{
    protected virtual void OnEnable()
    {
        InputManager.OnRestartPressed += Reset;
    }
    protected virtual void OnDisable()
    {
        InputManager.OnRestartPressed -= Reset;
    }

    protected abstract void Reset();
}