using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

public class ObstacleSpawner : Resettable
{
    [SerializeField] private List<GameObject> obstacleTypes;
    [SerializeField] private Camera cam;
    [SerializeField] private CharacterActions player;
    [SerializeField] private Transform groundHeight;
    [SerializeField] private float obstacleOffset = 5;
    [SerializeField] private float defaultGracePeriod = 1;

    private List<Obstacle> placedObstacles = new List<Obstacle>();
    private Vector3 lastDestroyedObstaclePosition = Vector3.zero;

    private int obstacleCounter = 0;
    private float gracePeriod;
    private bool reset = false;
    protected void OnEnable()
    {
        gracePeriod = defaultGracePeriod;
        base.OnEnable();
        Obstacle.OnObstacleDestroyedEvent += RemoveObstacleFromList;
    }

    protected void OnDisable()
    {
        base.OnDisable();
        Obstacle.OnObstacleDestroyedEvent -= RemoveObstacleFromList;
    }

    private void RemoveObstacleFromList(Obstacle obstacle)
    {
        Debug.Log($"Removing {obstacle} from the list.");
        lastDestroyedObstaclePosition = obstacle.transform.position;
        placedObstacles.Remove(obstacle);
    }

    private void Update()
    {
        if (gracePeriod >= 0)
        {
            gracePeriod -= Time.deltaTime;
        }
        else if (player.IsAlive)
        {
            var dist = (transform.position - cam.transform.position).z;
            var leftBorder = cam.ViewportToWorldPoint(new Vector3(0, 0, dist)).x;
            var rightBorder = cam.ViewportToWorldPoint(new Vector3(1, 0, dist)).x;

            if (placedObstacles.Count > 0)
            {
                //delete the first item in the list if it is left of the left border, aka not in view anymore.
                var obj = placedObstacles.First();
                if (obj.transform.position.x < leftBorder-1)
                {
                    Destroy(obj.gameObject);
                    RemoveObstacleFromList(obj);
                }
            }

            if(placedObstacles.Count > 0){
            //if the last item in the list is left of the right border, aka in view, check if we can place a new obstacle and do so
                var obj = placedObstacles.Last();
                if (rightBorder - obj.transform.position.x > obstacleOffset)
                {
                    SpawnObstacle(new Vector3(rightBorder+1,groundHeight.position.y, 0));
                }
            }
            else if(lastDestroyedObstaclePosition==Vector3.zero || rightBorder - lastDestroyedObstaclePosition.x > obstacleOffset)
            {
                SpawnObstacle(new Vector3(rightBorder+1,groundHeight.position.y, 0));
            }
        }
        
    }

    private void SpawnObstacle(Vector3 placement)
    {
        int obstacleType = Random.Range(0, obstacleTypes.Count);
        var obstacle = Instantiate(obstacleTypes[obstacleType], placement, Quaternion.identity, transform);
        obstacle.name = obstacleTypes[obstacleType].name + $" #{obstacleCounter++}";
        placedObstacles.Add(obstacle.GetComponent<Obstacle>());
    }

    protected override void Reset()
    {
        foreach (var x in placedObstacles) Destroy(x.gameObject);
        placedObstacles = new List<Obstacle>();
        lastDestroyedObstaclePosition = Vector3.zero;
        obstacleCounter = 0;
        gracePeriod = defaultGracePeriod;
    }
}
