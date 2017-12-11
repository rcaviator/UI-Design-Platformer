using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RetryLevelButtonScript : MonoBehaviour
{
    float maxDeactivatedTimer = 1f;
    float deactivatedTimer = 0f;
    bool isActivated = false;

    private void Start()
    {
        GetComponent<Button>().interactable = false;
    }

    private void Update()
    {
        if (deactivatedTimer < maxDeactivatedTimer)
        {
            deactivatedTimer += Time.deltaTime;
        }
        else
        {
            if (!isActivated)
            {
                isActivated = true;
                GetComponent<Button>().interactable = true;
            }
        }
    }

    public void OnRetryLevel()
    {
        //GameManager.Instance.Player.GetComponent<Player>().PlayerHealth = Constants.PLAYER_RESPAWN_HEALTH_AMOUNT;
        MySceneManager.Instance.ChangeScene(MySceneManager.Instance.PreviousScene);
    }
}
