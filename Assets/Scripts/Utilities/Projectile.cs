using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class Projectile : PauseableObject
{
    //ownership is tags and layers

    float bulletLifetime = 0;
    float startTime = 0;

    protected override void Awake()
    {
        base.Awake();
        //extra
    }

    protected virtual void Update()
    {
        //bullet lifetime
        if (!GameManager.Instance.Paused)
        {
            startTime += Time.deltaTime;
            if (startTime >= bulletLifetime)
            {
                Destroy(gameObject);
            }
        }
    }

    /// <summary>
    /// Sets up the basic properties of the projectile
    /// </summary>
    /// <param name="bulletVelocity">the velocity vector</param>
    /// <param name="bulletTime">the bullet lifetime</param>
    protected void Initialize(Vector2 bulletVelocity, float bulletTime)
    {
        rBody.velocity = bulletVelocity;
        bulletLifetime = bulletTime;
    }

    protected virtual void OnCollisionEnter2D(Collision2D collision)
    {
        //put collision logic on each bullet

        //if (collision.gameObject.tag != "Player")
        //{
        //    Instantiate(Resources.Load<GameObject>("Prefabs/Explosion"), transform.position, Quaternion.identity);
        //    //Destroy(gameObject);
        //}

        if (collision.gameObject.tag == "Ground")
        {
            Instantiate(Resources.Load<GameObject>("Prefabs/Explosion"), transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
}