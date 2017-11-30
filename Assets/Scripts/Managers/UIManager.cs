using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

class UIManager
{
    #region Fields

    //singleton instance of class
    static UIManager instance;

    public Dictionary<ItemType, Sprite> inventoryImages;// = new Dictionary<ItemType, Sprite>();

    InventoryUI inventoryDisplay;

    #endregion

    #region Constructor

    /// <summary>
    /// private constructor
    /// </summary>
    private UIManager()
    {
        //effectsDict = (Resources.LoadAll<AudioClip>("Audio/Sounds")).ToDictionary(s => s.name);
        inventoryImages = new Dictionary<ItemType, Sprite>()
        {
            { ItemType.HealthPotion, Resources.Load<Sprite>("Graphics/Items/HealthPotion") },
            //{ ItemType.HolyGrailQuestItem, Resources.Load<Sprite>("Graphics/Items/HealthPotion") },
            //{ ItemType.PizzaCutterQuestItem, Resources.Load<Sprite>("Graphics/Items/HealthPotion") },
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

    #endregion

    #region Methods

    public void Update()
    {
        //escape closes all ui
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            CloseUI();
        }

        //inventory
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

    void CloseUI()
    {
        if (PlayerInventoryUI)
        {
            MonoBehaviour.Destroy(PlayerInventoryUI.gameObject);
        }
    }

    #endregion
}