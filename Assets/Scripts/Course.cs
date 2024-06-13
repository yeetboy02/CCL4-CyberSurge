using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Course : MonoBehaviour {
    public CoursePoint start;
    public CoursePoint[] checkpoints;
    public CoursePoint end;

    [SerializeField] private GameObject courseStart, courseCheckpoint, courseEnd;

    void Start() {
        InstantiateCourse();
    }

    void InstantiateCourse() {
        InstantiateCoursePoint(courseStart, start);
        foreach (CoursePoint checkpoint in checkpoints) {
            InstantiateCoursePoint(courseCheckpoint, checkpoint);
        }
        InstantiateCoursePoint(courseEnd, end);
    }

    void InstantiateCoursePoint(GameObject coursePoint, CoursePoint point) {
        GameObject coursePointObject = Instantiate(coursePoint);
        coursePointObject.transform.position = new Vector3(point.position[0], point.position[1], point.position[2]);
        coursePointObject.transform.rotation = Quaternion.Euler(point.rotation[0], point.rotation[1], point.rotation[2]);
    }
}
