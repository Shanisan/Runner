using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Feet : MonoBehaviour
{
    [SerializeField] private LayerMask whatIsGround;
    private Collider2D feetCollider;
    public bool Grounded
    {
        get
        {
            List<Collider2D> overlaps = new List<Collider2D>();
            ContactFilter2D filter = new ContactFilter2D();
            filter.layerMask = whatIsGround;
            feetCollider.OverlapCollider(filter, overlaps);
            return overlaps.Count > 1;
        }
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
        
    }
}
