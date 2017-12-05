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
                //Debug.Log("mage quest dialog");
                UIManager.Instance.CreateQuestDialog("Mage:", "Deliver Magical Ward", "Go place this magical ward.", GetComponent<SpriteRenderer>().sprite);
            }
        }
    }
}
