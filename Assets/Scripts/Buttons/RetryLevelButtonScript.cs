using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RetryLevelButtonScript : MonoBehaviour
{

	public void OnRetryLevel()
    {
        //GameManager.Instance.Player.GetComponent<Player>().PlayerHealth = Constants.PLAYER_RESPAWN_HEALTH_AMOUNT;
        MySceneManager.Instance.ChangeScene(MySceneManager.Instance.PreviousScene);
    }
}
