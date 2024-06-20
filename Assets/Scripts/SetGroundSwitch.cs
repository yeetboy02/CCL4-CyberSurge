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
    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if(hit.gameObject.name.Contains("Air") || hit.gameObject.name.Contains("Jump"))
        {
            AkSoundEngine.SetSwitch("Ground", "Metal", gameObject);
            Debug.Log("Metal");
            Debug.Log(AkSoundEngine.SetSwitch("Ground", "Metal", gameObject));
        }
        else if (hit.gameObject.name.Contains("House") || hit.gameObject.name.Contains("Floor"))
        {
            AkSoundEngine.SetSwitch("Ground", "Concrete", gameObject);
            Debug.Log("Concrete");
            Debug.Log(AkSoundEngine.SetSwitch("Ground", "Concrete", gameObject));

        }

    }
}

