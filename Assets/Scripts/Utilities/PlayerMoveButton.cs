using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerMoveButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public enum MoveButtonAction
    {
        MoveLeft, MoveRight, Jump, UsePortal,
    };

    [SerializeField]
    MoveButtonAction action;

    void Start()
    {
        if (Application.platform != RuntimePlatform.Android)
        {
            gameObject.SetActive(false);
        }
    }

    public void OnPointerDown(PointerEventData data)
    {
        switch (action)
        {
            case MoveButtonAction.MoveLeft:
                GameManager.Instance.Player.GetComponent<Player>().MobileMoveLeft = true;
                break;
            case MoveButtonAction.MoveRight:
                GameManager.Instance.Player.GetComponent<Player>().MobileMoveRight = true;
                break;
            case MoveButtonAction.Jump:
                GameManager.Instance.Player.GetComponent<Player>().MobileJump = true;
                break;
            case MoveButtonAction.UsePortal:
                GameManager.Instance.Player.GetComponent<Player>().MobileUsePortal = true;
                break;
            default:
                break;
        }
    }

    public void OnPointerUp(PointerEventData data)
    {
        switch (action)
        {
            case MoveButtonAction.MoveLeft:
                GameManager.Instance.Player.GetComponent<Player>().MobileMoveLeft = false;
                break;
            case MoveButtonAction.MoveRight:
                GameManager.Instance.Player.GetComponent<Player>().MobileMoveRight = false;
                break;
            case MoveButtonAction.Jump:
                GameManager.Instance.Player.GetComponent<Player>().MobileJump = false;
                break;
            case MoveButtonAction.UsePortal:
                GameManager.Instance.Player.GetComponent<Player>().MobileUsePortal = false;
                break;
            default:
                break;
        }
    }
}
