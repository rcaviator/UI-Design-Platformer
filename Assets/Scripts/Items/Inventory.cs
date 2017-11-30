using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class Inventory
{
    public Dictionary<ItemType, List<Item>> inventory = new Dictionary<ItemType, List<Item>>();

    /// <summary>
    /// Inventory constructor. Adds all item types do the inventory dictionary
    /// </summary>
    public Inventory()
    {
        //loop through all ItemType enum values and add them to the dictionary
        //the new List<Item>() allows for stackable items
        foreach (ItemType t in Enum.GetValues(typeof(ItemType)))
        {
            inventory.Add(t, new List<Item>());
        }
    }

    /// <summary>
    /// AddItem takes in a item and adds it to the dictionary
    /// </summary>
    /// <param name="newItem">the item to add</param>
    public void AddItem(Item newItem)
    {
        //if the item type is in the dictionary, add it to the internal list
        if (inventory.ContainsKey(newItem.itemType))
        {
            inventory[newItem.itemType].Add(newItem);
        }
        else
        {
            inventory.Add(newItem.itemType, new List<Item>() { newItem });
        }
    }

    /// <summary>
    /// RemoveItem removes an item from the dictionary
    /// </summary>
    /// <param name="newItem">the item to remove</param>
    /// <returns></returns>
    public Item RemoveItem(Item newItem)
    {
        //if the item is in the dictionary and the count is not 0, remove it from the internal list
        if (inventory.ContainsKey(newItem.itemType))
        {
            if (inventory[newItem.itemType].Count != 0)
            {
                inventory[newItem.itemType].Remove(newItem);
                return newItem;
            }
            else
            {
                return null;
            }
        }
        else
        {
            return null;
        }
    }

    /// <summary>
    /// RemoveFirstItemOfType removes the first item of the ItemType from the dictionary
    /// </summary>
    /// <param name="item">the item to remove</param>
    /// <returns></returns>
    public Item RemoveFirstItemOfType(ItemType item)
    {
        //if the item is in the dictionary and the count is not 0, remove it from the internal list
        if (inventory.ContainsKey(item))
        {
            if (inventory[item].Count != 0)
            {
                Item itemToRemove = inventory[item][0];
                inventory[item].RemoveAt(0);
                return itemToRemove;
            }
            else
            {
                return null;
            }
        }
        else
        {
            return null;
        }
    }

    /// <summary>
    /// ContainsItemOfType returns true if the item to check is in the dictionary
    /// </summary>
    /// <param name="item">the item to check</param>
    /// <returns></returns>
    public bool ContainsItemOfType(ItemType item)
    {
        //if the item is in the dictionary and its count is not 0 in the internal list
        if (inventory.ContainsKey(item))
        {
            if (inventory[item].Count != 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        else
        {
            return false;
        }
    }
}

/// <summary>
/// enum of all item types in the game
/// </summary>
public enum ItemType
{
    None, HealthPotion, /*PizzaCutterQuestItem, HolyGrailQuestItem, */EnergyShield, FireSpray,
    KeyPartPickupHandle, KeyPartPickupShaft, KeyPartPickupBit, Key
}

/// <summary>
/// 
/// </summary>
public class Item
{
    public ItemType itemType;
    //durability, stats, min-max lvl requirements, type of damage, enchants, etc

    /// <summary>
    /// constructor
    /// </summary>
    /// <param name="item">item</param>
    public Item(ItemType item)
    {
        itemType = item;

        //set item properties based on item type
        if (item == ItemType.HealthPotion)
        {
            HealthRestored = 50f;
        }
        else if (item == ItemType.EnergyShield)
        {
            Durability = 100f;
            ItemTimer = 10f;
        }
        else if (item == ItemType.FireSpray)
        {
            NumberOfUses = 10;
            Damage = 10f;
        }
        else if (item == ItemType.KeyPartPickupHandle)
        {
            //Debug.Log("key part handle");
        }
        else if (item == ItemType.KeyPartPickupShaft)
        {
            //Debug.Log("key part shaft");
        }
        else if (item == ItemType.KeyPartPickupBit)
        {
            //Debug.Log("key part bit");
        }
        else if (item == ItemType.Key)
        {

        }
    }

    /// <summary>
    /// Item's number of uses
    /// </summary>
    public int NumberOfUses
    { get; set; }

    /// <summary>
    /// Item's durability
    /// </summary>
    public float Durability
    { get; set; }

    /// <summary>
    /// Item's damage output
    /// </summary>
    public float Damage
    { get; set; }

    /// <summary>
    /// Item's health restoration
    /// </summary>
    public float HealthRestored
    { get; set; }

    /// <summary>
    /// Item expiration timer in seconds
    /// </summary>
    public float ItemTimer
    { get; set; }
}
