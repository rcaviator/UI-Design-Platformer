using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

/// <summary>
/// Music enum
/// </summary>
public enum MusicSoundEffect
{
    None, Test, MainMenu, Tutorial, Village, Quest1, Quest2, Quest3, QuestMain,
    Boss1, BossFinal, Victory,
}

/// <summary>
/// UI sound effects enum
/// </summary>
public enum UISoundEffect
{
    None, Test, MenuButtonFocused, MenuForward, MenuBack, GameSelect, GameStart, GameExit,
    GameSavedButton, GamePaused,

    //in player menu sounds
    UseHealthPotion, UseShield, UseKey,
}

/// <summary>
/// Game play sound effects enum
/// </summary>
public enum GamePlaySoundEffect
{
    //player
    PlayerFootsteps, PlayerJump, PlayerLand, PlayerEdge, PlayerClimb, PlayerAttack, PlayerHurt, PlayerDeath,
    PlayerHitWall,

    //enemies
    EnemyFootsteps, EnemyJump, EnemyLand, EnemyHurt, EnemyDeath, EnemyAttack,
    BossFootsteps, BossJump, BossLand, BossHurt, BossDeath, BossAttack, EnemyProjectile,
    
    //npcs

    //items
    ItemPickup, ItemUse, ItemExpired, ItemCombine, ItemCrafted, ItemAttack, ItemHit,
    
    //environment
    Explosion1, Explosion2, Explosion3, Explosion4, Explosion5, Explosion6,
    Teleport,
    
    //bullets
    ProjectileHitWall,
    
    //game
    //GameWin, GameLose
};

/// <summary>
/// Class AudioManager is the singleton that handles all audio in the game
/// </summary>
class AudioManager
{
    #region Fields

    //singleton instance
    static AudioManager instance;

    //music, ui, and game sound effects dictionaries
    Dictionary<MusicSoundEffect, AudioClip> musicSoundEffectsDict;
    Dictionary<UISoundEffect, AudioClip> uiSoundEffectsDict;
    Dictionary<GamePlaySoundEffect, AudioClip> gamePlaySoundEffectsDict;

    //game object for audio sources
    GameObject audioController;

    //audio source references
    AudioSource backgroundMusicAudioSource;
    AudioSource UIAudioSource;
    AudioSource gamePlaySoundEffectsAudioSource;

    //reference to currently playing background music
    MusicSoundEffect currentMusic;
    
    #endregion

    #region Constructor

