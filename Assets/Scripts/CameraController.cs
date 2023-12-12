using System.Collections;
using Cinemachine;
using UnityEngine;

public class CameraController : Resettable
{
    [SerializeField]private CinemachineVirtualCamera cineCam;
    private Vector3 defaultPosition;

    private void Awake()
    {
        defaultPosition = transform.position;
    }


    protected override void Reset()
    {
        StartCoroutine(ResetCoroutine());
    }

    private IEnumerator ResetCoroutine()
    {
        cineCam.enabled = false;
        transform.position = defaultPosition;
        yield return null;
        cineCam.enabled = true;
    }
}
