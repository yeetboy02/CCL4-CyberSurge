using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour {

    #region Variables

    [SerializeField] private GameObject timerText;

    private bool timerActive = false;

    private float currTime = 0.0f;

    #endregion


    #region Singleton

    public static Timer instance;

    void Start() {
        if (instance != null)
            return;
        instance = this;
    }

    #endregion

    #region TimerLogic

    void Update () {
        if (timerActive) {
            // INCREMENT TIMER
            currTime += Time.deltaTime;

            // UPDATE TIMER TEXT
            timerText.GetComponent<TMPro.TextMeshProUGUI>().text = ConvertTimeToString(GetCurrTime());
        }
    }

    public void StartTimer() {
        ResetTimer();

        // START TIMER
        timerActive = true;
    }

    public void StopTimer() {
        // STOP TIMER
        timerActive = false;
    }

    public void ResetTimer() {
        // RESET TIMER
        currTime = 0.0f;
    }

    public float GetCurrTime() {
        return currTime;
    }

    public string ConvertTimeToString(float time) {
        TimeSpan timeSpan = TimeSpan.FromSeconds(time);
        return timeSpan.ToString(@"mm\:ss\:ff");
    }

    #endregion

    #region TimerUI

    public void SetTimerActive(bool active) {
        // DISPLAY/HIDE TIMER
        timerText.SetActive(active);
    }

    #endregion
}
