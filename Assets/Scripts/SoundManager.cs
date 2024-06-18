using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SoundManager : MonoBehaviour
{

    #region Singleton

    public static SoundManager instance;

    void Start()
    {
        if (instance != null)
            return;
        instance = this;

        DontDestroyOnLoad(gameObject);
    }

    #endregion

    private void Awake()
    {
        if(SceneManager.GetActiveScene().name == "CityLevel")
        {
        AkSoundEngine.PostEvent("Play_AmbientNoise", gameObject);

        }
        else if(SceneManager.GetActiveScene().name == "SmallLevel")
        {
            AkSoundEngine.PostEvent("Play_Cyberpunk_Beat_Quiet", gameObject);

        }
    }

}

