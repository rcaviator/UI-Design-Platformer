using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

/// <summary>
/// enum of all the scenes in the game
/// </summary>
public enum Scenes
{
    //main scenes
    None, MainMenu, Credits, Options, HowToPlay, Tutorial, Village,
    Quest1, Quest2, Quest3, QuestMain, Boss1, BossFinal, Defeat, Victory,

    //test scenes
    TestLevel,
};

/// <summary>
/// enum for all player starting locations per scene
/// </summary>
public enum PlayerSceneLocations
{
    None, Tutorial, Village, Quest1, Quest2, Quest3, QuestMain, Boss1,
    BossFinal, Defeat, Victory,

    TestLevel,
};

/// <summary>
/// MySceneManager is a singleton class for handling all scene information
/// </summary>
class MySceneManager
{
    #region Fields

    //singleton instance
    static MySceneManager instance;

    //reference to event system object
    EventSystem eventSystem;

    //dictionary of SceneData
    Dictionary<Scenes, string> sceneDict;

    //dictionary to hold what soundtrack to play per scene
    Dictionary<Scenes, MusicSoundEffect> soundtrackDict;

    //dictionary to hold player starting locations in each game scene
    Dictionary<PlayerSceneLocations, Vector3> playerLocations;

    #endregion

    #region Constructor

    /// <summary>
    /// constructor
    /// </summary>
    private MySceneManager()
    {
        //initiallize scene dictionary
        sceneDict = new Dictionary<Scenes, string>()
        {
            { Scenes.MainMenu, "MainMenu" },
            { Scenes.Credits, "Credits" },
            { Scenes.Options, "Options" },
            { Scenes.HowToPlay, "HowToPlay" },
            { Scenes.Tutorial, "Tutorial" },
            { Scenes.Village, "Village" },
            { Scenes.Quest1, "Quest1" },
            { Scenes.Quest2, "Quest2" },
            { Scenes.Quest3, "Quest3" },
            { Scenes.QuestMain, "QuestMain" },
            { Scenes.Boss1, "Boss1" },
            { Scenes.BossFinal, "BossFinal" },
            { Scenes.Defeat, "Defeat" },
            { Scenes.Victory, "Victory" },
            { Scenes.TestLevel, "TestLevel" },
        };

        //initialize the soundtrack dictionary
        soundtrackDict = new Dictionary<Scenes, MusicSoundEffect>()
        {
            { Scenes.MainMenu, MusicSoundEffect.MainMenu },
            { Scenes.Credits, MusicSoundEffect.MainMenu },
            { Scenes.Options, MusicSoundEffect.MainMenu },
            { Scenes.HowToPlay, MusicSoundEffect.MainMenu },
            { Scenes.Tutorial, MusicSoundEffect.Tutorial },
            { Scenes.Village, MusicSoundEffect.Village },
            { Scenes.Quest1, MusicSoundEffect.Quest1 },
            { Scenes.Quest2, MusicSoundEffect.Quest2 },
            { Scenes.Quest3, MusicSoundEffect.Quest3 },
            { Scenes.QuestMain, MusicSoundEffect.QuestMain },
            { Scenes.Boss1, MusicSoundEffect.Boss1 },
            { Scenes.BossFinal, MusicSoundEffect.BossFinal },
            { Scenes.TestLevel, MusicSoundEffect.Test },
        };

        //initialize the player scene locations dictionary
        playerLocations = new Dictionary<PlayerSceneLocations, Vector3>()
        {
            { PlayerSceneLocations.Tutorial, new Vector3(0f, 0f, 0f) },
            { PlayerSceneLocations.Village, new Vector3(-7f, -1.8f, 0f) },
            { PlayerSceneLocations.Quest1, new Vector3(-15f, -1.8f, 0f) },
            { PlayerSceneLocations.Quest2, new Vector3(-15f, -1.8f, 0f) },
            { PlayerSceneLocations.Quest3, new Vector3(-15f, -1.8f, 0f) },
            { PlayerSceneLocations.QuestMain, new Vector3(0f, 0f, 0f) },
            { PlayerSceneLocations.Boss1, new Vector3(0f, 0f, 0f) },
            { PlayerSceneLocations.BossFinal, new Vector3(0f, 0f, 0f) },
            { PlayerSceneLocations.Defeat, new Vector3(0f, 0f, 0f) },
            { PlayerSceneLocations.Victory, new Vector3(0f, 0f, 0f) },
            { PlayerSceneLocations.TestLevel, new Vector3(-7f, -2f, 0f) },
        };

        //register scene change delegate
        SceneManager.sceneLoaded += OnLevelLoaded;

        //get reference to previous scene with delegate
        SceneManager.sceneUnloaded += OnLevelUnloaded;
    }

