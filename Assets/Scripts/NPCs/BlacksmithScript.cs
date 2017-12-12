using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlacksmithScript : MonoBehaviour
{

	// Use this for initialization
	void Awake()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if (InputManager.Instance.GetButtonDown(PlayerAction.Interact))
            {
                UIManager.Instance.CreateQuestDialog("Blacksmith:", "Collect Legendary Weapon", "I've heard of a weapon that allows you to shoot multiple fireballs at once! You should go get it! Use this portal. It will take to you where it was last seen. Be careful!", GetComponent<SpriteRenderer>().sprite);
            }
        }
    }
}
