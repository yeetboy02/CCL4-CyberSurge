using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CourseMenu : MonoBehaviour {

    #region Parameters

    [SerializeField] private float countdownTime = 3.0f;

    [SerializeField] private int scoreboardLength = 5;

    [SerializeField] private GameObject menuText;
    [SerializeField] private GameObject countdownText;
    [SerializeField] private GameObject scoreboardText;

    #endregion

    private float[] times;

    private Course currCourse;

    public void SetMenuActive(bool active) {
        gameObject.SetActive(active);
        if (active) {
            countdownText.SetActive(false);
            scoreboardText.SetActive(true);
            menuText.SetActive(true);
            currCourse = GameManager.instance.currMenuCourse;
            times = currCourse.times;
            FillScoreboard();
        }
        else {
            GameManager.instance.currMenuCourse = null;
        }
    }
    
    void Start() {
        gameObject.SetActive(false);
    }

    void OnEnterPress() {
        currCourse.PrepareCourse();
        StartCoroutine(Countdown());
    }

    #region Countdown 

    IEnumerator Countdown() {
        menuText.SetActive(false);
        countdownText.SetActive(true);
        scoreboardText.SetActive(false);
        for (int i = 0; i < countdownTime; i++) {
            countdownText.GetComponent<TMPro.TextMeshProUGUI>().text = (countdownTime - i).ToString();
            yield return new WaitForSeconds(1f);
        }

        currCourse.StartCourse();
        gameObject.SetActive(false);
    }

    #endregion

    #region Scoreboard

    void FillScoreboard() {
        if (times.Length == 0) {
            scoreboardText.GetComponent<TMPro.TextMeshProUGUI>().text = "No scores yet...";
        }
        else {
            string scoreboardString = "";
            int i = 0;

            Array.Sort(times);

            while (i < times.Length && i < scoreboardLength) {
                scoreboardString += i + 1 + ". " + GameManager.instance.ConvertTime(times[i]) + "\n";
                
                i++;
            }

            scoreboardText.GetComponent<TMPro.TextMeshProUGUI>().text = scoreboardString;
        }
    }

    #endregion
}
