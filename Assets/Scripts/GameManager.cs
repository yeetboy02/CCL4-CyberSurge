using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;

public class GameManager: MonoBehaviour {

        public static GameManager instance;

        #region CourseParameters

        public Course currCourse = null;

        #endregion

        #region TimerParameters

        private bool timerActive = false;
        public float currTime = 0.0f;

        #endregion 


        void Start() {
                if (instance != null)
                        return;
                instance = this;
                
                DontDestroyOnLoad(gameObject);
        }

        #region JSON Logic
        public string ReadJSON(FileInfo jsonFile) {
                return File.ReadAllText(jsonFile.FullName);
        }

        #endregion

        void Update() {
                if (timerActive) {
                        currTime += Time.deltaTime;
                }
        }

        #region Timer
        
        public void StartTimer() {
                if (!timerActive) {
                        currTime = 0.0f;
                        timerActive = true;
                }
        }

        public void StopTimer() {
                if (timerActive) {
                        timerActive = false;
                        Debug.Log("Time: " + ConvertTime(currTime));
                }
        }

        public string ConvertTime(float floatTime) {
                TimeSpan time = TimeSpan.FromSeconds(floatTime);
                return time.ToString(@"mm\:ss\:ff");
        }

        #endregion
}
