using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class UseItemScript : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerUpHandler
{
    GameObject frameImageReference;
    GameObject itemFrame;

    [SerializeField]
    bool isCraftButton;

    bool craftingWasClickedOn = false;

    static int craftItemCount = 0;

    private void Start()
    {
        frameImageReference = Resources.Load<GameObject>("Prefabs/ItemFrameUI");
        itemFrame = Instantiate(frameImageReference, transform.position, Quaternion.identity, transform);
        itemFrame.SetActive(false);
    }

    private void Update()
    {
        if (craftItemCount >= 3 && isCraftButton)
        {
            //isCraftButton = false;
            GetComponent<Button>().interactable = true;
        }
    }

    public void OnButtonClick()
    {
        UIManager.Instance.PlayerCharacterMenuCanvas.UseItem(gameObject.GetComponent<Image>());

        //Debug.Log("inventory pointer up");
        if (gameObject.GetComponent<Image>().sprite != null)
        {
            if (!craftingWasClickedOn)
            {
                if (gameObject.GetComponent<Image>().sprite.name == "KeyHandle")
                {
                    craftItemCount++;
                    craftingWasClickedOn = true;
                    itemFrame.SetActive(true);
                }
                else if (gameObject.GetComponent<Image>().sprite.name == "KeyShaft")
                {
                    craftItemCount++;
                    craftingWasClickedOn = true;
                    itemFrame.SetActive(true);
                }
                else if (gameObject.GetComponent<Image>().sprite.name == "KeyBit")
                {
                    craftItemCount++;
                    craftingWasClickedOn = true;
                    itemFrame.SetActive(true);
                }
            }
        }
    }

    public void OnCraftClick()
    {
        Debug.Log("Craft item!");
        //craft key
        itemFrame.SetActive(false);
        craftingWasClickedOn = false;
        craftItemCount = 0;

        GameManager.Instance.Player.GetComponent<Player>().PlayerInventory.RemoveFirstItemOfType(ItemType.KeyPartPickupHandle);
        GameManager.Instance.Player.GetComponent<Player>().PlayerInventory.RemoveFirstItemOfType(ItemType.KeyPartPickupShaft);
        GameManager.Instance.Player.GetComponent<Player>().PlayerInventory.RemoveFirstItemOfType(ItemType.KeyPartPickupBit);
        GameManager.Instance.Player.GetComponent<Player>().PlayerInventory.AddItem(new Item(ItemType.Key));
        
        UIManager.Instance.PlayerCharacterMenuCanvas.ReStartUI();
    }

    //unity events
    public void OnPointerEnter(PointerEventData eventData)
    {
        if (!isCraftButton)
        {
            //Debug.Log("inventory pointer enter");
            itemFrame.SetActive(true);
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (!isCraftButton)
        {
            if (!craftingWasClickedOn)
            {
                itemFrame.SetActive(false);
            }
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        //Debug.Log("inventory pointer up");
        if (gameObject.GetComponent<Image>().sprite != null)
        {
            if (!craftingWasClickedOn)
            {
                if (gameObject.GetComponent<Image>().sprite.name == "KeyHandle")
                {
                    craftItemCount++;
                    craftingWasClickedOn = true;
                }
                else if (gameObject.GetComponent<Image>().sprite.name == "KeyShaft")
                {
                    craftItemCount++;
                    craftingWasClickedOn = true;
                }
                else if (gameObject.GetComponent<Image>().sprite.name == "KeyBit")
                {
                    craftItemCount++;
                    craftingWasClickedOn = true;
                }
            }
        }
        
        //Debug.Log(craftItemCount);
    }
}
