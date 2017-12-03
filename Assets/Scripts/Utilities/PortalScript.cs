using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalScript : MonoBehaviour
{
    [SerializeField]
    Scenes destinationScene;

    bool useUIButtonIcon = false;
    bool showButton = false;

	// Use this for initialization
	void Awake ()
    {
		
	}
	

    public void Initialize(Scenes destinationScene, bool buttonIcon)
    {
        this.destinationScene = destinationScene;
        useUIButtonIcon = buttonIcon;
    }

	// Update is called once per frame
	void Update ()
    {
        if (useUIButtonIcon)
        {
            if (showButton)
            {
                //show ui button to enter
            }
        }
	}

    void UsePortal()
    {
        AudioManager.Instance.PlayGamePlaySoundEffect(GamePlaySoundEffect.Teleport);
        MySceneManager.Instance.ChangeScene(destinationScene);
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            if (useUIButtonIcon)
            {
                showButton = true;
            }

            if (InputManager.Instance.GetAxisRaw(PlayerAction.MoveVertical) > 0)
            {
                UsePortal();
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            if (useUIButtonIcon)
            {
                showButton = false;
            }
        }
    }

    //private void OnTriggerEnter2D(Collider2D collision)
    //{
    //    Debug.Log("in enter");
    //}
}
