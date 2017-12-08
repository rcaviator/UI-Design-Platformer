using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ControllerSchemeDetailsScript : MonoBehaviour
{
    Text detailsText;

	// Use this for initialization
	void Start ()
    {
        detailsText = GetComponent<Text>();
        ChangeText();
	}
	
	// Update is called once per frame
	void Update ()
    {
        
	}

    public void ChangeText()
    {
        //build the string
        detailsText.text = InputManager.Instance.GetCurrentSettingsDetails(PlayerAction.PauseGame) + "\n" +
            InputManager.Instance.GetCurrentSettingsDetails(PlayerAction.ViewInventory) + "\n" +
            InputManager.Instance.GetCurrentSettingsDetails(PlayerAction.MoveHorizontal) + "\n" +
            InputManager.Instance.GetCurrentSettingsDetails(PlayerAction.MoveVertical) + "\n" +
            InputManager.Instance.GetCurrentSettingsDetails(PlayerAction.Jump) + "\n" +
            InputManager.Instance.GetCurrentSettingsDetails(PlayerAction.Interact) + "\n" +
            InputManager.Instance.GetCurrentSettingsDetails(PlayerAction.FirePrimary) + "\n" +
            InputManager.Instance.GetCurrentSettingsDetails(PlayerAction.FireSecondary);
    }
}
