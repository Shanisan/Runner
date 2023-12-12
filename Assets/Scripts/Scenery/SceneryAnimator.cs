using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SceneryAnimator : Resettable
{
    [SerializeField] private Transform backgroundContainer, middlegroundContainer;
    [SerializeField] private GameObject backgroundPrefab, middlegroundPrefab;
    [SerializeField, Range(0f, 5f)] private float backgroundSpeed, middlegroundSpeed;
    [SerializeField] private CharacterActions player;
    private Camera cam;
    private Vector3 bgDefaultPos, mgDefaultPos;

    float backgroundWidth, middlegroundWidth;
    private void Awake()
    {
        cam=Camera.main;
        backgroundWidth = backgroundPrefab.GetComponent<SpriteRenderer>().bounds.size.x;
        middlegroundWidth = middlegroundPrefab.GetComponent<SpriteRenderer>().bounds.size.x;
        bgDefaultPos = backgroundContainer.position;
        mgDefaultPos = middlegroundContainer.position;
    }

    private bool resetting = false;
    private void Update()
    {
        if (GameManager.Instance.isGameStarted && player.IsAlive && !resetting)
        {
            var dist = (transform.position - cam.transform.position).z;
            var leftBorder = cam.ViewportToWorldPoint(new Vector3(0, 0, dist)).x;
            var rightBorder = cam.ViewportToWorldPoint(new Vector3(1, 0, dist)).x;

            ParallaxScenery();
            ExtendScenery(rightBorder);
            CleanupPastScenery(leftBorder);
        }
    }

    private void ParallaxScenery()
    {
        backgroundContainer.position += new Vector3(backgroundSpeed * Time.deltaTime * -1, 0, 0);
        middlegroundContainer.position += new Vector3(middlegroundSpeed * Time.deltaTime * -1, 0, 0);
    }

    private void ExtendScenery(float rightBorder)
    {
        Transform lastBackground = backgroundContainer.GetChild(backgroundContainer.childCount - 1);
        Transform lastMiddleGround = middlegroundContainer.GetChild(middlegroundContainer.childCount - 1);

        if (lastBackground.position.x - backgroundWidth / 2 < rightBorder)
        {
            MakeNewBackground(lastBackground.position.x + backgroundWidth);
        }

        if (lastMiddleGround.position.x - middlegroundWidth / 2 < rightBorder)
        {
            MakeNewMiddleground(lastMiddleGround.position.x + middlegroundWidth);
        }
    }

    private void CleanupPastScenery(float leftBorder)
    {
        Transform firstBackground = backgroundContainer.GetChild(0);
        Transform firstMiddleGround = middlegroundContainer.GetChild(0);
        if (firstBackground.position.x + backgroundWidth / 2 < leftBorder)
        {
            Destroy(firstBackground.gameObject);
        }

        if (firstMiddleGround.position.x + middlegroundWidth / 2 < leftBorder)
        {
            Destroy(firstMiddleGround.gameObject);
        }
    }

    protected override void Reset()
    {
        StartCoroutine(ResetCoroutine());
    }

    private IEnumerator ResetCoroutine()
    {
        resetting = true;
        foreach (Transform child in backgroundContainer)
        {
            Destroy(child.gameObject);
        }

        foreach (Transform child in middlegroundContainer)
        {
            Destroy(child.gameObject);
        }

        backgroundContainer.position = bgDefaultPos;
        middlegroundContainer.position = mgDefaultPos;
        yield return null;
        
        MakeNewBackground(-backgroundWidth);
        MakeNewBackground(0);
        MakeNewBackground(backgroundWidth);
        MakeNewMiddleground(-middlegroundWidth);
        MakeNewMiddleground(0);
        MakeNewMiddleground(middlegroundWidth);

        yield return null;
        resetting = false;
    }

    private void MakeNewMiddleground(float positionX)
    {
        Instantiate(middlegroundPrefab, new Vector3(positionX, 0, 5), Quaternion.identity, middlegroundContainer);
    }

    private void MakeNewBackground(float positionX)
    {
        Instantiate(backgroundPrefab, new Vector3(positionX, 0, 10), Quaternion.identity, backgroundContainer);
    }
}
