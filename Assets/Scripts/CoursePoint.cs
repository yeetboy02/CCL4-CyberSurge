using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoursePoint : MonoBehaviour {

    [SerializeField] private string type;

    public int? checkpointIndex = null;

    private Course parentCourse;

    void Start() {
        parentCourse = transform.parent.GetComponent<Course>();

        if (type != "Start") {
            gameObject.SetActive(false);
        }
    }

    #region Collision
    void OnTriggerEnter(Collider other) {
        if (other.gameObject.CompareTag("Player")) {
            switch (type) {
                case "Start":
                    GameManager.instance.currMenuCourse = parentCourse;
                    GameManager.instance.OpenCourseMenu();
                    break;
                case "Checkpoint":
                    parentCourse.CourseCheckpoint(checkpointIndex);
                    break;
                case "End":
                    parentCourse.CourseEnd();
                    break;
            }
        }
    }

    void OnTriggerExit(Collider other) {
        if (other.gameObject.CompareTag("Player") && type == "Start") {
            GameManager.instance.currMenuCourse = null;
            GameManager.instance.CloseCourseMenu();
        }
    }
    #endregion
}