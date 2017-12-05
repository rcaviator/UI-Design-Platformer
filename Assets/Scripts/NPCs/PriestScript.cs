using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PriestScript : MonoBehaviour
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
            if (!GameManager.Instance.Paused)
            {
                if (InputManager.Instance.GetButtonDown(PlayerAction.Interact))
                {
                    //Debug.Log("priest quest dialog");
                    UIManager.Instance.CreateQuestDialog("Priest:", "Fetch Lost Relic", "Go get me necklace", GetComponent<SpriteRenderer>().sprite);
                }
            }
        }
    }
}
