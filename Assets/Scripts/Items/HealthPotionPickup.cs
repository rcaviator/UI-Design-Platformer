using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPotionPickup : PickupItem
{
    // Use this for initialization
    protected override void Awake()
    {
        base.Awake();
        self = new Item(ItemType.HealthPotion);
    }
}
