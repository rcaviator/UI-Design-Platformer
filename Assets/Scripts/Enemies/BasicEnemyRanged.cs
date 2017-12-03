using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BasicEnemyRanged : Enemy
{
    //item references
    GameObject explosion;
    GameObject healthPotion;
    GameObject enemyProjectile;

    //health image
    [SerializeField]
    Image healthBar;

    bool inRange = false;
    float timer = 0f;

    // Use this for initialization
    protected override void Awake()
    {
        base.Awake();

        health = Constants.BASIC_ENEMY_RANGED_HEALTH;

        explosion = Resources.Load<GameObject>("Prefabs/Explosion");
        healthPotion = Resources.Load<GameObject>("Prefabs/HealthPotionItem");
        enemyProjectile = Resources.Load<GameObject>("Prefabs/EnemyRangedAttackProjectile");
    }

    // Update is called once per frame
    protected override void Update()
    {
        if (!GameManager.Instance.Paused)
        {
            base.Update();

            #region Movement

            //move if player is detected
            if (Vector3.Distance(transform.position, GameManager.Instance.Player.transform.position) < Constants.BASIC_ENEMY_DETECTION_DISTANCE)
            {
                //direction
                if (GameManager.Instance.Player.transform.position.x < transform.position.x)
                {
                    //move to the left if not in range
                    if (Vector3.Distance(transform.position, GameManager.Instance.Player.transform.position) > Constants.BASIC_ENEMY_RANGED_FIRING_DISTANCE)
                    {
                        facingLeft = false;
                        currentHorizontalSpeed = Constants.BASIC_ENEMY_MAX_HORIZONTAL_SPEED;//Mathf.Clamp(rBody.velocity.x - Constants.BASIC_ENEMY_HORIZONTAL_ACCELERATION * Time.deltaTime, -Constants.BASIC_ENEMY_HORIZONTAL_ACCELERATION, Constants.BASIC_ENEMY_MAX_HORIZONTAL_SPEED);
                        rBody.velocity = new Vector2(-currentHorizontalSpeed, rBody.velocity.y);
                        inRange = false;
                    }
                    else
                    {
                        currentHorizontalSpeed = 0f;
                        rBody.velocity = new Vector2(currentHorizontalSpeed, rBody.velocity.y);
                        inRange = true;
                    }
                }
                else
                {
                    //move to the right if not in range
                    if (Vector3.Distance(transform.position, GameManager.Instance.Player.transform.position) > Constants.BASIC_ENEMY_RANGED_FIRING_DISTANCE)
                    {
                        facingLeft = true;
                        currentHorizontalSpeed = Constants.BASIC_ENEMY_MAX_HORIZONTAL_SPEED;//Mathf.Clamp(rBody.velocity.x + Constants.BASIC_ENEMY_HORIZONTAL_ACCELERATION * Time.deltaTime, -Constants.BASIC_ENEMY_HORIZONTAL_ACCELERATION, Constants.BASIC_ENEMY_MAX_HORIZONTAL_SPEED);
                        rBody.velocity = new Vector2(currentHorizontalSpeed, rBody.velocity.y);
                        inRange = false;
                    }
                    else
                    {
                        currentHorizontalSpeed = 0f;
                        rBody.velocity = new Vector2(currentHorizontalSpeed, rBody.velocity.y);
                        inRange = true;
                    }
                }
            }
            else
            {
                currentHorizontalSpeed = 0f;
            }

            #endregion

            #region Fire Control

            //firing control
            if (inRange)
            {
                if (timer <= Constants.BASIC_ENEMY_RANGED_FIRING_TIMER)
                {
                    timer += Time.deltaTime;
                }
                else
                {
                    timer = 0f;
                    //fire projectile
                    Instantiate(enemyProjectile, transform.position, Quaternion.identity);
                }
            }

            #endregion

            #region Health Check

            healthBar.fillAmount = health / Constants.BASIC_ENEMY_RANGED_HEALTH;

            //die if health hits 0
            if (health < 1)
            {
                GameManager.Instance.Score += 100;

                Instantiate(healthPotion, transform.position, Quaternion.identity);
                Instantiate(explosion, transform.position, Quaternion.identity);
                switch (Random.Range(1, 3))
                {
                    //case 0:
                    //    AudioManager.Instance.PlayGamePlaySoundEffect(GamePlaySoundEffect.EnemyDeath);
                    //    break;
                    case 1:
                        AudioManager.Instance.PlayGamePlaySoundEffect(GamePlaySoundEffect.EnemyDeath2);
                        break;
                    case 2:
                        AudioManager.Instance.PlayGamePlaySoundEffect(GamePlaySoundEffect.EnemyDeath3);
                        break;
                    default:
                        break;
                }
                Destroy(gameObject);
            }

            #endregion
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
            GameManager.Instance.Player.GetComponent<Player>().PlayerHealth -= Constants.BASIC_ENEMY_RANGED_DAMAGE_TO_PLAYER;
            //Instantiate(explosion, transform.position, Quaternion.identity);
            AudioManager.Instance.PlayGamePlaySoundEffect(GamePlaySoundEffect.EnemyDeath);
            Destroy(gameObject);
        }
    }
}
