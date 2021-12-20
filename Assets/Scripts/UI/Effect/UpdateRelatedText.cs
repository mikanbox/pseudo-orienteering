using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpdateRelatedText : MonoBehaviour
{
    [SerializeField]
    private Text BaseText;
    [SerializeField]
    private List<Text> Texts;


    public void OnValueChanged(){
        foreach(var item in Texts) {
            item.text = BaseText.text;
        }
    }


}
