//using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public enum CharacterMenuMenus
{
    None, Inventory, Quests,
}

public class CharacterMenuCanvas : MonoBehaviour
{
    //inventory panels
    [SerializeField]
    Image[] inventoryPanels;

    //crafting panels
    [SerializeField]
    Image[] craftingPanels;

    //crafting button
    [SerializeField]
    Button craftingButton;

    //spawnable items from viewing inventory
    GameObject shield;

    Color trans;
    Color craftingTrans;

    //dictionary for the menus
    Dictionary<CharacterMenuMenus, GameObject> characterMenuPanels;

    private void Awake()
    {
        //create the menu dictionary and populate it
        characterMenuPanels = new Dictionary<CharacterMenuMenus, GameObject>()
        {
            { CharacterMenuMenus.Inventory, transform.GetChild(0).transform.GetChild(1).gameObject },
            { CharacterMenuMenus.Quests, transform.GetChild(0).transform.GetChild(2).gameObject },
        };

        //disable all of them
        foreach (KeyValuePair<CharacterMenuMenus, GameObject> menu in characterMenuPanels)
        {
            menu.Value.SetActive(false);
        }

        //enable the inventory one
        characterMenuPanels[CharacterMenuMenus.Inventory].SetActive(true);

        //play menu popup sound
        AudioManager.Instance.PlayUISoundEffect(UISoundEffect.GamePaused);

        //set inventory panel color to reference
        trans = inventoryPanels[0].color;

        //set crafting panel color reference
        craftingTrans = craftingPanels[0].color;
    }

    // Use this for initialization
    void Start ()
    {
        //pause game
        GameManager.Instance.Paused = true;

        //get spawnable items
        shield = Resources.Load<GameObject>("Prefabs/Shield");

        int index = 0;

        //set sprites for inventory panels
        foreach (KeyValuePair<ItemType, List<Item>> item in GameManager.Instance.Player.GetComponent<Player>().PlayerInventory.inventory)
        {
            //if the item count is not zero and the item is not a crafting item
            if (item.Value.Count != 0 && !item.Value[0].IsCraftingItem)
            {
                inventoryPanels[index].color = Color.white;
                inventoryPanels[index].sprite = UIManager.Instance.inventoryImages[item.Key];
                inventoryPanels[index].gameObject.GetComponentInChildren<Text>().text = item.Value.Count.ToString();
                index++;
            }
            else
            {
                inventoryPanels[index].sprite = null;
                inventoryPanels[index].color = trans;
                inventoryPanels[index].gameObject.GetComponentInChildren<Text>().text = "";
            }
        }

        //reset index
        index = 0;

        //set sprites for crafting panels
        foreach (KeyValuePair<ItemType, List<Item>> item in GameManager.Instance.Player.GetComponent<Player>().PlayerInventory.inventory)
        {
            //if the item count is not zero and the item is crafting item
            if (item.Value.Count != 0 && item.Value[0].IsCraftingItem)
            {
                //set appropreate bool
                switch (item.Value[0].TheItem)
                {
                    case ItemType.KeyPartPickupHandle:
                        HasKeyHandle = true;
                        craftingPanels[0].color = Color.white;
                        break;
                    case ItemType.KeyPartPickupShaft:
                        HasKeyShaft = true;
                        craftingPanels[1].color = Color.white;
                        break;
                    case ItemType.KeyPartPickupBit:
                        HasKeyBit = true;
                        craftingPanels[2].color = Color.white;
                        break;
                    case ItemType.Key:
                        //HasKey = true;
                        break;
                    default:
                        Debug.Log("Error in setting crafting item. Found: " + item.Value[0].TheItem.ToString());
                        break;
                }

                index++;
            }
        }
    }

    public bool HasKeyHandle
    { get; set; }

    public bool HasKeyShaft
    { get; set; }

    public bool HasKeyBit
    { get; set; }

    public bool HasKey
    { get; set; }


    bool HasKeyHandleClickedOn
    { get; set; }

    bool HasKeyShaftClickedOn
    { get; set; }
    
    bool HasKeyBitClickedOn
    { get; set; }