    #endregion

    #region Properties

    /// <summary>
    /// Gets the singleton instance of the scene manager
    /// </summary>
    public static MySceneManager Instance
    {
        get { return instance ?? (instance = new MySceneManager()); }
    }

    /// <summary>
    /// The current scene
    /// </summary>
    public Scenes CurrentScene
    { get; set; }

    /// <summary>
    /// The previous scene
    /// </summary>
    public Scenes PreviousScene
    { get; set; }

    #endregion

    #region Methods

    /// <summary>
    /// changes the scene
    /// </summary>
    /// <param name="name">the name of the scene</param>
    public void ChangeScene(Scenes name)
    {
        if (sceneDict.ContainsKey(name))
        {
            SceneManager.LoadScene(sceneDict[name]);
        }
    }

    /// <summary>
    /// Called when the scene changes and things need to update on scene change
    /// </summary>
    /// <param name="scene">the new scene</param>
    /// <param name="mode">load scene mode</param>
    void OnLevelLoaded(Scene scene, LoadSceneMode mode)
    {
        //update event system axis
        InputManager.Instance.UpdateEventSystemAxis();

        //hold reference to current scene as enum
        //Scenes currScene;

        #region Get scene reference

        //get scene value and set scene enum
        if (scene.name == Scenes.MainMenu.ToString())
        {
            CurrentScene = Scenes.MainMenu;
        }
        else if (scene.name == Scenes.Credits.ToString())
        {
            CurrentScene = Scenes.Credits;
        }
        else if (scene.name == Scenes.Options.ToString())
        {
            CurrentScene = Scenes.Options;
        }
        else if (scene.name == Scenes.HowToPlay.ToString())
        {
            CurrentScene = Scenes.HowToPlay;
        }
        else if (scene.name == Scenes.Tutorial.ToString())
        {
            CurrentScene = Scenes.Tutorial;
        }
        else if (scene.name == Scenes.Village.ToString())
        {
            CurrentScene = Scenes.Village;
        }
        else if (scene.name == Scenes.Quest1.ToString())
        {
            CurrentScene = Scenes.Quest1;
        }
        else if (scene.name == Scenes.Quest2.ToString())
        {
            CurrentScene = Scenes.Quest2;
        }
        else if (scene.name == Scenes.Quest3.ToString())
        {
            CurrentScene = Scenes.Quest3;
        }
        else if (scene.name == Scenes.QuestMain.ToString())
        {
            CurrentScene = Scenes.QuestMain;
        }
        else if (scene.name == Scenes.Boss1.ToString())
        {
            CurrentScene = Scenes.Boss1;
        }
        else if (scene.name == Scenes.BossFinal.ToString())
        {
            CurrentScene = Scenes.BossFinal;
        }
        else if (scene.name == Scenes.TestLevel.ToString())
        {
            CurrentScene = Scenes.TestLevel;
        }
        else if (scene.name == Scenes.Defeat.ToString())
        {
            CurrentScene = Scenes.Defeat;
        }
        else if (scene.name == Scenes.Victory.ToString())
        {
            CurrentScene = Scenes.Victory;
        }
        else
        {
            CurrentScene = Scenes.None;
        }

        #endregion

        #region Adjust soundtrack

        //change soundtracks if needed
        if (soundtrackDict.ContainsKey(CurrentScene))
        {
            if (!(soundtrackDict[CurrentScene] == AudioManager.Instance.WhatMusicIsPlaying()))
            {
                AudioManager.Instance.PlayMusic(soundtrackDict[CurrentScene]);
            }
        }
        else
        {
            Debug.Log("MySceneManager sountrackDict does not contain key " + CurrentScene.ToString() + " for changing sountracks!");
            AudioManager.Instance.StopMusic();
        }

        #endregion

        #region Player position

        //if death or victory scenes, stop the player from falling infinitly
        if (GameManager.Instance.Player)
        {
            GameManager.Instance.Player.GetComponent<Rigidbody2D>().simulated = false;
            GameManager.Instance.Player.GetComponent<Player>().PausePlayer = true;
        }

        //handle moving the player or destroying it
        switch (CurrentScene)
        {
            //nothing
            case Scenes.None:
                break;
            //reset game if needed
            case Scenes.MainMenu:
                GameManager.Instance.ResetGame();
                break;
            //nothing
            case Scenes.Credits:
                break;
            //nothing
            case Scenes.Options:
                break;
            //nothing
            case Scenes.HowToPlay:
                break;
            //set player, new game
            case Scenes.Tutorial:
                if (!GameManager.Instance.Player)
                {
                    MonoBehaviour.Instantiate(Resources.Load<GameObject>("Prefabs/Player"), Vector3.zero, Quaternion.identity);
                }
                GameManager.Instance.Player.GetComponent<Rigidbody2D>().simulated = true;
                GameManager.Instance.Player.GetComponent<Player>().PausePlayer = false;
                GameManager.Instance.Player.GetComponent<Player>().SetPlayerLocation(playerLocations[PlayerSceneLocations.Tutorial]);
                break;
            //set player, possible tutorial skip
            case Scenes.Village:
                if (!GameManager.Instance.Player)
                {
                    MonoBehaviour.Instantiate(Resources.Load<GameObject>("Prefabs/Player"), Vector3.zero, Quaternion.identity);
                }
                GameManager.Instance.Player.GetComponent<Rigidbody2D>().simulated = true;
                GameManager.Instance.Player.GetComponent<Player>().PausePlayer = false;
                GameManager.Instance.Player.GetComponent<Player>().SetPlayerLocation(playerLocations[PlayerSceneLocations.Village]);
                break;
            //set player
            case Scenes.Quest1:
                GameManager.Instance.Player.GetComponent<Rigidbody2D>().simulated = true;
                GameManager.Instance.Player.GetComponent<Player>().PausePlayer = false;
                GameManager.Instance.Player.GetComponent<Player>().SetPlayerLocation(playerLocations[PlayerSceneLocations.Quest1]);
                break;
            //set player
            case Scenes.Quest2:
                GameManager.Instance.Player.GetComponent<Rigidbody2D>().simulated = true;
                GameManager.Instance.Player.GetComponent<Player>().PausePlayer = false;
                GameManager.Instance.Player.GetComponent<Player>().SetPlayerLocation(playerLocations[PlayerSceneLocations.Quest2]);
                break;
            //set player
            case Scenes.Quest3:
                GameManager.Instance.Player.GetComponent<Rigidbody2D>().simulated = true;
                GameManager.Instance.Player.GetComponent<Player>().PausePlayer = false;
                GameManager.Instance.Player.GetComponent<Player>().SetPlayerLocation(playerLocations[PlayerSceneLocations.Quest3]);
                break;
            //set player
            case Scenes.QuestMain:
                GameManager.Instance.Player.GetComponent<Rigidbody2D>().simulated = true;
                GameManager.Instance.Player.GetComponent<Player>().PausePlayer = false;
                GameManager.Instance.Player.GetComponent<Player>().SetPlayerLocation(playerLocations[PlayerSceneLocations.QuestMain]);
                break;
            //set player
            case Scenes.Boss1:
                GameManager.Instance.Player.GetComponent<Rigidbody2D>().simulated = true;
                GameManager.Instance.Player.GetComponent<Player>().PausePlayer = false;
                GameManager.Instance.Player.GetComponent<Player>().SetPlayerLocation(playerLocations[PlayerSceneLocations.Boss1]);
                break;
            //set player
            case Scenes.BossFinal:
                GameManager.Instance.Player.GetComponent<Rigidbody2D>().simulated = true;
                GameManager.Instance.Player.GetComponent<Player>().PausePlayer = false;
                GameManager.Instance.Player.GetComponent<Player>().SetPlayerLocation(playerLocations[PlayerSceneLocations.BossFinal]);
                break;
            //play player death animation
            case Scenes.Defeat:
                GameManager.Instance.Player.GetComponent<Player>().PlayerHealth = Constants.PLAYER_RESPAWN_HEALTH_AMOUNT;
                GameManager.Instance.Player.GetComponent<Player>().SetPlayerLocation(playerLocations[PlayerSceneLocations.Defeat]);
                GameManager.Instance.Player.GetComponent<Rigidbody2D>().simulated = false;
                GameManager.Instance.Player.GetComponent<Player>().PausePlayer = true;
                //GameManager.Instance.ResetGame();
                break;
            //play player victory animation
            case Scenes.Victory:
                GameManager.Instance.Player.GetComponent<Player>().SetPlayerLocation(playerLocations[PlayerSceneLocations.Victory]);
                GameManager.Instance.Player.GetComponent<Rigidbody2D>().simulated = false;
                GameManager.Instance.Player.GetComponent<Player>().PausePlayer = true;
                break;
            //test level, set player
            case Scenes.TestLevel:
                if (!GameManager.Instance.Player)
                {
                    MonoBehaviour.Instantiate(Resources.Load<GameObject>("Prefabs/Player"), Vector3.zero, Quaternion.identity);
                }
                GameManager.Instance.Player.GetComponent<Rigidbody2D>().simulated = true;
                GameManager.Instance.Player.GetComponent<Player>().PausePlayer = false;
                GameManager.Instance.Player.GetComponent<Player>().SetPlayerLocation(playerLocations[PlayerSceneLocations.TestLevel]);
                break;
            default:
                Debug.Log("Error in MySceneManager setting location/changing the game!");
                break;
        }

        #endregion
    }


