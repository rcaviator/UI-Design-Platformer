using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class PlayerFireButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public void OnPlayerFireButtonPress()
    {
        //Debug.Log("firing");
    }

    public void OnPointerDown(PointerEventData data)
    {
        //Debug.Log("down");
        GameManager.Instance.Player.GetComponent<Player>().InputPlayerShoot = true;
    }

    public void OnPointerUp(PointerEventData data)
    {
        //Debug.Log("up");
        GameManager.Instance.Player.GetComponent<Player>().InputPlayerShoot = false;
    }
}
