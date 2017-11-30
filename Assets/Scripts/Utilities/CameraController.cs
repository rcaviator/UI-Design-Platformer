using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using jp.gulti.ColorBlind;

public class CameraController : MonoBehaviour
{
    GameObject player;
    Vector3 velocity = Vector3.zero;

    //colorblindness simulator
    ColorBlindnessSimulator sim;

	// Use this for initialization
	void Start ()
    {
        player = GameManager.Instance.Player;
        transform.position = new Vector3(player.transform.position.x, player.transform.position.y, transform.position.z);

        sim = GetComponent<ColorBlindnessSimulator>();

        if (sim == null)
        {
            sim = gameObject.AddComponent<ColorBlindnessSimulator>();
        }

        sim.enabled = false;
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (!GameManager.Instance.Paused)
        {
            GetComponent<Rigidbody2D>().MovePosition(Vector3.SmoothDamp(transform.position, new Vector3(player.transform.position.x + player.GetComponent<Rigidbody2D>().velocity.x * .5f, 
                player.transform.position.y + player.GetComponent<Rigidbody2D>().velocity.y * .3f, transform.position.z), 
                ref velocity, 0.2f));
        }
        
        //colorblindness simulator
        if (Input.GetKeyDown(KeyCode.F1))
        {
            sim.enabled = false;
            //sim.BlindMode = ColorBlindnessSimulator.ColorBlindMode.Protanope; 
        }
        else if (Input.GetKeyDown(KeyCode.F2))
        {
            sim.enabled = true;
            sim.BlindMode = ColorBlindnessSimulator.ColorBlindMode.Protanope;
        }
        else if (Input.GetKeyDown(KeyCode.F3))
        {
            sim.enabled = true;
            sim.BlindMode = ColorBlindnessSimulator.ColorBlindMode.Deuteranope;
        }
    }
}
