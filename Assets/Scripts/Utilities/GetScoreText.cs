using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GetScoreText : MonoBehaviour
{

	// Use this for initialization
	void Start ()
    {
        GetComponent<Text>().text = "Your Score was: " + GameManager.Instance.Score.ToString();
	}
}
