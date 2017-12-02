using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class Projectile : PauseableObject
{
    //ownership is tags and layers

    protected GameObject explosion;

    float bulletLifetime = 0;
    float startTime = 0;

    protected override void Awake()
    {
        base.Awake();
        //extra
        explosion = Resources.Load<GameObject>("Prefabs/Explosion");
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

        //all bullets will die on ground collision
        if (collision.gameObject.tag == "Ground")
        {
            Instantiate(explosion, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
}