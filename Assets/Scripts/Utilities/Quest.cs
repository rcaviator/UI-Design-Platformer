using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;


/// <summary>
/// An individual quest
/// </summary>
public class Quest
{
    #region Fields

    //the quest type
    Quests questType;

    //the quest name
    string name;

    //list of quest nodes for quest type
    Queue<QuestStateNode> quest;

    #endregion

    #region Constructors

    /// <summary>
    /// Constructor for the quest
    /// </summary>
    /// <param name="questTypeName">the type of quest</param>
    /// <param name="questName">the name of the quest</param>
    public Quest(string questName, Quests questTypeName)
    {
        //set variables
        questType = questTypeName;
        quest = new Queue<QuestStateNode>();
        name = questName;

        //populate the list based on which quest type is selected
        switch (questType)
        {
            case Quests.None:
                break;
            //tutorial
            case Quests.Tutorial:
                break;
            //main quest
            case Quests.MainQuest:

                break;
            //fetch quest, bring back holy necklace, desert location, extra dmg to enemies
            case Quests.Quest1:
                quest.Enqueue(new QuestStateNode("Use Portal", "I decided to help the local Priest in aiding against the undead uprising and he told me where to go get a holy necklace for him by first stepping through the portal he provided."));
                quest.Enqueue(new QuestStateNode("Find and Collect the Necklace", "Now that I'm here, I need to fight off any enemies and search for this holy necklace for the local Priest."));
                quest.Enqueue(new QuestStateNode("Use Return Portal", "I got the holy necklace and now a return portal has spawned. I should use it to return to the village and deliver the necklace to the local Priest."));
                quest.Enqueue(new QuestStateNode("Talk to Priest", "I have returned to the village with the holy necklace and now I only need to give the necklace to the local Priest."));
                //quest completion
                break;
            //collect quest, collect secondary weapon, icy location, additional attack type
            case Quests.Quest2:
                quest.Enqueue(new QuestStateNode("Use Portal", "The local Blacksmith has told me where to go get a powerful magical weapon that can help me on and I need to use the portal to take me to the area where it was last known."));
                quest.Enqueue(new QuestStateNode("Find and Collect the Weapon", "Now that I'm here, I need to fight off any enemies and search for this weapon the local Blacksmith said."));
                quest.Enqueue(new QuestStateNode("Use Return Portal", "I now have the weapon and a return portal has spawned. I should use it to return back to the village and continue on."));
                //quest completion
                break;
            //deliver quest, deliver ward to location, mountain location, reduce received dmg
            case Quests.Quest3:
                quest.Enqueue(new QuestStateNode("Use Portal", "The local Mage needs my help in protecting an area with a ward and I agreeded to help him. I should use the portal he created to travel there."));
                quest.Enqueue(new QuestStateNode("Find Location to Place the Ward", "Now that I'm here, I need to fight off any enemies and search for the alter the local Mage told me to place the ward."));
                quest.Enqueue(new QuestStateNode("Place the Ward", "I found the location of where to put the ward. Now I just need to put it there."));
                quest.Enqueue(new QuestStateNode("Use Return Portal", "The area is now safe with the ward from the local Mage gave me. I can use this return portal to take me back to the village."));
                quest.Enqueue(new QuestStateNode("Talk to Mage", "Now that I'm back, I should tell the local Mage the outcome of delivering the ward."));
                break;
            default:
                break;
        }
    }

    #endregion

    #region Properties

    public string QuestName
    { get { return name; } }

    #endregion

    #region Public Methods

    //update quest methods

    #endregion

    #region Private Methods



    #endregion
}