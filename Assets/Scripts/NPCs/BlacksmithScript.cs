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
                //Debug.Log("blacksmith quest dialog");
                UIManager.Instance.CreateQuestDialog("Blacksmith:", "Collect Legendary Weapon", "Go get yourself a new weapon.", GetComponent<SpriteRenderer>().sprite);
            }
        }
    }
}
