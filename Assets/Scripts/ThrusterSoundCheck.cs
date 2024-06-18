using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrusterSound : MonoBehaviour
{
    private uint _thrusterId;
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
           _thrusterId = AkSoundEngine.PostEvent("Play_thrusters_loop", gameObject);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            AkSoundEngine.StopPlayingID(_thrusterId, 500);
        }
    }
}
