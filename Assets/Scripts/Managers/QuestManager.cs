using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// enum for all quests
/// </summary>
public enum Quests
{
    None, Tutorial, MainQuest, Quest1, Quest2, Quest3,
}

/// <summary>
/// Manager for handling quests
/// </summary>
class QuestManager
{
    #region Fields

    //singleton instance
    static QuestManager instance;

    //dictionary to hold all the quests
    Dictionary<Quests, Quest> questDict;

    Dictionary<Quests, Quest> currentQuests;
    Dictionary<Quests, Quest> completedQuests;

    ////each quest
    //Quest tutorial;       //complete tutorial and background info, forest location
    //Quest questMain;      //defeat the main antagonist, village: plains, pathway: moutain, bosses: inside
    //Quest quest1Fetch;    //bring back holy necklace, desert location, extra dmg to enemies
    //Quest quest2Collect;  //collect secondary weapon, icy location, additional attack type
    //Quest quest3Deliver;  //deliver ward to location, mountain location, reduce received dmg

    #endregion

    #region Constructors

    /// <summary>
    /// Constructor
    /// </summary>
    private QuestManager()
    {
        //create and populate the quest dictionary
        questDict = new Dictionary<Quests, Quest>()
        {
            { Quests.Tutorial, new Quest("Tutorial", Quests.Tutorial) },
            { Quests.MainQuest, new Quest("Save The World!", Quests.MainQuest) },
            { Quests.Quest1, new Quest("Fetch Lost Relic", Quests.Quest1) },
            { Quests.Quest2, new Quest("Collect Legendary Weapon", Quests.Quest2) },
            { Quests.Quest3, new Quest("Deliver Magical Ward", Quests.Quest3) },
        };
    }

    #endregion

    #region Properties

    /// <summary>
    /// Returns the instance
    /// </summary>
    public static QuestManager Instance
    {
        get { return instance ?? (instance = new QuestManager()); }
    }


    public Dictionary<Quests, Quest> GetAvailableQuests
    { get { return questDict; } }

    public Dictionary<Quests, Quest> GetCurrentQuests
    { get { return currentQuests; } }

    public Dictionary<Quests, Quest> GetCompletedQuests
    { get { return completedQuests; } }

    #endregion

    #region Public Methods

    public void AddQuest(Quests quest)
    {
        currentQuests.Add(quest, questDict[quest]);
    }

    public void CompleteQuest(Quests quest)
    {

    }

    #endregion

    #region Private Methods



    #endregion
}