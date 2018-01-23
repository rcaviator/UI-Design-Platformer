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
	public void Start()
    {
        UIManager.Instance.PlayerUICanvas = this;

        //set references
        locationText = transform.GetChild(1).transform.GetChild(0).GetComponent<Text>();
        questText = transform.GetChild(2).transform.GetChild(0).GetComponent<Text>();
        viewQuestsButton = transform.GetChild(2).transform.GetChild(1).GetComponent<Button>();
        controllsText = transform.GetChild(3).transform.GetChild(0).GetComponent<Text>();
        scoreText = transform.GetChild(4).transform.GetChild(0).GetComponent<Text>();

        //set location text
        switch (MySceneManager.Instance.CurrentScene)
        {
            case Scenes.None:
                break;
            case Scenes.MainMenu:
                break;
            case Scenes.Credits:
                break;
            case Scenes.Options:
                break;
            case Scenes.HowToPlay:
                break;
            case Scenes.Tutorial:
                locationText.text = "Location: Tutorial";
                break;
            case Scenes.Village:
                locationText.text = "Location: Village";
                break;
            case Scenes.Quest1:
                locationText.text = "Location: Desert";
                break;
            case Scenes.Quest2:
                locationText.text = "Location: Frozen North";
                break;
            case Scenes.Quest3:
                locationText.text = "Location: Mountain";
                break;
            case Scenes.QuestMain:
                locationText.text = "Location: Plains";
                break;
            case Scenes.Boss1:
                locationText.text = "Location: Castle Town";
                break;
            case Scenes.BossFinal:
                locationText.text = "Location: Throne Room";
                break;
            case Scenes.Defeat:
                break;
            case Scenes.Victory:
                break;
            case Scenes.TestLevel:
                locationText.text = "Location: Test Level";
                break;
            default:
                break;
        }

        //quest text
        switch (QuestManager.Instance.CurrentQuestEnum)
        {
            case Quests.None:
                break;
            case Quests.Tutorial:
                questText.text = "Tutorial\n" +
                    "Follow the leader";
                break;
            case Quests.MainQuest:
                questText.text = "Main\n" +
                    "Explore further ahead";
                break;
            case Quests.Quest1:
                questText.text = "Fetch Lost Relic\n" +
                    "Find and collect lost relic";
                break;
            case Quests.Quest2:
                questText.text = "Collect Legendary Weapon\n" +
                    "Find and collect the weapon";
                break;
            case Quests.Quest3:
                questText.text = "Deliver Magical Ward\n" +
                    "Find suitable ward location";
                break;
            default:
                break;
        }

        controllsText.text = "Pause Menu: " + InputManager.Instance.GetCurrentSettingsDetails(PlayerAction.PauseGame)
            + ". Inventory: " + InputManager.Instance.GetCurrentSettingsDetails(PlayerAction.ViewInventory)
            + ". Shoot: " + InputManager.Instance.GetCurrentSettingsDetails(PlayerAction.FirePrimary)
            + ". Enter Portal: " + InputManager.Instance.GetCurrentSettingsDetails(PlayerAction.MoveVertical);
	}

    private void OnDestroy()
    {
        UIManager.Instance.PlayerUICanvas = null;
    }

    public void ScoreChange(bool change)
    {
        scoreText.GetComponent<ScoreText>().ScoreChange(change);
    }

    public void OnViewQuestsClick()
    {
        UIManager.Instance.CreateCharacterMenu();
    }

    public void UpdateQuestDisplay(string questName, string questObjective)
    {
        questText.text = questName + "\n" +
                    questObjective;
    }

    public void PlayerFire()
    {
        GameManager.Instance.Player.GetComponent<Player>().InputPlayerShoot = true;
    }
}
