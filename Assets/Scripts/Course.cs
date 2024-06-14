using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Course : MonoBehaviour {

    #region CourseData

    public CoursePointData start;
    public CoursePointData[] checkpoints;
    public CoursePointData end;

    public string setupPath;

    public float[] times;

    #endregion

    #region CourseState

    private bool courseActive = false;
    private int currCheckpoints = 0;

    private List<float> currTimes = new List<float>();

    #endregion

    [SerializeField] private GameObject courseStart, courseCheckpoint, courseEnd;


    #region CourseGameObjects
    private GameObject startObject;

    private List<GameObject> checkpointObjects = new List<GameObject>();

    private GameObject endObject;

    #endregion

    void Start() {
        InstantiateCourse();
    }

    #region CourseSetup
    void InstantiateCourse() {
        // COURSE START
        startObject = InstantiateCoursePoint(courseStart, start);


        // COURSE CHECKPOINTS
        for (int i = 0; i < checkpoints.Length; i++) {
            checkpointObjects.Add(InstantiateCoursePoint(courseCheckpoint, checkpoints[i], i));
        }

        // COURSE END
        endObject = InstantiateCoursePoint(courseEnd, end);
    }

    GameObject InstantiateCoursePoint(GameObject coursePoint, CoursePointData point, int? index = null) {
        GameObject coursePointObject = Instantiate(coursePoint, this.transform);
        coursePointObject.GetComponent<CoursePoint>().checkpointIndex = index;
        coursePointObject.transform.position = new Vector3(point.position[0], point.position[1], point.position[2]);
        coursePointObject.transform.rotation = Quaternion.Euler(point.rotation[0], point.rotation[1], point.rotation[2]);

        return coursePointObject;
    }

    #endregion

    #region CourseStart

    public void PrepareCourse() {
        if (GameManager.instance.currCourse == null) {
            GameManager.instance.currCourse = this;
            SetCourseActive(true);
            currCheckpoints = 0;
            GameManager.instance.PrepareCourse();
        }
    }

    public void StartCourse() {
        if (GameManager.instance.currCourse == this) {
            GameManager.instance.StartCourse();
            GameManager.instance.StartTimer();
        }
    }

    #endregion

    #region CourseCheckpoint

    public void CourseCheckpoint(int? index) {
        if (index == currCheckpoints) {
            currCheckpoints++;
            checkpointObjects[(int)index].SetActive(false);
        }
    }

    #endregion

    #region CourseEnd

    public void CourseEnd() {
        if (currCheckpoints == checkpoints.Length && GameManager.instance.currCourse == this) {
            GameManager.instance.StopTimer();
            GameManager.instance.currCourse = null;
            currCheckpoints = 0;
            SetCourseActive(false);
        }
    }

    #endregion

    #region CourseHandling

    void SetCourseActive(bool active) {
        courseActive = active;
        startObject.SetActive(!active);
        foreach (GameObject checkpoint in checkpointObjects) {
            checkpoint.SetActive(active);
        }
        endObject.SetActive(active);
    }

    public void UpdateCourseTimes() {
        float[] temp = new float[times.Length + 1];
        times.CopyTo(temp, 0);
        temp[temp.Length - 1] = GameManager.instance.currTime;
        times = temp;
    }

    void Update() {
        if (GameManager.instance.currCourse != this && GameManager.instance.currCourse != null) {
            startObject.SetActive(false);
        }
        else {
            if (!courseActive) {
                startObject.SetActive(true);
            }
        }
    }

    #endregion
}
