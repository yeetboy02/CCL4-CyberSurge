using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CourseMenu : MonoBehaviour {

    #region TextObjects

    [SerializeField] private GameObject courseMenuText;

    [SerializeField] private GameObject scoreBoardText;

    [SerializeField] private GameObject countDownText;

    [SerializeField] private GameObject endScreenText;

    [SerializeField] private GameObject continueBtn;

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
        InactiveState();
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

        // DEACTIVATE END SCREEN TEXT
        endScreenText.SetActive(false);
        continueBtn.SetActive(false);
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

        // DEACTIVATE END SCREEN TEXT
        endScreenText.SetActive(false);
        continueBtn.SetActive(false);
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

        // DEACTIVATE END SCREEN TEXT
        endScreenText.SetActive(false);
        continueBtn.SetActive(false);
    }

    #endregion

    #region EndMenuState

    public void EndMenuState() {
        // ACTIVATE COURSE MENU
        gameObject.SetActive(true);

        // ACTIVATE COURSE MENU TEXT
        courseMenuText.SetActive(false);

        // ACTIVATE SCOREBOARD TEXT
        scoreBoardText.SetActive(false);

        // DEACTIVATE COUNTDOWN TEXT
        countDownText.SetActive(false);

        // ACTIVATE END SCREEN TEXT
        endScreenText.SetActive(true);
        continueBtn.SetActive(true);
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

        // DEACTIVATE END SCREEN TEXT
        endScreenText.SetActive(false);
        continueBtn.SetActive(false);
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

    #region EndScreen

    public void UpdateEndScreen(float[] times, float currTime) {
        string endScreenString = "";
        int i = 0;
        bool currTimeAdded = false;

        // SORT TIMES
        Array.Sort(times);

        // ASSEMBLE END SCREEN STRING WITH THE TOP 5 TIMES
        while (i < times.Length && i < scoreBoardLength) {
            int currNumber = i + 1;
            if (times[i] != currTime) {
                endScreenString += currNumber + ". " + Timer.instance.ConvertTimeToString(times[i]) + "\n";
            }
            else {
                currTimeAdded = true;
                endScreenString += "<color=white>" + currNumber + ". " + Timer.instance.ConvertTimeToString(times[i]) + "</color>\n";
            }
            
            i++;
        }

        if (!currTimeAdded) {
            int currTimeRank = Array.IndexOf(times, currTime) + 1;
            endScreenString += "\n" + "<color=white>" + currTimeRank + ". " + Timer.instance.ConvertTimeToString(currTime) + "</color>\n";
        }

        // DISPLAY END SCREEN STRING
        endScreenText.GetComponent<TMPro.TextMeshProUGUI>().text = endScreenString;
    }

    #endregion

}
