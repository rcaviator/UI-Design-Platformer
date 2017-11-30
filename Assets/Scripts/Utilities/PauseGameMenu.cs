using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// enum for cycling pause menu panels
/// </summary>
public enum PauseMenuPanel
{
    None, Main, Options, SaveGame, LoadGame,
};

public class PauseGameMenu : MonoBehaviour
{
    //dictionary of menu panel canvases
    Dictionary<PauseMenuPanel, GameObject> pausePanels;

	// Use this for initialization
	void Awake ()
    {
        //pause the game
        GameManager.Instance.Paused = true;

        //get the panels and populate the dictionary


        //set the main panel to active
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    public void OnPanelChange(PauseMenuPanel panel)
    {
        //close all panels and enable the passed panel
    }
}
