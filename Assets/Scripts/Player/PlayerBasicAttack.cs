using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBasicAttack : Projectile
{
	// Use this for initialization
	protected override void Awake()
    {
        base.Awake();

        AudioManager.Instance.PlayGamePlaySoundEffect(GamePlaySoundEffect.PlayerAttack);
	}

    /// <summary>
    /// Initializes the player basic attack projectile
    /// </summary>
    /// <param name="bulletVelocity">the velocity vector</param>
    /// <param name="bulletTime">the bullet lifetime</param>
    public void InitializePlayerBasicProjectile(Vector2 bulletVelocity, float bulletTime)
    {
        Initialize(bulletVelocity, bulletTime);
    }
	
	// Update is called once per frame
	protected override void Update ()
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

        if (collision.gameObject.tag == "Enemy")
        {
            Instantiate(explosion, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
}
