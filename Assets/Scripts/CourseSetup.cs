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

#region CourseData Classes

[System.Serializable]
public class CourseData {
    public CoursePointData start;
    public CoursePointData[] checkpoints;
    public CoursePointData end;
}

[System.Serializable]
public class CoursePointData {
    public float[] position;
    public float[] rotation;
}

#endregion