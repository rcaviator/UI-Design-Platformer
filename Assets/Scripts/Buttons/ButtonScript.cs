using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ButtonScript : MonoBehaviour, IPointerEnterHandler, ISelectHandler, IDeselectHandler
{
    [SerializeField]
    UISoundEffect buttonSound;

    [SerializeField]
    Scenes goToScene;

    bool increaseScale = false;

    float timer = 0f;
    float animationTimer = 0.2f;
    float scaleRate = 1f;

    private void Start()
    {
        
    }

    private void Update()
    {
        if (increaseScale)
        {
            if (timer < animationTimer)
            {
                timer += Time.deltaTime;
                GetComponent<RectTransform>().localScale = new Vector3(GetComponent<RectTransform>().localScale.x + (scaleRate * Time.deltaTime), GetComponent<RectTransform>().localScale.y + (scaleRate * Time.deltaTime), GetComponent<RectTransform>().localScale.z);
            }
        }
        else
        {
            if (timer > 0)
            {
                timer -= Time.deltaTime;
                GetComponent<RectTransform>().localScale = new Vector3(GetComponent<RectTransform>().localScale.x - (scaleRate * Time.deltaTime), GetComponent<RectTransform>().localScale.y - (scaleRate * Time.deltaTime), GetComponent<RectTransform>().localScale.z);
            }
            else
            {
                timer = 0;
                GetComponent<RectTransform>().localScale = new Vector3(1f, 1f, GetComponent<RectTransform>().localScale.z);
            }
        }
    }

    public void OnPointerEnter(PointerEventData pointerData)
    {
        GetComponent<Button>().Select();
    }

    public void OnSelect(BaseEventData eventData)
    {
        increaseScale = true;
        AudioManager.Instance.PlayUISoundEffect(UISoundEffect.MenuButtonFocused);
    }

    public void OnDeselect(BaseEventData eventData)
    {
        increaseScale = false;
    }

    public void OnButtonClick()
    {
        AudioManager.Instance.PlayUISoundEffect(buttonSound);
        MySceneManager.Instance.ChangeScene(goToScene);
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void ChangeToWASD()
    {
        AudioManager.Instance.PlayUISoundEffect(buttonSound);
        InputManager.Instance.ChangeToWASD();
    }

    public void ChangeToIJKL()
    {
        AudioManager.Instance.PlayUISoundEffect(buttonSound);
        InputManager.Instance.ChangeToIJKL();
    }

    public void ChangeToArrows()
    {
        AudioManager.Instance.PlayUISoundEffect(buttonSound);
        InputManager.Instance.ChangeToArrows();
    }
}
