using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointIndicator : MonoBehaviour {

    #region Parameters

    [SerializeField] private Vector2 offsetFromPlayer = new Vector2(2f, -1f);

    #endregion

    #region Variables


    private GameObject indicatorObject;

    private Vector3 nextCheckPointPosition;

    #endregion

    #region Methods

    public void SetNextCheckPointPosition(Vector3 newPosition) {
        // SET NEW CHECKPOINT POSITION
        nextCheckPointPosition = newPosition;
    }

    #endregion

    #region Setup

    void Start() {

        // RETRIEVE CHILD GAME OBJECT
        indicatorObject = gameObject.transform.GetChild(0).gameObject;

        // SET INITIAL POSITION
        indicatorObject.transform.localPosition = new Vector3(0.0f, offsetFromPlayer.y, offsetFromPlayer.x);
    }

    #endregion

    void Update() {
        // DEACTIVATE OBJECT IF NO COURSE IS ACTIVE
        if (CheckCourseState()) {
            indicatorObject.SetActive(true);
            // SET ROTATION TO POINT TOWARDS NEXT CHECKPOINT
            SetRotation();
        }
        else {
            indicatorObject.SetActive(false);
        }
    }

    #region CheckCourseState

    bool CheckCourseState() {
        // CHECK IF COURSE IS ACTIVE AND IN COUNTDOWN OR RACING STATE
        return (CourseHandler.instance.GetCurrCourse() != null && (CourseHandler.instance.GetCurrState() == CourseHandler.CourseState.CountDown || CourseHandler.instance.GetCurrState() == CourseHandler.CourseState.Racing));
    }

    #endregion
    
    #region Rotation

    void SetRotation() {

        // GET TARGET POSITION ON THE SAME Y-HEIGHT AS THE INDICATOR
        Vector3 targetPosition = new Vector3(nextCheckPointPosition.x, gameObject.transform.position.y, nextCheckPointPosition.z);

        // SET ROTATION TO POINT TOWARDS NEXT CHECKPOINT
        gameObject.transform.LookAt(targetPosition);
    }

    #endregion

}
