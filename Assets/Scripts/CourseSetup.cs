using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class CourseSetup : MonoBehaviour {

    #region Variables
        private string courseSetupDir = "Assets/CoursesSetup/";

        DirectoryInfo dir;

        FileInfo[] courses;
    #endregion

        [SerializeField] private GameObject Course;

    void Start() {
        GetCourseJSONFiles();

        SetupCourses();
    }

    void GetCourseJSONFiles() {
        dir = new DirectoryInfo(courseSetupDir);
        courses = dir.GetFiles("*.json");
    }

    void SetupCourses() {
        foreach (FileInfo course in courses) {
            string currCourse = GameManager.instance.ReadJSON(course);
            CourseData courseObject = JsonUtility.FromJson<CourseData>(currCourse);
            CreateCourse(courseObject);
        }
    }

    void CreateCourse(CourseData course) {
        GameObject courseObject = Instantiate(Course);
        Course courseScript = courseObject.GetComponent<Course>();
        courseScript.start = course.start;
        courseScript.checkpoints = course.checkpoints;
        courseScript.end = course.end;
    }
}

[System.Serializable]
public class CourseData {
    public CoursePoint start;
    public CoursePoint[] checkpoints;
    public CoursePoint end;
}

[System.Serializable]
public class CoursePoint {
    public float[] position;
    public float[] rotation;
}