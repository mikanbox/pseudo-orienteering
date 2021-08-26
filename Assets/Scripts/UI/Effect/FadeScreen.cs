using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeScreen : MonoBehaviour
{
    public void DisplayOn () 
    {
        this.gameObject.SetActive(true);
    }

    public void DisplayOFF ()
    {
        this.gameObject.SetActive(false);
    }

}
