using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoursePoint : MonoBehaviour {

    #region Variables

    public string type;

    public int? checkpointIndex;

    private Course parentCourse;

    #endregion

    void Start() {
        // DEACTIVATE IF NOT STARTING POINT
        if (type != "Start") {
            Activate(false);
        }

        // SET PARENT COURSE
        parentCourse = transform.parent.GetComponent<Course>();
    }

    public void Activate(bool active) {
        // ACTIVATE/DEACTIVATE GAMEOBJECT
        gameObject.SetActive(active);
    }


    #region Collision

    void OnTriggerEnter(Collider other) {
        // CHECK IF PLAYER
        if (other.CompareTag("Player")) {
            CourseHandler.instance.CoursePointEnter(type, checkpointIndex, parentCourse);
        }
    }

    void OnTriggerExit(Collider other) {
        // CHECK IF PLAYER
        if (other.CompareTag("Player")) {
            CourseHandler.instance.CoursePointExit(type, checkpointIndex, parentCourse);
        }
    }

    #endregion
}