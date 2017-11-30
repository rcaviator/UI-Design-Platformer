using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyPartPickup : PickupItem
{
	// Use this for initialization
	protected override void Awake()
    {
        base.Awake();

        if (gameObject.GetComponent<SpriteRenderer>().name == "KeyHandle")
        {
            self = new Item(ItemType.KeyPartPickupHandle);
        }
        else if (gameObject.GetComponent<SpriteRenderer>().name == "KeyShaft")
        {
            self = new Item(ItemType.KeyPartPickupShaft);
        }
        else if (gameObject.GetComponent<SpriteRenderer>().name == "KeyBit")
        {
            self = new Item(ItemType.KeyPartPickupBit);
        }
    }
}
