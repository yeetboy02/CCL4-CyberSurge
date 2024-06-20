using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialMenu : MonoBehaviour {

    #region Parameters

    [SerializeField] private GameObject introPanel;

    [SerializeField] private GameObject moveToCityPanel;

    [SerializeField] private SwitchScenes switchScenes;

    #endregion

    #region Variables

    private TutorialMenuState currState;

    #endregion


    #region Singleton

    public static TutorialMenu instance;

    void Start() {
        if (instance != null)
            return;
        instance = this;

        // SET INITIAL STATE
        SetTutorialMenuState(TutorialMenuState.Intro);
    }

    #endregion

    #region TutorialMenuState

    public enum TutorialMenuState {
        Intro,
        MoveToCity,
        Inactive,
    }

    public void SetTutorialMenuState(TutorialMenuState newState) {
        // SET NEW STATE
        currState = newState;

        // SWITCH STATE
        switch(newState) {
            case TutorialMenuState.Intro:
                IntroState();
                break;
            case TutorialMenuState.MoveToCity:
                MoveToCityState();
                break;
            case TutorialMenuState.Inactive:
                InactiveState();
                break;
        }
    }

    #endregion

    #region IntroState

    void IntroState() {
        // ACTIVATE INTRO PANEL
        introPanel.SetActive(true);

        // DEACTIVATE MOVE TO CITY PANEL
        moveToCityPanel.SetActive(false);
    }

    #endregion

    #region MoveToCityState

    void MoveToCityState() {
        // DEACTIVATE INTRO PANEL
        introPanel.SetActive(false);

        // ACTIVATE MOVE TO CITY PANEL
        moveToCityPanel.SetActive(true);
    }

    #endregion

    #region InactiveState

    void InactiveState() {
        // DEACTIVATE INTRO PANEL
        introPanel.SetActive(false);

        // DEACTIVATE MOVE TO CITY PANEL
        moveToCityPanel.SetActive(false);
    }

    #endregion

    #region Input

    void OnEnterPress() {
        // CHECK IF MOVE TO CITY PANEL IS OPEN
        if (currState == TutorialMenuState.MoveToCity) {
            // SWITCH SCENE TO CITY
            switchScenes.GoToCityScene();
        }
    }

    #endregion

}
