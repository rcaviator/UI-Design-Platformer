using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UICanvas : MonoBehaviour
{
    //references
    Text locationText;
    Text questText;
    Button viewQuestsButton;
    Text controllsText;
    Text scoreText;

	// Use this for initialization
	void Awake()
    {
        //set references
        locationText = transform.GetChild(0).transform.GetChild(0).GetComponent<Text>();
        questText = transform.GetChild(1).transform.GetChild(0).GetComponent<Text>();
        viewQuestsButton = transform.GetChild(1).transform.GetChild(1).GetComponent<Button>();
        controllsText = transform.GetChild(2).transform.GetChild(0).GetComponent<Text>();

        //set text
        locationText.text = "Location: " + SceneManager.GetActiveScene().name;

        controllsText.text = "Pause Menu: " + InputManager.Instance.GetCurrentSettingsDetails(PlayerAction.PauseGame)
            + ". Inventory: " + InputManager.Instance.GetCurrentSettingsDetails(PlayerAction.ViewInventory)
            + ". Shoot: " + InputManager.Instance.GetCurrentSettingsDetails(PlayerAction.FirePrimary)
            + ". Enter Portal: " + InputManager.Instance.GetCurrentSettingsDetails(PlayerAction.MoveVertical);
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    public void OnViewQuestsClick()
    {
        UIManager.Instance.CreateCharacterMenu();
    }
}
