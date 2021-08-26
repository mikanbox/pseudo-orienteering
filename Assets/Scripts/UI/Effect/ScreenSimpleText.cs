using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScreenSimpleText : MonoBehaviour
{

    [SerializeField]private Text Message;
    void Awake ()
    {

    }
    public void DisplayOn () 
    {
        this.gameObject.SetActive(true);
    }

    public void DisplayOFF ()
    {
        this.gameObject.SetActive(false);
    }
    public void SetText(string text) 
    {
        Message.text = text;
    }

}
