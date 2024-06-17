using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CourseMenu : MonoBehaviour {

    #region TextObjects

    [SerializeField] private GameObject courseMenuText;

    [SerializeField] private GameObject scoreBoardText;

    [SerializeField] private GameObject countDownText;

    #endregion

    #region Parameters

    [SerializeField] private int scoreBoardLength = 5;

    #endregion

    #region Singleton

    public static CourseMenu instance;

    void Start() {
        if (instance != null)
            return;
        instance = this;

        // SET START MENU STATE
        CourseHandler.instance.SetCourseState(CourseHandler.CourseState.Inactive);
    }

    #endregion

    #region StartMenuState

    public void StartMenuState() {
        // ACTIVATE COURSE MENU
        gameObject.SetActive(true);

        // ACTIVATE COURSE MENU TEXT
        courseMenuText.SetActive(true);

        // ACTIVATE SCOREBOARD TEXT
        scoreBoardText.SetActive(true);

        // DEACTIVATE COUNTDOWN TEXT
        countDownText.SetActive(false);
    }

    #endregion

    #region CountDownState

    public void CountDownState() {
        // ACTIVATE COURSE MENU
        gameObject.SetActive(true);

        // DEACTIVATE COURSE MENU TEXT
        courseMenuText.SetActive(false);

        // DEACTIVATE SCOREBOARD TEXT
        scoreBoardText.SetActive(false);

        // ACTIVATE COUNTDOWN TEXT
        countDownText.SetActive(true);
    }

    #endregion 

    #region RacingState

    public void RacingState() {
        // DEACTIVATE COURSE MENU
        gameObject.SetActive(false);

        // DEACTIVATE COURSE MENU TEXT
        courseMenuText.SetActive(false);

        // DEACTIVATE SCOREBOARD TEXT
        scoreBoardText.SetActive(false);

        // DEACTIVATE COUNTDOWN TEXT
        countDownText.SetActive(false);
    }

    #endregion

    #region EndMenuState

    public void EndMenuState() {
        // ACTIVATE COURSE MENU
        gameObject.SetActive(true);

        // ACTIVATE COURSE MENU TEXT
        courseMenuText.SetActive(false);

        // ACTIVATE SCOREBOARD TEXT
        scoreBoardText.SetActive(true);

        // DEACTIVATE COUNTDOWN TEXT
        countDownText.SetActive(false);
    }

    #endregion

    #region InactiveState

    public void InactiveState() {
        // DEACTIVATE COURSE MENU
        gameObject.SetActive(false);

        // DEACTIVATE COURSE MENU TEXT
        courseMenuText.SetActive(false);

        // DEACTIVATE SCOREBOARD TEXT
        scoreBoardText.SetActive(false);

        // DEACTIVATE COUNTDOWN TEXT
        countDownText.SetActive(false);
    }

    #endregion

    #region CountDown

    public void SetCountDownTime(int time) {
        countDownText.GetComponent<TMPro.TextMeshProUGUI>().text = time.ToString();
    }

    #endregion

    #region ScoreBoard

    public void UpdateScoreBoard(float[] times) {
        // UPDATE SCOREBOARD TEXT
        if (times.Length == 0) {
            // DISPLAY NO SCORES TEXT IF NO TIMES YET
            scoreBoardText.GetComponent<TMPro.TextMeshProUGUI>().text = "No scores yet...";
        }
        else {
            string scoreBoardString = "";
            int i = 0;

            // SORT TIMES
            Array.Sort(times);

            // ASSEMBLE SCOREBOARD STRING WITH THE TOP 5 TIMES
            while (i < times.Length && i < scoreBoardLength) {
                scoreBoardString += i + 1 + ". " + Timer.instance.ConvertTimeToString(times[i]) + "\n";
                
                i++;
            }

            // DISPLAY SCOREBOARD STRING
            scoreBoardText.GetComponent<TMPro.TextMeshProUGUI>().text = scoreBoardString;
        }
    }

    #endregion
}