    public void UseItem(Image useItem)
    {
        //create image variable
        Image aImage = inventoryPanels[0];

        //get image from panel
        foreach (Image i in inventoryPanels)
        {
            if (useItem == i)
            {
                aImage = i;
                break;
            }
        }

        //create item type variable
        ItemType aItem = ItemType.None;

        //get item type from image
        foreach (KeyValuePair<ItemType, Sprite> i in UIManager.Instance.inventoryImages)
        {
            //Debug.Log(i.Key);
            if (aImage.sprite == i.Value)
            {
                aItem = i.Key;
                break;
            }
        }

        //activate item
        if (aItem == ItemType.EnergyShield)
        {
            Instantiate(shield, GameManager.Instance.Player.transform.position, Quaternion.identity);
            GameManager.Instance.Player.gameObject.GetComponent<Player>().PlayerInventory.RemoveFirstItemOfType(aItem);
            AudioManager.Instance.PlayUISoundEffect(UISoundEffect.UseShield);
        }
        else if (aItem == ItemType.HealthPotion)
        {
            GameManager.Instance.Player.gameObject.GetComponent<Player>().PlayerInventory.RemoveFirstItemOfType(aItem);
            GameManager.Instance.Player.gameObject.GetComponent<Player>().PlayerHealth += Constants.ITEM_HEALTH_POTION_RESTORATION;
            AudioManager.Instance.PlayUISoundEffect(UISoundEffect.UseHealthPotion);
        }

        ResetUI();
    }

    
    public void UseCraftItem(Image craftableItem)
    {
        if (HasKeyHandle && HasKeyShaft && HasKeyBit)
        {
            if (!HasKeyHandleClickedOn && craftableItem.sprite.name == "KeyHandle")
            {
                HasKeyHandleClickedOn = true;
            }
            else if (!HasKeyShaftClickedOn && craftableItem.sprite.name == "KeyShaft")
            {
                HasKeyShaftClickedOn = true;
            }
            else if (!HasKeyBitClickedOn && craftableItem.sprite.name == "KeyBit")
            {
                HasKeyBitClickedOn = true;
            }

            //if all items are in and have been clicked on
            if (HasKeyHandleClickedOn && HasKeyShaftClickedOn && HasKeyBitClickedOn)
            {
                craftingButton.interactable = true;
            }
        }
    }


    public void CraftItem()
    {
        if (HasKeyHandleClickedOn && HasKeyShaftClickedOn && HasKeyBitClickedOn)
        {
            //create key, add key to inventory, remove key parts, add key to crafted panel, reset other panels, reset bools
            GameManager.Instance.Player.GetComponent<Player>().PlayerInventory.RemoveFirstItemOfType(ItemType.KeyPartPickupHandle);
            GameManager.Instance.Player.GetComponent<Player>().PlayerInventory.RemoveFirstItemOfType(ItemType.KeyPartPickupShaft);
            GameManager.Instance.Player.GetComponent<Player>().PlayerInventory.RemoveFirstItemOfType(ItemType.KeyPartPickupBit);
            GameManager.Instance.Player.GetComponent<Player>().PlayerInventory.AddItem(new Item(ItemType.Key));
            craftingPanels[3].sprite = UIManager.Instance.inventoryImages[ItemType.Key];
            craftingPanels[3].color = Color.white;
            craftingPanels[0].color = craftingTrans;
            craftingPanels[1].color = craftingTrans;
            craftingPanels[2].color = craftingTrans;
            craftingPanels[0].GetComponent<UseItemScript>().SetCraftableItemSelect(false);
            craftingPanels[1].GetComponent<UseItemScript>().SetCraftableItemSelect(false);
            craftingPanels[2].GetComponent<UseItemScript>().SetCraftableItemSelect(false);
            HasKey = true;
            HasKeyHandle = false;
            HasKeyShaft = false;
            HasKeyBit = false;
            HasKeyHandleClickedOn = false;
            HasKeyShaftClickedOn = false;
            HasKeyBitClickedOn = false;
        }
    }

    public void ResetUI()
    {
        for (int i = 0; i < inventoryPanels.Length; i++)
        {
            inventoryPanels[i].sprite = null;
            inventoryPanels[i].color = trans;
            inventoryPanels[i].gameObject.GetComponentInChildren<Text>().text = "";
        }

        Start();
    }

    private void OnDestroy()
    {
        AudioManager.Instance.PlayUISoundEffect(UISoundEffect.GameStart);
        GameManager.Instance.Paused = false;
    }

    /// <summary>
    /// used for changing inbetween each character menu
    /// </summary>
    /// <param name="newMenu">the string name of the menu to change to. stupid enum bypass</param>
    public void ChangeMenu(string newMenu)
    {
        CharacterMenuMenus tempMenu = CharacterMenuMenus.None;

        if (newMenu == CharacterMenuMenus.None.ToString())
        {
            tempMenu = CharacterMenuMenus.None;
        }
        else if (newMenu == CharacterMenuMenus.Inventory.ToString())
        {
            tempMenu = CharacterMenuMenus.Inventory;
        }
        else if (newMenu == CharacterMenuMenus.Quests.ToString())
        {
            tempMenu = CharacterMenuMenus.Quests;
        }

        foreach (KeyValuePair<CharacterMenuMenus, GameObject> menu in characterMenuPanels)
        {
            menu.Value.SetActive(false);
        }

        characterMenuPanels[tempMenu].SetActive(true);
        AudioManager.Instance.PlayUISoundEffect(UISoundEffect.MenuForward);
    }

    /// <summary>
    /// closes the character menu
    /// </summary>
    public void CloseMenu()
    {
        Destroy(gameObject);
    }
}
