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

    #region Getters

    public Vector3 GetStartPoint() {
        return startObject.transform.position;
    }

    public Vector3 GetCheckpointPoint(int index) {
        return checkpointObjects[index].transform.position;
    }

    public Vector3 GetEndPoint() {
        return endObject.transform.position;
    }

    #endregion

    #region CourseSetup
    void InstantiateCourse() {
        // INSTANTIATE COURSE START
        startObject = InstantiateCoursePoint(courseStart, start);


        // INSTANTIATE COURSE CHECKPOINTS
        for (int i = 0; i < checkpoints.Length; i++) {
            checkpointObjects.Add(InstantiateCoursePoint(courseCheckpoint, checkpoints[i], i));
        }

        // INSTANTIATE COURSE END
        endObject = InstantiateCoursePoint(courseEnd, end);
    }

    GameObject InstantiateCoursePoint(GameObject coursePoint, CoursePointData point, int? index = null) {
        // INSTANTIATE COURSE POINT
        GameObject coursePointObject = Instantiate(coursePoint, this.transform);

        // SET COURSE POINT DATA
        coursePointObject.GetComponent<CoursePoint>().checkpointIndex = index;
        coursePointObject.transform.position = new Vector3(point.position[0], point.position[1], point.position[2]);
        coursePointObject.transform.rotation = Quaternion.Euler(point.rotation[0], point.rotation[1], point.rotation[2]);

        return coursePointObject;
    }

    #endregion

    void Update() {
        UpdateCourseState();
    }

    #region CheckState

    public void UpdateCourseState() {
        // GET CURRENT STATE
        CourseHandler.CourseState currState = CourseHandler.instance.GetCurrState();

        // GET CURRENT COURSE
        Course currCourse = CourseHandler.instance.GetCurrCourse();

        // GET CURRENT CHECKPOINT
        int currCheckpoint = CourseHandler.instance.GetCurrCheckpoint();

        if (currCourse == this) {
            // CHECK CURRENT STATE
            switch (currState) {
                case CourseHandler.CourseState.StartMenu:
                    // ACTIVATE START POINT
                    startObject.GetComponent<CoursePoint>().Activate(true);

                    // DEACTIVATE ALL CHECKPOINTS
                    checkpointObjects.ForEach(checkpoint => checkpoint.GetComponent<CoursePoint>().Activate(false));

                    // DEACTIVATE END POINT
                    endObject.GetComponent<CoursePoint>().Activate(false);

                    break;

                case CourseHandler.CourseState.CountDown:
                    // DEACTIVATE START POINT
                    startObject.GetComponent<CoursePoint>().Activate(false);

                    // ACTIVATE ALL CHECKPOINTS
                    checkpointObjects.ForEach(checkpoint => checkpoint.GetComponent<CoursePoint>().Activate(true));

                    // ACTIVATE END POINT
                    endObject.GetComponent<CoursePoint>().Activate(true);

                    break;
                
                case CourseHandler.CourseState.Racing:
                    // DEACTIVATE START POINT
                    startObject.GetComponent<CoursePoint>().Activate(false);

                    // ACTIVATE CURRENT CHECKPOINT
                    checkpointObjects.ForEach(checkpoint => {
                        if (checkpoint.GetComponent<CoursePoint>().checkpointIndex >= currCheckpoint) {
                            checkpoint.GetComponent<CoursePoint>().Activate(true);
                        }
                        else {
                            checkpoint.GetComponent<CoursePoint>().Activate(false);
                            
                        }
                    });

                    break;

                case CourseHandler.CourseState.EndMenu:
                    // DEACTIVATE START POINT
                    startObject.GetComponent<CoursePoint>().Activate(false);

                    // DEACTIVATE ALL CHECKPOINTS
                    checkpointObjects.ForEach(checkpoint => checkpoint.GetComponent<CoursePoint>().Activate(false));

                    // DEACTIVATE END POINT
                    endObject.GetComponent<CoursePoint>().Activate(false);

                    break;
            }
        }
        else {
            // ACTIVATE START POINT
            startObject.GetComponent<CoursePoint>().Activate(true);

            // DEACTIVATE ALL CHECKPOINTS
            checkpointObjects.ForEach(checkpoint => checkpoint.GetComponent<CoursePoint>().Activate(false));

            // DEACTIVATE END POINT
            endObject.GetComponent<CoursePoint>().Activate(false);
        }
    }

    #endregion

    #region AddTime

    public void AddTime(float time) {
        // ADD TIME TO ARRAY
        List<float> timeList = new List<float>(times);
        timeList.Add(time);
        times = timeList.ToArray();
    }

    #endregion
}
