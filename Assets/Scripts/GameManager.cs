using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;
using UnityEngine.InputSystem;

public class GameManager: MonoBehaviour {


        #region Singleton

        public static GameManager instance;

        void Start() {
                if (instance != null)
                        return;
                instance = this;
                
                DontDestroyOnLoad(gameObject);
        }
        
        #endregion

}
