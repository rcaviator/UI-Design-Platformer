using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : PauseableObject
{
    //set drops in child enemies

    //health
    protected float health;

    //speed
    protected float currentHorizontalSpeed = 0f;

    //sprite direction
    protected bool facingLeft = true;

    //sprite renderer
    SpriteRenderer sRender;

    // Use this for initialization
    protected override void Awake ()
    {
        base.Awake();

        if (GetComponent<SpriteRenderer>())
        {
            sRender = GetComponent<SpriteRenderer>();
        }

        rBody.freezeRotation = true;
	}
	
	// Update is called once per frame
	protected virtual void Update ()
    {
        //left vs right facing
        if (!facingLeft)
        {
            sRender.flipX = false;
        }
        else
        {
            sRender.flipX = true;
        }
    }

    protected virtual void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "PlayerBullet")
        {
            health--;
        }
    }
}
