using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class PickupItem : PauseableObject
{
    protected Item self;
    bool hasBeenAdded = false;


    protected override void Awake()
    {
        base.Awake();
        //extra
    }

    /// <summary>
    /// returns itself
    /// </summary>
    /// <returns>the Item type of self</returns>
    public Item GetSelf()
    {
        return self;
    }

    /// <summary>
    /// Unity on collision enter 2d event
    /// </summary>
    /// <param name="collision">collision object</param>
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player" && !hasBeenAdded)
        {
            hasBeenAdded = true;
            GameManager.Instance.Player.GetComponent<Player>().PlayerInventory.AddItem(self);
            AudioManager.Instance.PlayGamePlaySoundEffect(GamePlaySoundEffect.ItemPickup);
            Destroy(gameObject);
        }
    }
}
