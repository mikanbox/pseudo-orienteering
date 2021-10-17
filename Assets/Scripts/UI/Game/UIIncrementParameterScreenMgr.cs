using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIIncrementParameterScreenMgr : MonoBehaviour
{
    [SerializeField]
    private int _points;
    [SerializeField]
    private UnityEngine.UI.Text _pointsText;


    [SerializeField]
    private List<GameObject> _parametersObjects;

    [SerializeField]
    private GameObject _baseObject;


    public void SetUI(int initpoint, int num, List<GameUserParameter> parameterTypes, List<int> initvalues) {
        _points = initpoint;
        _pointsText.text = _points.ToString();

        for (int i = 0; i < num; i++) {
            GameObject obj =  GameObject.Instantiate(_baseObject,Vector3.zero, Quaternion.identity,_baseObject.transform.parent);
            obj.SetActive(true);
            _parametersObjects.Add(obj);
            obj.GetComponent<UIMultiIncrementParameter>().Initialize(parameterTypes[i],initvalues[i]);
            obj.GetComponent<UIMultiIncrementParameter>().f = changeParameter;
        }
    }


    public void changeParameter(int i, UIMultiIncrementParameter m) {
        Debug.Log("ChangeParameter : " + i);
        switch (i) {
            case 1:
            case 10:
            case 100:
                int value = i;
                if (_points < value) 
                    value = _points;
                m._value += value;
                _points -= value;
            break;
            case 0:
                int value2 = m._value;
                m._value = 0;
                _points += value2;
            break;
        }
        _pointsText.text = _points.ToString();
    }


    
    public Dictionary<GameUserParameter,int> getResults() {
        Dictionary<GameUserParameter,int> dic = new Dictionary<GameUserParameter, int>();
        foreach (var item in _parametersObjects) {
            UIMultiIncrementParameter p = item.GetComponent<UIMultiIncrementParameter>();
            dic[p._parameterType] = p._value;
        }
        CloseAllUI();
        return dic;
    }
    
    
    private void CloseAllUI () {
        foreach(var obj in _parametersObjects) {
            GameObject.Destroy(obj);
        }
        _parametersObjects.Clear();
    }

}
