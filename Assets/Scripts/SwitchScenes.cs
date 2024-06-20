using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SwitchScenes : MonoBehaviour
{

    public void GoToTutorialScene() {
        SceneManager.LoadSceneAsync(1);
    }


    public void GoToCityScene() {
        SceneManager.LoadSceneAsync(2);
    }

}
