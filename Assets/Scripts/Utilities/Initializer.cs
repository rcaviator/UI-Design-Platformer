using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Initializer : MonoBehaviour
{
	// Use this for initialization
	void Awake ()
    {
        //start up all managers
        GameManager.Instance.ToString();
        AudioManager.Instance.ToString();
        InputManager.Instance.ToString();
        UIManager.Instance.ToString();
        MySceneManager.Instance.ToString();
        QuestManager.Instance.ToString();
	}
}
