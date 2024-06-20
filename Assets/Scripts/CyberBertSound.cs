using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CyberSteveSound : MonoBehaviour { 

    private uint _cyberBertId;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            _cyberBertId = AkSoundEngine.PostEvent("Play_HoloRobo", gameObject);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            AkSoundEngine.StopPlayingID(_cyberBertId, 500);
        }
    }

    private void OnDestroy()
    {
        AkSoundEngine.StopPlayingID(_cyberBertId);

    }
}