    //private constructor
    private AudioManager()
    {
        //create and populate the music dictionary
        musicSoundEffectsDict = new Dictionary<MusicSoundEffect, AudioClip>()
        {
            { MusicSoundEffect.Test, Resources.Load<AudioClip>("Audio/Music/IceStation") },
            { MusicSoundEffect.MainMenu, Resources.Load<AudioClip>("Audio/Music/TitleScreen") },
            { MusicSoundEffect.Tutorial, Resources.Load<AudioClip>("Audio/Music/TrainingGround") },
            { MusicSoundEffect.Village, Resources.Load<AudioClip>("Audio/Music/Village") },
            { MusicSoundEffect.Quest1, Resources.Load<AudioClip>("Audio/Music/WildWest") },
            { MusicSoundEffect.Quest2, Resources.Load<AudioClip>("Audio/Music/Spaceship") },
            { MusicSoundEffect.Quest3, Resources.Load<AudioClip>("Audio/Music/ReturnToPlanetX") },
            { MusicSoundEffect.QuestMain, Resources.Load<AudioClip>("Audio/Music/Hanger") },
            { MusicSoundEffect.Boss1, Resources.Load<AudioClip>("Audio/Music/Docks") },
            { MusicSoundEffect.BossFinal, Resources.Load<AudioClip>("Audio/Music/Warzone") },
            { MusicSoundEffect.Victory, Resources.Load<AudioClip>("Audio/Music/IceStation") },
        };

        //create and populate the UI dictionary
        uiSoundEffectsDict = new Dictionary<UISoundEffect, AudioClip>()
        {
            { UISoundEffect.Test, Resources.Load<AudioClip>("Audio/UI/nocandonu_02") },
            { UISoundEffect.MenuButtonFocused, Resources.Load<AudioClip>("Audio/UI/ButtonActionButtonFocused") },
            { UISoundEffect.MenuForward, Resources.Load<AudioClip>("Audio/UI/ButtonActionForward") },
            { UISoundEffect.MenuBack, Resources.Load<AudioClip>("Audio/UI/ButtonActionBack") },
            { UISoundEffect.GameSelect, Resources.Load<AudioClip>("Audio/UI/ButtonActionSelect") },
            { UISoundEffect.GameStart, Resources.Load<AudioClip>("Audio/UI/ButtonActionEngage") },
            { UISoundEffect.GameSavedButton, Resources.Load<AudioClip>("Audio/UI/") },
            { UISoundEffect.GameExit, Resources.Load<AudioClip>("Audio/UI/ButtonActionNegative") },
            { UISoundEffect.GamePaused, Resources.Load<AudioClip>("Audio/UI/PauseGame") },
            { UISoundEffect.UseHealthPotion, Resources.Load<AudioClip>("Audio/Effects/UseHealthPotion") },
            { UISoundEffect.UseShield, Resources.Load<AudioClip>("Audio/Effects/UseShield") },
            { UISoundEffect.UseKey, Resources.Load<AudioClip>("Audio/") },
        };

        //create and populate the game play sound effects dictionary
        //effectsDict = (Resources.LoadAll<AudioClip>("Audio/Sounds")).ToDictionary(s => s.name);
        gamePlaySoundEffectsDict = new Dictionary<GamePlaySoundEffect, AudioClip>()
        {
            { GamePlaySoundEffect.PlayerFootsteps, Resources.Load<AudioClip>("Audio/Effects/PlayerFootstep")},
            { GamePlaySoundEffect.PlayerJump, Resources.Load<AudioClip>("Audio/Effects/PlayerJump")},
            { GamePlaySoundEffect.PlayerLand, Resources.Load<AudioClip>("Audio/Effects/PlayerLand")},
            { GamePlaySoundEffect.PlayerEdge, Resources.Load<AudioClip>("")},
            { GamePlaySoundEffect.PlayerClimb, Resources.Load<AudioClip>("")},
            { GamePlaySoundEffect.PlayerAttack, Resources.Load<AudioClip>("Audio/Effects/PlayerAttack")},
            { GamePlaySoundEffect.PlayerHurt, Resources.Load<AudioClip>("Audio/Effects/PlayerHurt")},
            { GamePlaySoundEffect.PlayerDeath, Resources.Load<AudioClip>("")},
            { GamePlaySoundEffect.PlayerHitWall, Resources.Load<AudioClip>("")},
            { GamePlaySoundEffect.EnemyFootsteps, Resources.Load<AudioClip>("")},
            { GamePlaySoundEffect.EnemyJump, Resources.Load<AudioClip>("")},
            { GamePlaySoundEffect.EnemyLand, Resources.Load<AudioClip>("")},
            { GamePlaySoundEffect.EnemyDeath, Resources.Load<AudioClip>("Audio/Effects/EnemyDeath")},
            { GamePlaySoundEffect.EnemyAttack, Resources.Load<AudioClip>("")},
            { GamePlaySoundEffect.BossFootsteps, Resources.Load<AudioClip>("")},
            { GamePlaySoundEffect.BossJump, Resources.Load<AudioClip>("")},
            { GamePlaySoundEffect.BossLand, Resources.Load<AudioClip>("")},
            { GamePlaySoundEffect.BossHurt, Resources.Load<AudioClip>("")},
            { GamePlaySoundEffect.BossDeath, Resources.Load<AudioClip>("")},
            { GamePlaySoundEffect.BossAttack, Resources.Load<AudioClip>("")},
            { GamePlaySoundEffect.EnemyProjectile, Resources.Load<AudioClip>("Audio/Effects/EnemyFireball") },
            { GamePlaySoundEffect.ItemPickup, Resources.Load<AudioClip>("Audio/Effects/PickupItem")},
            { GamePlaySoundEffect.ItemUse, Resources.Load<AudioClip>("")},
            { GamePlaySoundEffect.ItemExpired, Resources.Load<AudioClip>("")},
            { GamePlaySoundEffect.ItemCombine, Resources.Load<AudioClip>("")},
            { GamePlaySoundEffect.ItemCrafted, Resources.Load<AudioClip>("")},
            { GamePlaySoundEffect.ItemAttack, Resources.Load<AudioClip>("")},
            { GamePlaySoundEffect.ItemHit, Resources.Load<AudioClip>("")},
            { GamePlaySoundEffect.Explosion1, Resources.Load<AudioClip>("Audio/Effects/Blast1") },
            { GamePlaySoundEffect.Explosion2, Resources.Load<AudioClip>("Audio/Effects/Blast2") },
            { GamePlaySoundEffect.Explosion3, Resources.Load<AudioClip>("Audio/Effects/Blast3") },
            { GamePlaySoundEffect.Explosion4, Resources.Load<AudioClip>("Audio/Effects/Blast4") },
            { GamePlaySoundEffect.Explosion5, Resources.Load<AudioClip>("Audio/Effects/Blast5") },
            { GamePlaySoundEffect.Explosion6, Resources.Load<AudioClip>("Audio/Effects/Blast6") },
            { GamePlaySoundEffect.Teleport, Resources.Load<AudioClip>("Audio/Effects/PlayerTeleportSpawn") },
            { GamePlaySoundEffect.ProjectileHitWall, Resources.Load<AudioClip>("") },
            //{ GamePlaySoundEffect.GameWin, Resources.Load<AudioClip>("")},
            //{ GamePlaySoundEffect.GameLose, Resources.Load<AudioClip>("")},
        };

        //create audio game object
        audioController = new GameObject("AudioController");
        GameObject.DontDestroyOnLoad(audioController);

        //create audio source references
        UIAudioSource = audioController.AddComponent<AudioSource>() as AudioSource;
        backgroundMusicAudioSource = audioController.AddComponent<AudioSource>() as AudioSource;
        gamePlaySoundEffectsAudioSource = audioController.AddComponent<AudioSource>() as AudioSource;

        //set audio sources for ignore pausing
        UIAudioSource.ignoreListenerPause = true;
        backgroundMusicAudioSource.ignoreListenerPause = true;
        gamePlaySoundEffectsAudioSource.ignoreListenerPause = false;

        //test references
        //UIAudioSource.clip = soundEffects[SoundEffect.PlayerAttack];
        //backgroundMusicAudioSource.clip = soundEffects[SoundEffect.PlayerFootsteps];
        //soundEffectsAudioSource.clip = soundEffects[SoundEffect.PlayerJump];
        //UIAudioSource.Play();
        //backgroundMusicAudioSource.Play();
        //soundEffectsAudioSource.Play();
    }

