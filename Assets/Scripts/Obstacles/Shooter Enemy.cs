using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShooterEnemy : Obstacle
{
    [SerializeField] private float shootingFrequency = 2f;
    [SerializeField] private float shootingDistance = 5f; //the shooter will not shoot if the player is closer than this threshold
    [SerializeField] private Transform gunTip;
    [SerializeField] private GameObject bulletPrefab;

    private static readonly int SHOOT_TRIGGER = Animator.StringToHash("shoot_trigger");
    private static readonly int DIE_TRIGGER = Animator.StringToHash("die_trigger");

    
    private float shootingCooldown = 0;

    private void Awake()
    {
        var rotation = transform.rotation;
        rotation=Quaternion.Euler(rotation.x, 180, rotation.z);
        transform.rotation = rotation;
    }

    void Update()
    {
        if (shootingCooldown <= 0)
        {
            if (Vector2.Distance(transform.position, GameManager.Instance.Player.transform.position) > shootingDistance)
            {
                Shoot();
                shootingCooldown = shootingFrequency;
            }
        }
        else
        {
            shootingCooldown -= Time.deltaTime;
        }
    }

    private void Shoot()
    {
        GetComponent<Animator>().SetTrigger(SHOOT_TRIGGER);
    }

    private void ReleaseBullet()
    {
        Instantiate(bulletPrefab, gunTip.position, Quaternion.identity, transform);
    }
    
    public override void GetDestroyed()
    {
        InvokeObstacleDestroyedEvent(this);
        isAlive = false;
        GetComponent<Animator>().SetTrigger(DIE_TRIGGER);
        GetComponent<Collider2D>().enabled = false;
        GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
    }

    public void DestroyEnemy()
    {
        Destroy(gameObject);
    }
}