    void OnLevelUnloaded(Scene scene)
    {
        //get scene value and set scene enum
        if (scene.name == Scenes.MainMenu.ToString())
        {
            PreviousScene = Scenes.MainMenu;
        }
        else if (scene.name == Scenes.Credits.ToString())
        {
            PreviousScene = Scenes.Credits;
        }
        else if (scene.name == Scenes.Options.ToString())
        {
            PreviousScene = Scenes.Options;
        }
        else if (scene.name == Scenes.HowToPlay.ToString())
        {
            PreviousScene = Scenes.HowToPlay;
        }
        else if (scene.name == Scenes.Tutorial.ToString())
        {
            PreviousScene = Scenes.Tutorial;
        }
        else if (scene.name == Scenes.Village.ToString())
        {
            PreviousScene = Scenes.Village;
        }
        else if (scene.name == Scenes.Quest1.ToString())
        {
            PreviousScene = Scenes.Quest1;
        }
        else if (scene.name == Scenes.Quest2.ToString())
        {
            PreviousScene = Scenes.Quest2;
        }
        else if (scene.name == Scenes.Quest3.ToString())
        {
            PreviousScene = Scenes.Quest3;
        }
        else if (scene.name == Scenes.QuestMain.ToString())
        {
            PreviousScene = Scenes.QuestMain;
        }
        else if (scene.name == Scenes.Boss1.ToString())
        {
            PreviousScene = Scenes.Boss1;
        }
        else if (scene.name == Scenes.BossFinal.ToString())
        {
            PreviousScene = Scenes.BossFinal;
        }
        else if (scene.name == Scenes.TestLevel.ToString())
        {
            PreviousScene = Scenes.TestLevel;
        }
        else if (scene.name == Scenes.Defeat.ToString())
        {
            PreviousScene = Scenes.Defeat;
        }
        else if (scene.name == Scenes.Victory.ToString())
        {
            PreviousScene = Scenes.Victory;
        }
        else
        {
            PreviousScene = Scenes.None;
        }
    }

    /// <summary>
    /// Used for updating the scene
    /// </summary>
    /// <param name="name"></param>
    /// <param name="entity"></param>
    public void UpdateScene(Scenes name, GameObject entity)
    {

    }

    #endregion

    #region SceneData

    class SceneData
    {
        //enemies, items, chests, environmental
    }

    #endregion
}