    #endregion

    #region Properties

    /// <summary>
    /// Gets the singleton instance of the audio manager
    /// </summary>
    public static AudioManager Instance
    {
        get { return instance ?? (instance = new AudioManager()); }
    }

    #region Background Music

    /// <summary>
    /// Plays a background music on loop. If a track is already playing,
    /// stops current track and plays new one.
    /// </summary>
    /// <param name="name">the enum name of the track to play</param>
    public void PlayMusic(MusicSoundEffect name)
    {
        if (musicSoundEffectsDict.ContainsKey(name))
        {
            if (backgroundMusicAudioSource.isPlaying)
            {
                backgroundMusicAudioSource.Stop();
                backgroundMusicAudioSource.clip = musicSoundEffectsDict[name];
                backgroundMusicAudioSource.Play();
                backgroundMusicAudioSource.loop = true;
            }
            else
            {
                backgroundMusicAudioSource.clip = musicSoundEffectsDict[name];
                backgroundMusicAudioSource.Play();
                backgroundMusicAudioSource.loop = true;
            }

            //set current music enum
            currentMusic = name;
        }
        else
        {
            Debug.Log("Music name " + name.ToString() + " is not in the music sound effects dictionary!");
        }
    }

    /// <summary>
    /// Stops the current music
    /// </summary>
    public void StopMusic()
    {
        if (backgroundMusicAudioSource.isPlaying)
        {
            backgroundMusicAudioSource.Stop();
            currentMusic = MusicSoundEffect.None;
        }
        else
        {
            Debug.Log("No music is playing to stop.");
        }
    }

