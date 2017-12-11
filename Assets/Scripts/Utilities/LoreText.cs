using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoreText : MonoBehaviour
{
	// Use this for initialization
	void Start () {
		
	}

    public void OnTextChange(string newText)
    {
        GetComponent<Text>().text = newText;
        AudioManager.Instance.PlayUISoundEffect(UISoundEffect.GameSelect);
    }
}
