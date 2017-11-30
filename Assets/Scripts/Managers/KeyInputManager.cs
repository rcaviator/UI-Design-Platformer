//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using UnityEngine;

//public enum KeyFunction
//{
//    /*MoveUp, MoveDown, */MoveLeft, MoveRight, Jump, Interact
//}

//class KeyInputManager
//{
//    #region Fields

//    //singleton instance
//    static KeyInputManager instance;

//    Dictionary<KeyFunction, KeyCode> currentSettings;
//    Dictionary<KeyFunction, KeyCode> wasdSettings;
//    Dictionary<KeyFunction, KeyCode> ijklSettings;
//    Dictionary<KeyFunction, KeyCode> arrowSettings;

//    #endregion

//    #region Constructor

//    /// <summary>
//    /// Private constructor
//    /// </summary>
//    private KeyInputManager()
//    {
//        //wasd settings
//        wasdSettings = new Dictionary<KeyFunction, KeyCode>()
//        {
//            { KeyFunction.Interact, KeyCode.F },
//            { KeyFunction.Jump, KeyCode.Space },
//            { KeyFunction.MoveLeft, KeyCode.A },
//            { KeyFunction.MoveRight, KeyCode.D }
//        };

//        ijklSettings = new Dictionary<KeyFunction, KeyCode>()
//        {
//            { KeyFunction.Interact, KeyCode.H },
//            { KeyFunction.Jump, KeyCode.Space },
//            { KeyFunction.MoveLeft, KeyCode.J },
//            { KeyFunction.MoveRight, KeyCode.L }
//        };

//        arrowSettings = new Dictionary<KeyFunction, KeyCode>()
//        {
//            { KeyFunction.Interact, KeyCode.LeftControl },
//            { KeyFunction.Jump, KeyCode.Space },
//            { KeyFunction.MoveLeft, KeyCode.LeftArrow },
//            { KeyFunction.MoveRight, KeyCode.RightArrow }
//        };

//        currentSettings = wasdSettings;

//        if (Input.GetJoystickNames().Length > 0)
//        {
//            //Debug.Log(Input.GetJoystickNames()[0]);
//            IsControllerConnected = true;
//        }
//    }

//    #endregion

//    #region Properties

//    /// <summary>
//    /// Gets the singleton instance of the key input manager
//    /// </summary>
//    public static KeyInputManager Instance
//    {
//        get { return instance ?? (instance = new KeyInputManager()); }
//    }

//    #endregion

//    #region Properties

//    /// <summary>
//    /// gets the current keyboard settings
//    /// </summary>
//    public Dictionary<KeyFunction, KeyCode> GetCurrentSettings
//    { get { return currentSettings; } }


//    public bool IsControllerConnected
//    { get; set; }

//    #endregion

//    #region Methods

//    /// <summary>
//    /// wasd settings
//    /// </summary>
//    public void ChangeToWASD()
//    {
//        currentSettings = wasdSettings;
//    }

//    /// <summary>
//    /// ijkl settings
//    /// </summary>
//    public void ChangeToIJKL()
//    {
//        currentSettings = ijklSettings;
//    }

//    /// <summary>
//    /// arrow settings
//    /// </summary>
//    public void ChangeToArrows()
//    {
//        currentSettings = arrowSettings;
//    }

//    /// <summary>
//    /// gets the name of the current controlls
//    /// </summary>
//    /// <returns>the name of the controlls</returns>
//    public string GetCurrentSettingsName()
//    {
//        if (currentSettings == wasdSettings)
//        {
//            return "WASD controlls";
//        }
//        else if (currentSettings == ijklSettings)
//        {
//            return "IJKL controlls";
//        }
//        else
//        {
//            return "Arrow controlls";
//        }
//    }

//    #endregion
//}