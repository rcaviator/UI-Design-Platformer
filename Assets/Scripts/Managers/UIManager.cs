using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// UIManager handles all the dynamic ui elements
/// </summary>
class UIManager
{
    #region Fields

    //singleton instance of class
    static UIManager instance;

    //dictionary for inventory ui images
    public Dictionary<ItemType, Sprite> inventoryImages;

    ////pause game menu
    //PauseGameMenu pauseMenu;

    ////main player menu
    //CharacterMenuCanvas characterDisplay;

    ////main hud
    //UICanvas uiCanvas;

    ////quest menu
    //QuestUI questUI;

    #endregion

    #region Constructor

    /// <summary>
    /// private constructor
    /// </summary>
    private UIManager()
    {
        //initialize the inventory images dictionary
        inventoryImages = new Dictionary<ItemType, Sprite>()
        {
            { ItemType.HealthPotion, Resources.Load<Sprite>("Graphics/Items/HealthPotion") },
            { ItemType.EnergyShield, Resources.Load<Sprite>("Graphics/Items/EnergyShieldItem") },
            { ItemType.KeyPartPickupHandle, Resources.Load<Sprite>("Graphics/Items/KeyHandle") },
            { ItemType.KeyPartPickupShaft, Resources.Load<Sprite>("Graphics/Items/KeyShaft") },
            { ItemType.KeyPartPickupBit, Resources.Load<Sprite>("Graphics/Items/KeyBit") },
            { ItemType.Key, Resources.Load<Sprite>("Graphics/Items/Key") }
        };
    }

    #endregion

    #region Properties

    /// <summary>
    /// Returns the instance
    /// </summary>
    public static UIManager Instance
    {
        get { return instance ?? (instance = new UIManager()); }
    }

    public CharacterMenuCanvas PlayerCharacterMenuCanvas
    { get; set; }


    public PauseGameMenu PauseMenu
    { get; set; }

    public UICanvas PlayerUICanvas
    { get; set; }

    public QuestUI QuestDialog
    { get; set; }

    #endregion

    #region Methods

    public void Update()
    {
        //escape closes all ui
        //if (Input.GetKeyDown(KeyCode.Escape))
        //{
        //    CloseUI();
        //}

        //pause game menu
        if (InputManager.Instance.GetButtonDown(PlayerAction.PauseGame) && !PauseMenu)
        {
            CloseUI();
            PauseMenu = MonoBehaviour.Instantiate(Resources.Load<PauseGameMenu>("Prefabs/PauseMenuCanvas"), Vector3.zero, Quaternion.identity);
        }
        else if (PauseMenu && InputManager.Instance.GetButtonDown(PlayerAction.PauseGame))
        {
            CloseUI();
        }

        //player menu
        if (InputManager.Instance.GetButtonDown(PlayerAction.ViewInventory) && !PlayerCharacterMenuCanvas)
        {
            CloseUI();
            PlayerCharacterMenuCanvas = MonoBehaviour.Instantiate(Resources.Load<CharacterMenuCanvas>("Prefabs/CharacterMenuCanvas"), Vector3.zero, Quaternion.identity);
        }
        else if (PlayerCharacterMenuCanvas && InputManager.Instance.GetButtonDown(PlayerAction.ViewInventory))
        {
            CloseUI();
        }

        //quest dialog
        if (QuestDialog && InputManager.Instance.GetButtonDown(PlayerAction.PauseGame))
        {
            CloseUI();
        }
    }


    /// <summary>
    /// Closes all ui
    /// </summary>
    void CloseUI()
    {
        //pause menu
        if (PauseMenu)
        {
            MonoBehaviour.Destroy(PauseMenu.gameObject);
        }

        //player menu
        if (PlayerCharacterMenuCanvas)
        {
            MonoBehaviour.Destroy(PlayerCharacterMenuCanvas.gameObject);
        }

        //quest ui
        if (QuestDialog)
        {
            MonoBehaviour.Destroy(QuestDialog.gameObject);
        }
    }


    public void CreateCharacterMenu()
    {
        if (!PlayerCharacterMenuCanvas)
        {
            CloseUI();
            PlayerCharacterMenuCanvas = MonoBehaviour.Instantiate(Resources.Load<CharacterMenuCanvas>("Prefabs/CharacterMenuCanvas"), Vector3.zero, Quaternion.identity);
        }
    }

    public void CreateQuestDialog(string giver, string title, string dialog, Sprite npc)
    {
        if (!QuestDialog)
        {
            QuestDialog = MonoBehaviour.Instantiate(Resources.Load<QuestUI>("Prefabs/QuestDialogCanvas"), Vector3.zero, Quaternion.identity);
            QuestDialog.Initialize(giver, title, dialog, npc);
        }
    }

    public void CreatePauseGameMenu()
    {
        if (!PauseMenu)
        {
            CloseUI();
            PauseMenu = MonoBehaviour.Instantiate(Resources.Load<PauseGameMenu>("Prefabs/PauseMenuCanvas"), Vector3.zero, Quaternion.identity);
        }
    }

    #endregion
}