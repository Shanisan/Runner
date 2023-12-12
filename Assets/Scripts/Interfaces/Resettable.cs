using System;
using UnityEngine;

public abstract class Resettable : MonoBehaviour
{
    protected virtual void OnEnable()
    {
        GameManager.Instance.RestartGameEvent += Reset;
    }
    protected virtual void OnDisable()
    {
        GameManager.Instance.RestartGameEvent -= Reset;
    }

    protected abstract void Reset();
}