    /// <summary>
    /// Pauses the current music
    /// </summary>
    public void PauseMusic()
    {
        if (backgroundMusicAudioSource.isPlaying)
        {
            backgroundMusicAudioSource.Pause();
        }
        else
        {
            Debug.Log("No music is playing to pause.");
        }
    }

    /// <summary>
    /// Unpauses the current music
    /// </summary>
    public void UnpauseMusic()
    {
        if (!backgroundMusicAudioSource.isPlaying)
        {
            backgroundMusicAudioSource.UnPause();
        }
        else
        {
            Debug.Log("No music is paused to unpause.");
        }
    }

    /// <summary>
    /// Gets the enum of the current music playing
    /// </summary>
    /// <returns>the current music enum</returns>
    public MusicSoundEffect WhatMusicIsPlaying()
    {
        return currentMusic;
    }

    #endregion

    #region UI Sound effects

    /// <summary>
    /// Plays a UI sound effect
    /// </summary>
    /// <param name="name">the name of the sound effect</param>
    public void PlayUISoundEffect(UISoundEffect name)
    {
        if (uiSoundEffectsDict.ContainsKey(name))
        {
            UIAudioSource.PlayOneShot(uiSoundEffectsDict[name]);
        }
        else
        {
            Debug.Log("UI sound effect " + name.ToString() + " is not in the UI sound effects dictionary!");
        }
    }

    #endregion

    #region Game Play Sound Effects

    /// <summary>
    /// Plays a game play sound effect
    /// </summary>
    /// <param name="name">the name of the sound effect</param>
    public void PlayGamePlaySoundEffect(GamePlaySoundEffect name)
    {
        if (gamePlaySoundEffectsDict.ContainsKey(name))
        {
            gamePlaySoundEffectsAudioSource.PlayOneShot(gamePlaySoundEffectsDict[name]);
        }
    }

    /// <summary>
    /// Pauses the game play sound effects with AudioListener
    /// </summary>
    public void PauseGamePlaySoundEffects()
    {
        if (!AudioListener.pause)
        {
            AudioListener.pause = true;
        }
        else
        {
            Debug.Log("AudioListener is already paused.");
        }
    }

    /// <summary>
    /// Unpauses the game play sound effects with AudioListener
    /// </summary>
    public void UnpauseGamePlaySoundEffects()
    {
        if (AudioListener.pause)
        {
            AudioListener.pause = false;
        }
        else
        {
            Debug.Log("AudioListener is already unpaused.");
        }
    }

    #endregion

    /// <summary>
    /// Gets a music from the music dictionary
    /// </summary>
    /// <param name="name">the name of the music</param>
    /// <returns></returns>
    public AudioClip GetMusic(MusicSoundEffect name)
    {
        if (musicSoundEffectsDict.ContainsKey(name))
        {
            return musicSoundEffectsDict[name];
        }
        else
        {
            Debug.Log("Key " + name + " is not in the music dictionary!");
            return null;
        }
    }

    /// <summary>
    /// Gets a sound effect from the sound effect dictionary
    /// </summary>
    /// <param name="name">the name of the sound effect</param>
    /// <returns></returns>
    public AudioClip GetSoundEffect(GamePlaySoundEffect name)
    {
        if (gamePlaySoundEffectsDict.ContainsKey(name))
        {
            return gamePlaySoundEffectsDict[name];
        }
        else
        {
            Debug.Log("Key " + name + " is not in the sound effect dictionary!");
            return null;
        }
    }

    #endregion
}