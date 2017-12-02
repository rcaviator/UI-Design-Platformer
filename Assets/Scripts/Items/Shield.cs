using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//should be from pausable object, but theres a problem with spawning when paused
//problem is with rBody not being simulated when unpaused
public class Shield : MonoBehaviour
{
    Rigidbody2D rBody;
    GameObject player;
    float timer = 0f;

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

            timer += Time.deltaTime;
            if (timer > 10f)
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
