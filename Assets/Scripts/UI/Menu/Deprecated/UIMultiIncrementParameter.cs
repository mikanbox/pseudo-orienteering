using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIMultiIncrementParameter : MonoBehaviour
{
    public delegate void DelegateFunction(int i, UIMultiIncrementParameter m);
    public DelegateFunction f;

    [SerializeField]
    private UnityEngine.UI.Text _paramName;
    [SerializeField]
    private UnityEngine.UI.Text _paramValue;



    public UIMultiIncrementParameter mpb;
    public GameUserParameter _parameterType;
    public int _value = 0;
    private int _basevalue = 0;


    void Awake()
    {
        mpb = this.GetComponent<UIMultiIncrementParameter>();
    }

    public void Initialize (GameUserParameter paramName, int paramvalue) {
        _parameterType = paramName;
        _basevalue = paramvalue;
        _paramName.text = GameParameter._gameUserParameterToName[paramName];
        updateParamValue();
    }

    public void InvokeButton(int i) {
        f(i, mpb);
        updateParamValue();
    }
    
    public void updateParamValue() {
        _paramValue.text = (_value + _basevalue).ToString();
    }

    public int GetTotalValue() {
        return _value + _basevalue;
    }


}
