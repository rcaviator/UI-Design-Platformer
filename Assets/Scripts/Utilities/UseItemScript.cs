using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class UseItemScript : MonoBehaviour, IPointerEnterHandler, ISelectHandler, IDeselectHandler
{
    GameObject frameImageReference;
    GameObject itemFrame;

    [SerializeField]
    bool isCraftButton;

    //bool craftingWasClickedOn = false;

    //static int craftItemCount = 0;

    bool craftableItemSelected = false;

    private void Start()
    {
        frameImageReference = Resources.Load<GameObject>("Prefabs/ItemFrameUI");
        itemFrame = Instantiate(frameImageReference, transform.position, Quaternion.identity, transform);
        itemFrame.SetActive(false);
    }

    public void SetCraftableItemSelect(bool select)
    {
        craftableItemSelected = select;
        if (!craftableItemSelected)
        {
            itemFrame.SetActive(false);
        }
    }

    public void OnUseItemClick()
    {
        UIManager.Instance.PlayerCharacterMenuCanvas.UseItem(gameObject.GetComponent<Image>());
    }

    public void OnUseCraftItemClick()
    {
        if (GetComponent<Image>().color == Color.white)
        {
            craftableItemSelected = true;
            UIManager.Instance.PlayerCharacterMenuCanvas.UseCraftItem(gameObject.GetComponent<Image>());
            AudioManager.Instance.PlayUISoundEffect(UISoundEffect.GameSelect);
        }
        else
        {
            AudioManager.Instance.PlayUISoundEffect(UISoundEffect.GameExit);
        }
    }

    public void OnCraftClick()
    {
        UIManager.Instance.PlayerCharacterMenuCanvas.CraftItem();
        AudioManager.Instance.PlayUISoundEffect(UISoundEffect.GameStart);
    }

    //unity events
    public void OnPointerEnter(PointerEventData eventData)
    {
        GetComponent<Button>().Select();
    }

    public void OnSelect(BaseEventData data)
    {
        if (!isCraftButton)
        {
            itemFrame.SetActive(true);
        }
        AudioManager.Instance.PlayUISoundEffect(UISoundEffect.MenuButtonFocused);
    }

    public void OnDeselect(BaseEventData data)
    {
        if (!craftableItemSelected)
        {
            itemFrame.SetActive(false);
        }
    }
}
