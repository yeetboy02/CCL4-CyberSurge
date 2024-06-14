using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;
using UnityEngine.InputSystem;

public class GameManager: MonoBehaviour {

        public static GameManager instance;

        #region CourseParameters

        public Course currCourse = null;

        public Course currMenuCourse = null;

        [SerializeField] private CourseMenu courseMenu;

        #endregion

        #region PlayerParameters

        [SerializeField] private GameObject player;

        #endregion

        #region TimerParameters

        private bool timerActive = false;
        public float currTime = 0.0f;

        [SerializeField] private GameObject timerText;

        #endregion 

        private PlayerInput playerInput;


        void Start() {
                if (instance != null)
                        return;
                instance = this;
                
                DontDestroyOnLoad(gameObject);

                playerInput = player.GetComponent<PlayerInput>();
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
                        timerText.GetComponent<TMPro.TextMeshProUGUI>().text = ConvertTime(currTime);
                }
        }

        #region Timer
        
        public void StartTimer() {
                if (!timerActive) {
                        currTime = 0.0f;
                        timerActive = true;
                        timerText.SetActive(true);
                }
        }

        public void StopTimer() {
                if (timerActive) {
                        timerActive = false;
                        Debug.Log("Time: " + ConvertTime(currTime));
                        SaveTime();
                        StartCoroutine(HideTimer());
                }
        }

        IEnumerator HideTimer() {
                yield return new WaitForSeconds(5.0f);
                timerText.SetActive(false);
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

        #region CourseMenu

        public void OpenCourseMenu() {
                if (currCourse != null) return;
                courseMenu.SetMenuActive(true);
        }

        public void CloseCourseMenu() {
                if (currCourse != null) return;
                courseMenu.SetMenuActive(false);
        }

        #endregion

        #region CourseHandling

        public void PrepareCourse() {
                player.transform.position = new Vector3(currCourse.start.position[0], currCourse.start.position[1], currCourse.start.position[2]);
                player.transform.rotation = Quaternion.Euler(new Vector3(currCourse.start.rotation[0], currCourse.start.rotation[1], currCourse.start.rotation[2]));
                playerInput.actions.FindAction("Movement").Disable();
        }

        public void StartCourse() {
                playerInput.actions.FindAction("Movement").Enable();
        }

        #endregion
}
