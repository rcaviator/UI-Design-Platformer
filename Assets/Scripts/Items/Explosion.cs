using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : PauseableObject
{
	// Use this for initialization
	protected override void Awake()
    {
        base.Awake();

        //anim = GetComponent<Animator>();
        anim.Play("Explosion");

        int rSound = Random.Range(0, 6);

        switch (rSound)
        {
            case 0:
                AudioManager.Instance.PlayGamePlaySoundEffect(GamePlaySoundEffect.Explosion1);
                break;
            case 1:
                AudioManager.Instance.PlayGamePlaySoundEffect(GamePlaySoundEffect.Explosion2);
                break;
            case 2:
                AudioManager.Instance.PlayGamePlaySoundEffect(GamePlaySoundEffect.Explosion3);
                break;
            case 3:
                AudioManager.Instance.PlayGamePlaySoundEffect(GamePlaySoundEffect.Explosion4);
                break;
            case 4:
                AudioManager.Instance.PlayGamePlaySoundEffect(GamePlaySoundEffect.Explosion5);
                break;
            case 5:
                AudioManager.Instance.PlayGamePlaySoundEffect(GamePlaySoundEffect.Explosion6);
                break;
            default:
                break;
        }
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f)
        {
            Destroy(gameObject);
        }
    }
}
