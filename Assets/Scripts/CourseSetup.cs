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

    #region Singleton

    public static CourseSetup instance;

    void Start() {
        if (instance != null)
            return;
        instance = this;

        // SETUP
        GetCourseJSONFiles();
        SetupCourses();
    }

    #endregion

    #region Read JSON Files

    public string ReadJSON(FileInfo jsonFile) {
        // READ JSON FILE CONTENT AS A STRING
        return File.ReadAllText(jsonFile.FullName);
    }

    public void WriteJSON(string path, string json) {
        // WRITE JSON STRING TO FILE
        File.WriteAllText(path, json);
    }

    #endregion

    #region SaveTime

    public float[] SaveTime(Course currCourse, float time) {
        // GET CURRENT COURSE SETUP PATH
        string path = currCourse.setupPath;

        // CREATE NEW COURSEDATA WITH UPDATED TIMES
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

        return course.times;
    }

    #endregion

    #region Setup

    void GetCourseJSONFiles() {
        // GET ALL COURSE JSON FILES WITHIN COURSE SETUP DIRECTORY
        dir = new DirectoryInfo(courseSetupDir);
        courses = dir.GetFiles("*.json");
    }

    void SetupCourses() {
        foreach (FileInfo course in courses) {
            // GET JSON FILE CONTENT AS A STRING
            string currCourse = ReadJSON(course);

            // SERIALIZE JSON STRING INTO COURSE DATA OBJECT
            CourseData courseObject = JsonUtility.FromJson<CourseData>(currCourse);

            // CREATE COURSE OBJECT
            CreateCourse(courseObject, course.FullName);
        }
    }

    void CreateCourse(CourseData course, string path) {
        // INSTANTIATE COURSE OBJECT WITH COURSE DATA
        GameObject courseObject = Instantiate(Course);

        // SET COURSE DATA
        Course courseScript = courseObject.GetComponent<Course>();
        courseScript.start = course.start;
        courseScript.checkpoints = course.checkpoints;
        courseScript.end = course.end;
        courseScript.times = course.times;
        courseScript.setupPath = path;
    }
    #endregion
}

#region CourseData Classes/Models

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