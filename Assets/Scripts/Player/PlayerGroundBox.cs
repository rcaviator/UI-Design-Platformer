using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGroundBox : MonoBehaviour
{

	//// Use this for initialization
	//void Start () {
		
	//}
	
	//// Update is called once per frame
	//void Update () {
		
	//}

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            GameManager.Instance.Player.GetComponent<Player>().IsGrounded = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "Ground")
        {
            GameManager.Instance.Player.GetComponent<Player>().IsGrounded = false;
        }
    }
}
