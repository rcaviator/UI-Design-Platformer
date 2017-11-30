using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergyShieldPickup : PickupItem
{
	// Use this for initialization
	protected override void Awake()
    {
        base.Awake();
        self = new Item(ItemType.EnergyShield);
	}
}
