using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestText : MonoBehaviour
{

	// Use this for initialization
	void Start ()
    {
		
	}
	
	public void OnTextChange(string newText)
    {
        GetComponent<Text>().text = newText;
        AudioManager.Instance.PlayUISoundEffect(UISoundEffect.GameSelect);
    }

    public void OnQuestChange(int questNumber)
    {
        switch (questNumber)
        {
            case 0:
                QuestManager.Instance.CurrentQuestEnum = Quests.MainQuest;
                UIManager.Instance.PlayerUICanvas.UpdateQuestDisplay("Save The World!", "Explore further ahead");
                break;
            case 1:
                QuestManager.Instance.CurrentQuestEnum = Quests.Quest1;
                UIManager.Instance.PlayerUICanvas.UpdateQuestDisplay("Fetch Lost Relic", "Find and collect lost relic");
                break;
            case 2:
                QuestManager.Instance.CurrentQuestEnum = Quests.Quest2;
                UIManager.Instance.PlayerUICanvas.UpdateQuestDisplay("Collect Legendary Weapon", "Find and collect the weapon");
                break;
            case 3:
                QuestManager.Instance.CurrentQuestEnum = Quests.Quest3;
                UIManager.Instance.PlayerUICanvas.UpdateQuestDisplay("Deliver Magical Ward", "Find suitable ward location");
                break;
            default:
                break;
        }
    }
}
