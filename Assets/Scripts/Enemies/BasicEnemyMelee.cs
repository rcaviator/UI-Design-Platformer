using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicEnemyMelee : MonoBehaviour
{
    [SerializeField]
    int health;
    [SerializeField]
    GameObject explosion;
    [SerializeField]
    GameObject healthPotion;

	// Use this for initialization
	void Start ()
    {
        
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (Vector3.Distance(transform.position, GameManager.Instance.Player.transform.position) < 3f)
        {
            transform.position = Vector2.MoveTowards(transform.position, GameManager.Instance.Player.transform.position, 1f * Time.deltaTime);
        }

        if (health < 1)
        {
            GameManager.Instance.Score += 100;

            Instantiate(healthPotion, transform.position, Quaternion.identity);

            Destroy(gameObject);
        }
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "PlayerAttack")
        {
            health--;
        }
        else if (collision.gameObject.tag == "Player")
        {
            GameManager.Instance.Player.GetComponent<Player>().PlayerHealth -= 10f;
            Instantiate(explosion, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
}
