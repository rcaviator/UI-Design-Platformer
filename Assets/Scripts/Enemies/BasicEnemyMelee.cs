using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicEnemyMelee : Enemy
{
    //item references
    GameObject explosion;
    GameObject healthPotion;

	// Use this for initialization
	protected override void Awake ()
    {
        base.Awake();

        health = Constants.BASIC_ENEMY_MELEE_HEALTH;

        explosion = Resources.Load<GameObject>("Prefabs/Explosion");
        healthPotion = Resources.Load<GameObject>("Prefabs/HealthPotionItem");
	}
	
	// Update is called once per frame
	protected override void Update ()
    {
        if (!GameManager.Instance.Paused)
        {
            base.Update();

            //move it player is detected
            if (Vector3.Distance(transform.position, GameManager.Instance.Player.transform.position) < Constants.BASIC_ENEMY_DETECTION_DISTANCE)
            {
                //transform.position = Vector2.MoveTowards(transform.position, GameManager.Instance.Player.transform.position, 1f * Time.deltaTime);

                //direction
                if (GameManager.Instance.Player.transform.position.x < transform.position.x)
                {
                    //move to the left
                    facingLeft = false;
                    currentHorizontalSpeed = Mathf.Clamp(rBody.velocity.x - Constants.BASIC_ENEMY_HORIZONTAL_ACCELERATION * Time.deltaTime, -Constants.BASIC_ENEMY_HORIZONTAL_ACCELERATION, Constants.BASIC_ENEMY_MAX_HORIZONTAL_SPEED);
                    rBody.velocity = new Vector2(currentHorizontalSpeed, rBody.velocity.y);
                }
                else
                {
                    //move to the right
                    facingLeft = true;
                    currentHorizontalSpeed = Mathf.Clamp(rBody.velocity.x + Constants.BASIC_ENEMY_HORIZONTAL_ACCELERATION * Time.deltaTime, -Constants.BASIC_ENEMY_HORIZONTAL_ACCELERATION, Constants.BASIC_ENEMY_MAX_HORIZONTAL_SPEED);
                    rBody.velocity = new Vector2(currentHorizontalSpeed, rBody.velocity.y);
                }
            }
            else
            {
                currentHorizontalSpeed = 0f;
            }

            //die if health hits 0
            if (health < 1)
            {
                GameManager.Instance.Score += 100;

                Instantiate(healthPotion, transform.position, Quaternion.identity);
                Instantiate(explosion, transform.position, Quaternion.identity);
                Destroy(gameObject);
            }
        }
	}

    /// <summary>
    /// Unity collision method
    /// </summary>
    /// <param name="collision">collision object</param>
    protected override void OnCollisionEnter2D(Collision2D collision)
    {
        base.OnCollisionEnter2D(collision);

        if (collision.gameObject.tag == "Player")
        {
            GameManager.Instance.Player.GetComponent<Player>().PlayerHealth -= Constants.BASIC_ENEMY_MELEE_DAMAGE_TO_PLAYER;
            Instantiate(explosion, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
}
