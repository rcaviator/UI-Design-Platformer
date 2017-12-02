using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectile : Projectile
{   

	// Use this for initialization
	protected override void Awake()
    {
        base.Awake();

        //create new vectory with velocity to the player
        Vector2 velocity = GameManager.Instance.Player.transform.position - transform.position;
        velocity.Normalize();
        velocity *= Constants.BASIC_ENEMY_PROJECTILE_SPEED;
        Initialize(velocity, Constants.BASIC_ENEMY_PROJECTILE_LIFETIME);
        float angle = Mathf.Atan2(velocity.y, velocity.x) * Mathf.Rad2Deg + 90f;
        Quaternion q = Quaternion.AngleAxis(angle, Vector3.forward);
        transform.rotation = q;
        AudioManager.Instance.PlayGamePlaySoundEffect(GamePlaySoundEffect.EnemyProjectile);
    }
	
	// Update is called once per frame
	protected override void Update()
    {
        base.Update();

	}

    /// <summary>
    /// Unity on collision enter 2d event
    /// </summary>
    /// <param name="collision">unity collision object</param>
    protected override void OnCollisionEnter2D(Collision2D collision)
    {
        base.OnCollisionEnter2D(collision);

        if (collision.gameObject.tag == "Player")
        {
            Instantiate(explosion, transform.position, Quaternion.identity);
            GameManager.Instance.Player.GetComponent<Player>().PlayerHealth -= Constants.BASIC_ENEMY_PROJECTILE_DAMAGE_TO_PLAYER;
            Destroy(gameObject);
        }
    }
}
