using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SoundManager : MonoBehaviour
{
    private int AmbientNoiseId = -1;
    private int CyberpunkMusicId = -1;
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

    private void Update()
        {
        if(SceneManager.GetActiveScene().name == "CityLevel")
        {
            AkSoundEngine.StopPlayingID((uint)CyberpunkMusicId);
            CyberpunkMusicId = -1;
            if(AmbientNoiseId == -1)
            {
            AmbientNoiseId = (int)AkSoundEngine.PostEvent("Play_AmbientNoise", gameObject);
            }
        }
        else if(SceneManager.GetActiveScene().name == "SmallLevel")
        {
            AkSoundEngine.StopPlayingID((uint)AmbientNoiseId);
            AmbientNoiseId = -1;
            AkSoundEngine.StopPlayingID((uint)CyberpunkMusicId);
            if (CyberpunkMusicId == -1)
            {
                CyberpunkMusicId = (int)AkSoundEngine.PostEvent("Play_Cyberpunk_Beat_Quiet", gameObject);
            }
        }
        else if (SceneManager.GetActiveScene().name == "MainMenu")
        {
            if (CyberpunkMusicId == -1)
            {
                CyberpunkMusicId = (int)AkSoundEngine.PostEvent("Play_Cyberpunk_Beat_Quiet", gameObject);
            }
        }
    }

}

