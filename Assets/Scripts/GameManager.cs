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

        public void WriteJSON(string path, string json) {
                File.WriteAllText(path, json);
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
                        SaveTime();
                }
        }

        public void SaveTime() {
                if (timerActive) return;

                string path = currCourse.setupPath;
                float time = currTime;

                // CREATE NEW COURSEDATA WITH UPDATED TIME
                CourseData course = new CourseData();
                course.start = currCourse.start;
                course.checkpoints = currCourse.checkpoints;
                course.end = currCourse.end;
                course.times = new float[currCourse.times.Length + 1];
                if (currCourse.times.Length > 0) {
                        currCourse.times.CopyTo(course.times, 0);
                }
                course.times[course.times.Length - 1] = time;

                string jsonCourseObject = JsonUtility.ToJson(course);
                WriteJSON(path, jsonCourseObject);
                currCourse.UpdateCourseTimes();
        }

        public string ConvertTime(float floatTime) {
                TimeSpan time = TimeSpan.FromSeconds(floatTime);
                return time.ToString(@"mm\:ss\:ff");
        }

        #endregion
}
