using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : MonoBehaviour
{
    GameObject player;
    float timer = 0f;

    [SerializeField]
    GameObject explosion;

	// Use this for initialization
	void Start ()
    {
        player = GameManager.Instance.Player;
	}
	
	// Update is called once per frame
	void Update ()
    {
        transform.position = player.transform.position;

        timer += Time.deltaTime;
        if (timer > 10f)
        {
            Destroy(gameObject);
        }
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            Instantiate(explosion, collision.gameObject.transform.position, Quaternion.identity);
            Destroy(collision.gameObject);
        }
    }
}
