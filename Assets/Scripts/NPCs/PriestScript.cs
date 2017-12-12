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
                    UIManager.Instance.CreateQuestDialog("Priest:", "Fetch Lost Relic", "I haven't seen this kind of demonic power before! Legends say there is a holy necklace in the desert that should help us resist them. Go get it and bring it back here. We'll see what we can do with it.", GetComponent<SpriteRenderer>().sprite);
                }
            }
        }
    }
}
