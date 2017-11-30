using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DefaultButtonScript : MonoBehaviour
{

	// Use this for initialization
	void Start ()
    {
        if (InputManager.Instance.IsControllerConnected)
        {
            GetComponent<Button>().Select();
        }
    }
	
	//// Update is called once per frame
	//void Update () {
		
	//}
}
