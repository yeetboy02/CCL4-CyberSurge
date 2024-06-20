using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSound : MonoBehaviour
{
    public AK.Wwise.Event FootStep;
    public AK.Wwise.Event Landing;


    public void PlayFootstepSound()
    {
        FootStep.Post(gameObject);
    }

    public void PlayLandingSound()
    {
        Landing.Post(gameObject);
    }
}
