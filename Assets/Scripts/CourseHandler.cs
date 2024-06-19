using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CourseHandler : MonoBehaviour {

    #region CourseState

    private Course currCourse = null;

    private int currCheckpoint = 0;

    private CourseState currState = CourseState.Inactive;

    public enum CourseState {
        Inactive,
        StartMenu,
        CountDown,
        Racing,
        EndMenu,
    }

    public void SetCourseState(CourseState newState) {
        // SET NEW STATE
        currState = newState;

        // SWITCH STATE
        switch (currState) {
            case CourseState.Inactive:
                InactiveState();
                break;
            case CourseState.StartMenu:
                StartMenuState();
                break;
            case CourseState.CountDown:
                CountDownState();
                break;
            case CourseState.Racing:
                RacingState();
                break;
            case CourseState.EndMenu:
                EndMenuState();
                break;
        }
    }

    #endregion

    #region Variables

    [SerializeField] private PlayerController playerController;

    [SerializeField] private PlayerRespawn playerRespawn;

    [SerializeField] private int countdownTime = 3;

    #endregion

    #region Singleton

    public static CourseHandler instance;

    void Start() {
        if (instance != null)
            return;
        instance = this;
    }

    #endregion

    #region InactiveState

    void InactiveState() {
        // Update Course Menu
        CourseMenu.instance.InactiveState();

        // UNFREEZE PLAYER
        playerController.EnableMovement(true);
    }

    #endregion

    #region StartMenuState

    void StartMenuState() {
        // Update Course Menu
        CourseMenu.instance.StartMenuState();

        // UPDATE SCOREBOARD
        CourseMenu.instance.UpdateScoreBoard(currCourse.times);
    }

    #endregion

    #region CountDownState

    void CountDownState() {
        // Update Course Menu
        CourseMenu.instance.CountDownState();

        // START COUNTDOWN
        StartCountDown();
    }

    #endregion

    #region RacingState

    void RacingState() {
        // Update Course Menu
        CourseMenu.instance.RacingState();
    }

    #endregion

    #region EndMenuState

    void EndMenuState() {
        // Update Course Menu
        CourseMenu.instance.EndMenuState();

        // FREEZE PLAYER
        playerController.EnableMovement(false);

        // PLAY PLAYER WIN ANIMATION
        playerController.gameObject.GetComponent<PlayerAnimation>().TriggerVictoryAnimation();
    }

    #endregion

    #region CountDown

    void StartCountDown() {
        // FREEZE PLAYER
        playerController.EnableMovement(false);

        // SET PLAYER POSITION
        playerController.SetPosition(currCourse.GetStartPoint());

        // START COUNTDOWN
        StartCoroutine(CountDown());

        // SET RESPAWN POINT TO START POINT
        playerRespawn.SetCurrRespawnPoint(currCourse.GetStartPoint());
    }

    IEnumerator CountDown() {
        // START COUNTDOWN
        AkSoundEngine.PostEvent("Play_Countdown", gameObject);
        for (int i = 0; i < countdownTime; i++) {
            // UPDATE COUNTDOWN TEXT
            CourseMenu.instance.SetCountDownTime(countdownTime - i);
            yield return new WaitForSeconds(1f);
        }

        // STOP COUNTDOWN
        StopCountDown();
    }

    void StopCountDown() {
        // UNFREEZE PLAYER
        playerController.EnableMovement(true);

        // START COURSE
        StartCourse();
    }

    #endregion

    #region CourseRacing

    void StartCourse() {
        // RESET CHECKPOINT
        currCheckpoint = 0;

        // SET COURSE STATE
        SetCourseState(CourseState.Racing);

        // START TIMER
        Timer.instance.StartTimer();

        // DISPLAY TIMER
        Timer.instance.SetTimerActive(true);
    }

    void StopCourse() {
        if (currCheckpoint == currCourse.checkpoints.Length) {
            AkSoundEngine.PostEvent("Play_Finish", gameObject);

            // STOP TIMER
            Timer.instance.StopTimer();

            // SAVE TIME
            SaveTime();

            // UPDATE END SCREEN SCOREBOARD
            CourseMenu.instance.UpdateEndScreen(currCourse.times, Timer.instance.GetCurrTime());

            // SET COURSE STATE
            SetCourseState(CourseState.EndMenu);

            // HIDE TIMER
            Timer.instance.SetTimerActive(false);
        }
    }

    #endregion

    #region SaveTime

    void SaveTime() {
        // GET CURRENT TIME
        float currTime = Timer.instance.GetCurrTime();

        // GET CURRENT COURSE
        Course currCourse = GetCurrCourse();

        // SAVE TIME
        float[] times = CourseSetup.instance.SaveTime(currCourse, currTime);

        // UPDATE SCOREBOARD
        CourseMenu.instance.UpdateScoreBoard(times);
    }

    #endregion

    #region Collision

    public void CoursePointEnter(string type, int? checkpointIndex, Course parentCourse) {
        // CHECK WHICH TYPE OF COURSE POINT
        switch (type) {
            case "Start":
                // SET CURRENT COURSE
                currCourse = parentCourse;

                SetCourseState(CourseState.StartMenu);
                break;

            case "Checkpoint":
                // CHECK IF COURSE IS ACTIVE
                if (currState == CourseState.Racing) {
                    // CHECK IF CHECKPOINT IS NEXT
                    if (checkpointIndex == currCheckpoint) {
                        AkSoundEngine.PostEvent("Play_Achievement_sound", gameObject);

                        // SET CURRENT RESPAWN POINT
                        playerRespawn.SetCurrRespawnPoint(parentCourse.GetCheckpointPoint(currCheckpoint));

                        // INCREMENT CHECKPOINT
                        currCheckpoint++;
                    }
                }
                break;

            case "End":
                // STOP COURSE
                StopCourse();
                break;
        }
    }

    public void CoursePointExit(string type, int? checkpointIndex, Course parentCourse) {
        // CHECK WHICH TYPE OF COURSE POINT
        if (type == "Start") {
            // DEACTIVE ON START LEAVE WHEN COURSE IS NOT STARTED
            if (currState == CourseState.StartMenu) {
                SetCourseState(CourseState.Inactive);

                // SET CURRENT COURSE
                currCourse = null;
            }
        }
    }

    #endregion

    #region EnterPress

    void OnEnterPress() {
        // CHECK CURRENT STATE
        if (currState == CourseState.StartMenu) {
            SetCourseState(CourseState.CountDown);
        }
        else if (currState == CourseState.EndMenu) {
            // SET CURRENT COURSE
            currCourse = null;

            SetCourseState(CourseState.Inactive);
        }
    }

    #endregion

    #region Getters

    public CourseState GetCurrState() {
        return currState;
    }

    public Course GetCurrCourse() {
        return currCourse;
    }

    public int GetCurrCheckpoint() {
        return currCheckpoint;
    }

    #endregion
}
