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
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name.Contains("Air") || collision.gameObject.name.Contains("Jump"))
        {
            AkSoundEngine.SetSwitch("Ground", "Metal", gameObject);
            Debug.Log("Metal");
        }
        else if (collision.gameObject.name.Contains("House") || collision.gameObject.name.Contains("Floor"))
        {
            AkSoundEngine.SetSwitch("Ground", "Concrete", gameObject);
            Debug.Log("Concrete");

        }
        Debug.Log(collision.gameObject.name);
    }
}
