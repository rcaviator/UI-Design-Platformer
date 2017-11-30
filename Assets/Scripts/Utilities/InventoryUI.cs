using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour
{
    [SerializeField]
    Image[] inventoryPanels;

    //items
    GameObject shield;

	// Use this for initialization
	void Start ()
    {
        //pause game
        GameManager.Instance.Paused = true;

        //get items
        shield = Resources.Load<GameObject>("Prefabs/Shield");

        int index = 0;

        foreach (KeyValuePair<ItemType, List<Item>> item in GameManager.Instance.Player.GetComponent<Player>().PlayerInventory.inventory)
        {
            if (item.Value.Count != 0)
            {
                inventoryPanels[index].sprite = UIManager.Instance.inventoryImages[item.Key];
                inventoryPanels[index].gameObject.GetComponentInChildren<Text>().text = item.Value.Count.ToString();
                index++;
            }
            else
            {
                inventoryPanels[index].sprite = null;
                inventoryPanels[index].gameObject.GetComponentInChildren<Text>().text = "";
            }
        }
	}

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
        //Debug.Log(aItem);
        //activate item
        if (aItem == ItemType.EnergyShield)
        {
            Instantiate(shield, GameManager.Instance.Player.transform.position, Quaternion.identity);
            GameManager.Instance.Player.gameObject.GetComponent<Player>().PlayerInventory.RemoveFirstItemOfType(aItem);
            Start();
        }
        else if (aItem == ItemType.FireSpray)
        {
            //GameManager.Instance.Player.gameObject.GetComponent<Player>().PlayerInventory.RemoveFirstItemOfType(aItem);
            //Start();
        }
        else if (aItem == ItemType.HealthPotion)
        {
            GameManager.Instance.Player.gameObject.GetComponent<Player>().PlayerInventory.RemoveFirstItemOfType(aItem);
            //fix this later
            GameManager.Instance.Player.gameObject.GetComponent<Player>().PlayerHealth += 50f;
            Start();
        }
    }

    /// <summary>
    /// used to restart the ui
    /// </summary>
    public void ReStartUI()
    {
        Start();
    }

    private void OnDestroy()
    {
        GameManager.Instance.Paused = false;
    }

    //// Update is called once per frame
    //void Update () {

    //}
}
