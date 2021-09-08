using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultiplePlusBar : MonoBehaviour
{
    
    public delegate void DelegateFunction(int i, MultiplePlusBar m);
    public DelegateFunction f;
    public UnityEngine.UI.Text param;
    public MultiplePlusBar mpb;

    public GameUserParameter _parameterType;


    void Awake()
    {
        mpb = this.GetComponent<MultiplePlusBar>();
    }

    public void InvokeButton(int i) {
        f(i, mpb);
    }
    
    public void updateTexts(string s) {
        param.text = s;
    }


}
