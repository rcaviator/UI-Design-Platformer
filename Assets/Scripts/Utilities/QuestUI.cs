using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestUI : MonoBehaviour
{
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
    }

    public void OnQuestAccept()
    {

    }

    public void OnQuestDecline()
    {
        AudioManager.Instance.PlayUISoundEffect(UISoundEffect.MenuBack);
        Destroy(gameObject);
    }
}
