using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrabAnimationController : MonoBehaviour
{
    public void DestroyCrab()
    {
        Destroy(transform.parent.gameObject);
    }
}
