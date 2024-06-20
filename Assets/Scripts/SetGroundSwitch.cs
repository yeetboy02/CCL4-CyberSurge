using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetGroundSwitch : MonoBehaviour
{
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
        if ((this.gameObject.name.Contains("AirVent") || this.gameObject.name.Contains("JumpPad")) && other.gameObject.CompareTag("Player"))
        {
            Debug.Log("Metal");
            AkSoundEngine.SetSwitch("Ground", "Metal", gameObject);
        }
        else
        {
            Debug.Log("Concrete");

            AkSoundEngine.SetSwitch("Ground", "Concrete", gameObject);

        }
    }
}
