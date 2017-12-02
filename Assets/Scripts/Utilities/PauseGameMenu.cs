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
        pausePanels = new Dictionary<PauseMenuPanel, GameObject>()
        {
            { PauseMenuPanel.Main, transform.GetChild(0).transform.GetChild(0).gameObject },
            { PauseMenuPanel.Options, transform.GetChild(0).transform.GetChild(1).gameObject },
        };

        //disable them
        foreach (KeyValuePair<PauseMenuPanel, GameObject> panels in pausePanels)
        {
            panels.Value.SetActive(false);
        }

        //set the main panel to active
        pausePanels[PauseMenuPanel.Main].SetActive(true);

        //play sound
        AudioManager.Instance.PlayUISoundEffect(UISoundEffect.GamePaused);
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}


    private void OnDestroy()
    {
        GameManager.Instance.Paused = false;
    }

    /// <summary>
    /// Closes this menu
    /// </summary>
    public void ClosePauseMenu()
    {
        AudioManager.Instance.PlayUISoundEffect(UISoundEffect.GameStart);
        Destroy(gameObject);
        //UIManager.Instance.CloseUI();
    }

    /// <summary>
    /// stupid enum bypass for the button
    /// </summary>
    public void GoToOptionsPanel()
    {
        AudioManager.Instance.PlayUISoundEffect(UISoundEffect.MenuForward);
        OnPanelChange(PauseMenuPanel.Options);
    }

    /// <summary>
    /// same for this one
    /// </summary>
    public void GoToMainPanel()
    {
        AudioManager.Instance.PlayUISoundEffect(UISoundEffect.MenuBack);
        OnPanelChange(PauseMenuPanel.Main);
    }

    /// <summary>
    /// Changes the pause menu panel
    /// </summary>
    /// <param name="panel">the panel to change to</param>
    public void OnPanelChange(PauseMenuPanel panel)
    {
        //close all panels and enable the passed panel
        foreach (KeyValuePair<PauseMenuPanel, GameObject> panels in pausePanels)
        {
            panels.Value.SetActive(false);
        }

        //enable the one we want
        pausePanels[panel].SetActive(true);
    }
}
