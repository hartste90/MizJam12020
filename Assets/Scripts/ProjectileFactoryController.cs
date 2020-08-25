using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileFactoryController : Activatable
{
    public Projectile projectilePrefab;

    public float interval = 2f;

    public float initialDelay = 0f;

    public int numProjectilesCreated = 1;

    public Vector2 initialVelocity;
    

    private float lastTimeFired;
    private bool isActive = false;

    public override void Activate()
    {
        isActive = true;
        lastTimeFired = Time.time - interval + initialDelay;
    }

    public override void Deactivate()
    {
        isActive = false;
    }
    

    void Update()
    {
        if (isActive)
        {
            if (Time.time - lastTimeFired >= interval)
            {
                lastTimeFired = Time.time;
                CreateProjectile();
            }
        }
    }

    private void CreateProjectile()
    {
        for(int i = 0; i < numProjectilesCreated; i++)
        {
            Projectile projectile = Instantiate<Projectile>(projectilePrefab, null);
            projectile.transform.position = transform.position;
            projectile.GetComponentInChildren<Rigidbody2D>().velocity = initialVelocity;
        }
    }
}


