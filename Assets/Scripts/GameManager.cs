using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class GameManager: MonoBehaviour {

        public static GameManager instance;

        void Start() {
                if (instance != null)
                        return;
                instance = this;
                
                DontDestroyOnLoad(gameObject);
        }
        public string ReadJSON(FileInfo jsonFile) {
                string json = File.ReadAllText(jsonFile.FullName);
                return json;
        }
}
