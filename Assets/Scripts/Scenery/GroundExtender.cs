using System;
using UnityEngine;
using UnityEngine.Tilemaps;

public class GroundExtender : Resettable
{
    [SerializeField] private GameObject groundPrefab;
    [SerializeField] private CharacterActions player;
    private Camera cam;
    private bool resetting = false;

    private float groundSegmentWidth;
    private void Awake()
    {
        cam = Camera.main;
        groundSegmentWidth = groundPrefab.GetComponent<SpriteRenderer>().bounds.size.x;
    }

    private void Update()
    {
        if (player.IsAlive && !resetting)
        {
            var dist = (transform.position - cam.transform.position).z;
            var leftBorder = cam.ViewportToWorldPoint(new Vector3(0, 0, dist)).x;
            var rightBorder = cam.ViewportToWorldPoint(new Vector3(1, 0, dist)).x;

            ExtendGround(rightBorder);
            CleanupPastScenery(leftBorder);
        }
    }

    private void ExtendGround(float rightBorder)
    {
        Transform lastGroundSegment = transform.GetChild(transform.childCount - 1);

        if (lastGroundSegment.transform.position.x - groundSegmentWidth / 2 < rightBorder)
        {
            MakeNewGroundSegment(lastGroundSegment.position.x + groundSegmentWidth);
        }
    }

    private void CleanupPastScenery(float leftBorder)
    {
        Transform firstGroundSegment = transform.GetChild(0);
        

        if (firstGroundSegment.position.x + groundSegmentWidth / 2 < leftBorder)
        {
            Destroy(firstGroundSegment.gameObject);
        }
    }
    
    
    private void MakeNewGroundSegment(float positionX)
    {
        Instantiate(groundPrefab, new Vector3(positionX, -3.61f, 0f), Quaternion.identity, transform);
    }
    
    protected override void Reset()
    {
        //throw new System.NotImplementedException();
    }
}