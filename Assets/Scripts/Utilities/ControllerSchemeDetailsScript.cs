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
	}
	
	// Update is called once per frame
	void Update ()
    {
        //build the string
        detailsText.text = "Pause Game: " + InputManager.Instance.GetCurrentSettingsDetails(PlayerAction.PauseGame) + "\n" +
            "Game Menu: " + InputManager.Instance.GetCurrentSettingsDetails(PlayerAction.ViewInventory) + "\n" +
            "Movement Horizontal: " + InputManager.Instance.GetCurrentSettingsDetails(PlayerAction.MoveVertical) + "\n" +
            "Movement Vertical: " + InputManager.Instance.GetCurrentSettingsDetails(PlayerAction.MoveVertical) + "\n" +
            "Jump: " + InputManager.Instance.GetCurrentSettingsDetails(PlayerAction.Jump) + "\n" +
            "Interaction: " + InputManager.Instance.GetCurrentSettingsDetails(PlayerAction.Interact) + "\n" +
            "Fire Primary Attack: " + InputManager.Instance.GetCurrentSettingsDetails(PlayerAction.FirePrimary) + "\n" +
            "Fire Secondary Attack: " + InputManager.Instance.GetCurrentSettingsDetails(PlayerAction.FireSecondary);
	}
}
