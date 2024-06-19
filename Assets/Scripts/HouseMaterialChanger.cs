using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class HouseMaterialChanger : MonoBehaviour
{
    private int HouseStateNr;

    [SerializeField]
    private Material matRed;
    [SerializeField]
    private Material matOrange;
    [SerializeField]
    private Material matYellow;
    //[SerializeField]
    //private Material matPurple;
    //[SerializeField]
    //private Material matGreen;
    //[SerializeField]
    //private Material matOrange;


    private void Start()
    {
        HouseStateNr = Random.Range(1, 3);

        UpdatePotionAppearance();
    }




    private void UpdatePotionAppearance()
    {
        switch (HouseStateNr)
        {
            case 1:
                this.gameObject.GetComponent<Renderer>().material = matRed;
                break;
            case 2:
                this.gameObject.GetComponent<Renderer>().material = matOrange;
                break;
            case 3:
                this.gameObject.GetComponent<Renderer>().material = matYellow;
                break;
            //case 4:
            //    this.gameObject.GetComponent<Renderer>().material = matPurple;
            //    break;
            //case 5:
            //    this.gameObject.GetComponent<Renderer>().material = matGreen;
            //    break;
            //case 6:
            //    this.gameObject.GetComponent<Renderer>().material = matOrange;
            //    break;
        }
    }
}
