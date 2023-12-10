using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Feet : MonoBehaviour
{
    [SerializeField] private LayerMask whatIsGround;
    private Collider2D feetCollider;
    private bool? _grounded = null;
    public bool Grounded
    {
        get
        {
            if (_grounded == null)
            {
                List<Collider2D> overlaps = new List<Collider2D>();
                ContactFilter2D filter = new ContactFilter2D();
                filter.layerMask = whatIsGround;
                feetCollider.OverlapCollider(filter, overlaps);
                return overlaps.Count > 0;
            }
            else
            {
                return (bool)_grounded;
            }
        }
        private set => _grounded = value;
    }

    private void Awake()
    {
        feetCollider = GetComponent<Collider2D>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        List<Collider2D> overlaps = new List<Collider2D>();
        GetComponent<Collider2D>().OverlapCollider(new ContactFilter2D(), overlaps);
        Debug.Log("Overlaps are: "+ string.Join(", ", overlaps));
        if (other.gameObject.CompareTag("Ground"))
        {
            Grounded = true;
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            Grounded = false;
        }
    }
}
