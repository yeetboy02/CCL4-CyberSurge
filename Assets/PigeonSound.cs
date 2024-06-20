using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PigeonSound : MonoBehaviour
{

    private int pigeonSoundId;

    // Start is called before the first frame update
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            pigeonSoundId = (int)AkSoundEngine.PostEvent("Play_Pigeon", gameObject);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            AkSoundEngine.StopPlayingID((uint)pigeonSoundId);
        }
    }

    private void OnDestroy()
    {
        AkSoundEngine.StopPlayingID((uint)pigeonSoundId);
    }

}
