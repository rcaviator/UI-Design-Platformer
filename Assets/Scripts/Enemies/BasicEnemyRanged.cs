using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicEnemyRanged : MonoBehaviour
{
    [SerializeField]
    GameObject explosion;
    [SerializeField]
    GameObject healthPotion;

    AudioSource audioSource;

    // Use this for initialization
    void Start ()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.clip = AudioManager.Instance.GetSoundEffect(GamePlaySoundEffect.EnemyDeath);
    }
	
	// Update is called once per frame
	void Update ()
    {
		
	}
}
