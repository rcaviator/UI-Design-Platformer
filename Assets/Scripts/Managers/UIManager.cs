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

    //pause game menu
    PauseGameMenu pauseMenu;

    //main player menu
    InventoryUI inventoryDisplay;

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

        //PlayerInventoryUI = Resources.Load<InventoryUI>("Prefabs/InventoryUI");
    }

    #endregion

    #region Properties

    /// <summary>
    /// returns the instance
    /// </summary>
    public static UIManager Instance
    {
        get { return instance ?? (instance = new UIManager()); }
    }

    public InventoryUI PlayerInventoryUI
    { get; private set; }


    public PauseGameMenu PauseMenu
    { get; private set; }

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
        if (InputManager.Instance.GetButtonDown(PlayerAction.ViewInventory) && !PlayerInventoryUI)
        {
            CloseUI();
            PlayerInventoryUI = MonoBehaviour.Instantiate(Resources.Load<InventoryUI>("Prefabs/InventoryUI"), Vector3.zero, Quaternion.identity);
        }
        else if (PlayerInventoryUI && InputManager.Instance.GetButtonDown(PlayerAction.ViewInventory))
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
        if (PlayerInventoryUI)
        {
            MonoBehaviour.Destroy(PlayerInventoryUI.gameObject);
        }
    }

    #endregion
}