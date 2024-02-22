using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Tilemaps;

public class GroundExtender : Resettable
{
    [SerializeField] private CharacterActions player;
    private Camera cam;
    private bool resetting = false;

    private float groundSegmentWidth;

    [SerializeField] private List<GameObject> groundSegments = new List<GameObject>();
    private List<Vector3> groundSegmentDefaultPositions = new List<Vector3>();
    private void Awake()
    {
        foreach (var segment in groundSegments)
        {
            //groundSegments.Add(segment.gameObject);
            groundSegmentDefaultPositions.Add(segment.transform.position);
        }
        groundSegments.Sort(GroundSegmentSorter);
        cam = Camera.main;
        groundSegmentWidth = groundSegments[0].GetComponent<SpriteRenderer>().bounds.size.x;
    }

    private int GroundSegmentSorter(GameObject a, GameObject b)
    {
        return (int)a.transform.position.x - (int)b.transform.position.x ;
    }

    private void Update()
    {
        if (player.IsAlive && !resetting)
        {
            var dist = (transform.position - cam.transform.position).z;
            var leftBorder = cam.ViewportToWorldPoint(new Vector3(0, 0, dist)).x;
            var rightBorder = cam.ViewportToWorldPoint(new Vector3(1, 0, dist)).x;

            ExtendGround(rightBorder);
        }
    }

    private void ExtendGround(float rightBorder)
    {
        Transform lastGroundSegment = groundSegments.Last().transform;

        if (lastGroundSegment.transform.position.x + groundSegmentWidth / 2 < rightBorder)
        {
            groundSegments[0].transform.position = new Vector3(lastGroundSegment.position.x + groundSegmentWidth, -3.61f, 0f);
            groundSegments.Sort(GroundSegmentSorter);
        }
    }
    
    protected override void Reset()
    {
        for (int i = 0; i < groundSegments.Count; i++)
        {
            groundSegments[i].transform.position = groundSegmentDefaultPositions[i];
        }
        groundSegments.Sort(GroundSegmentSorter);
    }

}