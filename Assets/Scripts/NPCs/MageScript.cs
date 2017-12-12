using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MageScript : MonoBehaviour
{

    // Use this for initialization
    void Awake()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if (InputManager.Instance.GetButtonDown(PlayerAction.Interact))
            {
                UIManager.Instance.CreateQuestDialog("Mage:", "Deliver Magical Ward", "Theres been reports of deamons taking over the mountain and causing lots of trouble for the mountain people. I have a magical ward that should stop them. Can you go place it for me?", GetComponent<SpriteRenderer>().sprite);
            }
        }
    }
}
