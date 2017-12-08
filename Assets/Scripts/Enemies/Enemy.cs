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

    protected Image healthBar;
    Sprite normalHealthBar;
    Sprite damagedHealthBar;
    bool flashHealthBar = false;
    float maxHealthBarFlash = 0.2f;
    float healthBarFlash = 0f;

    // Use this for initialization
    protected override void Awake ()
    {
        base.Awake();

        if (GetComponent<SpriteRenderer>())
        {
            sRender = GetComponent<SpriteRenderer>();
        }

        normalHealthBar = healthBar.sprite;
        damagedHealthBar = Resources.Load<Sprite>("Graphics/Environment/HealthBarDamagedSprite");

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

        //healthbar
        //flash health bar if damaged
        if (flashHealthBar)
        {
            healthBarFlash += Time.deltaTime;

            if (healthBarFlash <= maxHealthBarFlash)
            {
                //healthBar.GetComponent<Image>().color = Color.red;
                healthBar.sprite = damagedHealthBar;
            }
            else
            {
                //healthBar.GetComponent<Image>().color = Color.white;
                healthBar.sprite = normalHealthBar;
                healthBarFlash = 0f;
                flashHealthBar = false;
            }
        }
    }

    protected virtual void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "PlayerBullet")
        {
            health--;
            flashHealthBar = true;
        }
    }
}
