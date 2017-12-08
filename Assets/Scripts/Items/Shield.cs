using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//should be from pausable object, but theres a problem with spawning when paused
//problem is with rBody not being simulated when unpaused
public class Shield : MonoBehaviour
{
    Rigidbody2D rBody;
    GameObject player;
    float shieldDurationTimer = 0f;
    bool flashShield = false;
    bool changeColor = true;
    float maxFlashShieldTimer = 0.5f;
    float flashShieldTimer = 0f;

    GameObject explosion;

	// Use this for initialization
	void Awake()
    {
        rBody = GetComponent<Rigidbody2D>();
        rBody.freezeRotation = true;
        player = GameManager.Instance.Player;
        explosion = Resources.Load<GameObject>("Prefabs/Explosion");
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (!GameManager.Instance.Paused)
        {
            transform.position = player.transform.position;

            shieldDurationTimer += Time.deltaTime;

            //time until shield flashing
            if (shieldDurationTimer >= Constants.SHIELD_DURATION - 3f)
            {
                flashShield = true;
            }

            //alternate between colors when little time remains
            if (flashShield)
            {
                if (changeColor)
                {
                    flashShieldTimer += Time.deltaTime;
                    GetComponent<SpriteRenderer>().color = Color.red;

                    if (flashShieldTimer >= maxFlashShieldTimer)
                    {
                        flashShieldTimer = 0f;
                        changeColor = false;
                    }
                }
                else
                {
                    flashShieldTimer += Time.deltaTime;
                    GetComponent<SpriteRenderer>().color = Color.white;

                    if (flashShieldTimer >= maxFlashShieldTimer)
                    {
                        flashShieldTimer = 0f;
                        changeColor = true;
                    }
                }
            }

            //destroy shield when time expires
            if (shieldDurationTimer > Constants.SHIELD_DURATION)
            {
                Destroy(gameObject);
            }
        }
	}

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy" || collision.gameObject.tag == "EnemyBullet")
        {
            Instantiate(explosion, collision.gameObject.transform.position, Quaternion.identity);
            Destroy(collision.gameObject);
        }
    }
}
