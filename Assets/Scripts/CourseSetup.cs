using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class CourseSetup : MonoBehaviour {

    #region Parameters
        [SerializeField] private string courseSetupDir = "Assets/CoursesSetup/";

        [SerializeField] private GameObject Course;

    #endregion

    #region Variables
        DirectoryInfo dir;

        FileInfo[] courses;

    #endregion

    #region Setup

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
            Debug.Log(GameManager.instance);
            string currCourse = GameManager.instance.ReadJSON(course);
            CourseData courseObject = JsonUtility.FromJson<CourseData>(currCourse);
            CreateCourse(courseObject, course.FullName);
        }
    }

    void CreateCourse(CourseData course, string path) {
        GameObject courseObject = Instantiate(Course);
        Course courseScript = courseObject.GetComponent<Course>();
        courseScript.start = course.start;
        courseScript.checkpoints = course.checkpoints;
        courseScript.end = course.end;
        courseScript.times = course.times;
        courseScript.setupPath = path;
    }
    #endregion
}

#region CourseData Classes

[System.Serializable]
public class CourseData {
    public CoursePointData start;
    public CoursePointData[] checkpoints;
    public CoursePointData end;

    public float[] times;
}

[System.Serializable]
public class CoursePointData {
    public float[] position;
    public float[] rotation;
}

#endregion