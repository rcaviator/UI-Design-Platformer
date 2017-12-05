using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Specific data per each state in a quest
/// </summary>
public class QuestStateNode
{
    #region Fields

    //bool isCompleted = false;
    string objectiveText;
    string detailedText;

    #endregion

    #region Constructors

    public QuestStateNode(string objective, string details)
    {
        objectiveText = objective;
        detailedText = details;
    }

    #endregion

    #region Properties

    public bool ObjectiveCompleted
    { get; set; }

    public string ObjectiveText
    { get { return ObjectiveText; } }

    public string DetailedText
    { get { return detailedText; } }

    #endregion
}