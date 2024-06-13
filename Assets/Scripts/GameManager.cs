using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class GameManager: MonoBehaviour {

        public static GameManager instance;

        public Course currCourse = null;

        void Start() {
                if (instance != null)
                        return;
                instance = this;
                
                DontDestroyOnLoad(gameObject);
        }

        #region JSON Logic
        public string ReadJSON(FileInfo jsonFile) {
                string json = File.ReadAllText(jsonFile.FullName);
                return json;
        }

        #endregion

        #region Timer

        #endregion
}
