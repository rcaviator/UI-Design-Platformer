using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreText : MonoBehaviour
{
    Text scoreText;

    bool increaseScale = false;

    float timer = 0f;
    float animationTimer = 0.2f;
    float scaleRate = 1f;

    // Use this for initialization
    void Start ()
    {
        scoreText = GetComponent<Text>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        scoreText.text = "Score: " + GameManager.Instance.Score.ToString();

        if (increaseScale)
        {
            if (timer < animationTimer)
            {
                timer += Time.deltaTime;
                GetComponent<RectTransform>().localScale = new Vector3(GetComponent<RectTransform>().localScale.x + (scaleRate * Time.deltaTime), GetComponent<RectTransform>().localScale.y + (scaleRate * Time.deltaTime), GetComponent<RectTransform>().localScale.z);
                scoreText.color = Color.red;
            }
            else
            {
                increaseScale = false;
            }
        }
        else
        {
            if (timer > 0)
            {
                timer -= Time.deltaTime;
                GetComponent<RectTransform>().localScale = new Vector3(GetComponent<RectTransform>().localScale.x - (scaleRate * Time.deltaTime), GetComponent<RectTransform>().localScale.y - (scaleRate * Time.deltaTime), GetComponent<RectTransform>().localScale.z);
                scoreText.color = Color.white;
            }
            else
            {
                timer = 0;
                GetComponent<RectTransform>().localScale = new Vector3(1f, 1f, GetComponent<RectTransform>().localScale.z);
            }
        }
    }

    public void ScoreChange(bool size)
    {
        increaseScale = true;
    }
}
