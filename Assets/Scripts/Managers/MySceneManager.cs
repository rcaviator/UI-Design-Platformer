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
            { PlayerSceneLocations.Village, new Vector3(-2f, -3f, 0f) },
            { PlayerSceneLocations.Quest1, new Vector3(0f, 0f, 0f) },
            { PlayerSceneLocations.Quest2, new Vector3(0f, 0f, 0f) },
            { PlayerSceneLocations.Quest3, new Vector3(0f, 0f, 0f) },
            { PlayerSceneLocations.QuestMain, new Vector3(0f, 0f, 0f) },
            { PlayerSceneLocations.Boss1, new Vector3(0f, 0f, 0f) },
            { PlayerSceneLocations.BossFinal, new Vector3(0f, 0f, 0f) },
            { PlayerSceneLocations.Defeat, new Vector3(0f, 0f, 0f) },
            { PlayerSceneLocations.Victory, new Vector3(0f, 0f, 0f) },
            { PlayerSceneLocations.TestLevel, new Vector3(-7f, -2f, 0f) },
        };

        //register scene change delegate
        SceneManager.sceneLoaded += OnLevelLoaded;
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
        Scenes currScene;

        #region Get scene reference

        //get scene value and set scene enum
        if (scene.name == Scenes.MainMenu.ToString())
        {
            currScene = Scenes.MainMenu;
        }
        else if (scene.name == Scenes.Credits.ToString())
        {
            currScene = Scenes.Credits;
        }
        else if (scene.name == Scenes.Options.ToString())
        {
            currScene = Scenes.Options;
        }
        else if (scene.name == Scenes.HowToPlay.ToString())
        {
            currScene = Scenes.HowToPlay;
        }
        else if (scene.name == Scenes.Tutorial.ToString())
        {
            currScene = Scenes.Tutorial;
        }
        else if (scene.name == Scenes.Village.ToString())
        {
            currScene = Scenes.Village;
        }
        else if (scene.name == Scenes.Quest1.ToString())
        {
            currScene = Scenes.Quest1;
        }
        else if (scene.name == Scenes.Quest2.ToString())
        {
            currScene = Scenes.Quest2;
        }
        else if (scene.name == Scenes.Quest3.ToString())
        {
            currScene = Scenes.Quest3;
        }
        else if (scene.name == Scenes.QuestMain.ToString())
        {
            currScene = Scenes.QuestMain;
        }
        else if (scene.name == Scenes.Boss1.ToString())
        {
            currScene = Scenes.Boss1;
        }
        else if (scene.name == Scenes.BossFinal.ToString())
        {
            currScene = Scenes.BossFinal;
        }
        else if (scene.name == Scenes.TestLevel.ToString())
        {
            currScene = Scenes.TestLevel;
        }
        else if (scene.name == Scenes.Defeat.ToString())
        {
            currScene = Scenes.Defeat;
        }
        else if (scene.name == Scenes.Victory.ToString())
        {
            currScene = Scenes.Victory;
        }
        else
        {
            currScene = Scenes.None;
        }

        #endregion

        #region Adjust soundtrack

        //change soundtracks if needed
        if (soundtrackDict.ContainsKey(currScene))
        {
            if (!(soundtrackDict[currScene] == AudioManager.Instance.WhatMusicIsPlaying()))
            {
                AudioManager.Instance.PlayMusic(soundtrackDict[currScene]);
            }
        }
        else
        {
            Debug.Log("MySceneManager sountrackDict does not contain key " + currScene.ToString() + " for changing sountracks!");
        }

        #endregion

        #region Player position

        //handle moving the player or destroying it
        switch (currScene)
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
                GameManager.Instance.Player.GetComponent<Player>().SetPlayerLocation(playerLocations[PlayerSceneLocations.Tutorial]);
                break;
            //set player, possible tutorial skip
            case Scenes.Village:
                if (!GameManager.Instance.Player)
                {
                    MonoBehaviour.Instantiate(Resources.Load<GameObject>("Prefabs/Player"), Vector3.zero, Quaternion.identity);
                }
                GameManager.Instance.Player.GetComponent<Player>().SetPlayerLocation(playerLocations[PlayerSceneLocations.Village]);
                break;
            //set player
            case Scenes.Quest1:
                GameManager.Instance.Player.GetComponent<Player>().SetPlayerLocation(playerLocations[PlayerSceneLocations.Quest1]);
                break;
            //set player
            case Scenes.Quest2:
                GameManager.Instance.Player.GetComponent<Player>().SetPlayerLocation(playerLocations[PlayerSceneLocations.Quest2]);
                break;
            //set player
            case Scenes.Quest3:
                GameManager.Instance.Player.GetComponent<Player>().SetPlayerLocation(playerLocations[PlayerSceneLocations.Quest3]);
                break;
            //set player
            case Scenes.QuestMain:
                GameManager.Instance.Player.GetComponent<Player>().SetPlayerLocation(playerLocations[PlayerSceneLocations.QuestMain]);
                break;
            //set player
            case Scenes.Boss1:
                GameManager.Instance.Player.GetComponent<Player>().SetPlayerLocation(playerLocations[PlayerSceneLocations.Boss1]);
                break;
            //set player
            case Scenes.BossFinal:
                GameManager.Instance.Player.GetComponent<Player>().SetPlayerLocation(playerLocations[PlayerSceneLocations.BossFinal]);
                break;
            //play player death animation
            case Scenes.Defeat:

                break;
            //play player victory animation
            case Scenes.Victory:

                break;
            //test level, set player
            case Scenes.TestLevel:
                if (!GameManager.Instance.Player)
                {
                    MonoBehaviour.Instantiate(Resources.Load<GameObject>("Prefabs/Player"), Vector3.zero, Quaternion.identity);
                }
                GameManager.Instance.Player.GetComponent<Player>().SetPlayerLocation(playerLocations[PlayerSceneLocations.TestLevel]);
                break;
            default:
                Debug.Log("Error in MySceneManager setting location/changing the game!");
                break;
        }

        #endregion
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
