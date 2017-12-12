using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestUI : MonoBehaviour
{
    [SerializeField]
    Quests quest;

    //references to the ui elements
    Text questGiver;
    Text questTitle;
    Text questDialog;
    Image questNPC;


	// Use this for initialization
	void Awake()
    {
        GameManager.Instance.Paused = true;
        questGiver = transform.GetChild(0).transform.GetChild(2).GetComponent<Text>();
        questTitle = transform.GetChild(0).transform.GetChild(3).GetComponent<Text>();
        questDialog = transform.GetChild(0).transform.GetChild(4).GetComponent<Text>();
        questNPC = transform.GetChild(0).transform.GetChild(5).GetComponent<Image>();
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
    /// Initializes the quest dialog
    /// </summary>
    /// <param name="giver">the npc giver's name</param>
    /// <param name="title">the title of the quest</param>
    /// <param name="dialog">the dialog for the opening quest</param>
    /// <param name="npc">the picture of the npc</param>
    public void Initialize(string giver, string title, string dialog, Sprite npc)
    {
        questGiver.text = giver;
        questTitle.text = title;
        questDialog.text = dialog;
        questNPC.sprite = npc;
        AudioManager.Instance.PlayUISoundEffect(UISoundEffect.GamePaused);
    }

    public void OnQuestAccept()
    {
        switch (quest)
        {
            case Quests.None:
                break;
            case Quests.Tutorial:
                break;
            case Quests.MainQuest:
                break;
            case Quests.Quest1:
                //check if quest exists
                if (QuestManager.Instance.GetAvailableQuests.ContainsKey(quest))
                {
                    //check if quest was already completed
                    if (!QuestManager.Instance.GetCompletedQuests.ContainsKey(quest))
                    {
                        //check if quest is already current
                        if (!QuestManager.Instance.GetCurrentQuests.ContainsKey(quest))
                        {
                            //add quest
                            QuestManager.Instance.AddQuest(quest);
                        }
                    }
                }
                break;
            case Quests.Quest2:
                break;
            case Quests.Quest3:
                break;
            default:
                break;
        }
    }

    public void OnQuestDecline()
    {
        AudioManager.Instance.PlayUISoundEffect(UISoundEffect.MenuBack);
        Destroy(gameObject);
    }
}
