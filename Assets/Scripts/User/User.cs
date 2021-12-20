using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Deprecated
public class User : MonoBehaviour
{
    Decoration _decoration;
    AnimatorControllerParameter _animParameter;
    public GameParameter _parameter;
    public static User _user;

    
    [SerializeField]
    private UnityEngine.UI.Slider _windowHpSlider;
    [SerializeField]
    private UnityEngine.UI.Slider _windowTrackingSlider;

    [SerializeField]
    private UnityEngine.ParticleSystem _drops;


    [SerializeField]
    private UnityEngine.ParticleSystem _questions;

    public bool _islostPosition =false;

    public Dictionary<GameUserParameter,int> _isLVUpCheckRes;
    public Dictionary<GameUserParameter,int> _PrevParams;
    public GameUserExp _userexp;



    void Awake()
    {
        _PrevParams = new Dictionary<GameUserParameter, int>();
        _user = this.GetComponent<User>();
        loadUserdata();
    }

// TODO セーブデータからロード
    public void loadUserdata() {
        _userexp = new GameUserExp();
    }

    public void ResetUserStateAfterClear() {
        _parameter._hp = _parameter._maxhp;
        _parameter._trackingPosition = 70f / Mathf.Log10(User._user._userexp._intelligence);
        _windowHpSlider.value = GetHPRatio();
        _windowTrackingSlider.value = _parameter._trackingPosition /100f;
    }



    public void UpdateExp(GameUserExp tmpexp) {
        _userexp.addExp(tmpexp);
        _isLVUpCheckRes = _userexp.isLVUpCheck();

        foreach (KeyValuePair<GameUserParameter, int> item in _isLVUpCheckRes) {
            int value = GameParameter.CalcParamperLV(item.Key,item.Value);
            _PrevParams[item.Key] = _parameter.GetSingleParam(item.Key);
            _parameter.addSingleParam(item.Key,value);
        }

        _parameter._hp = _parameter._maxhp;
    }




// ゲーム中

    public void UpdateHP(float minusHpAmount) {
        _parameter._hp -= minusHpAmount * 10;
        _parameter._hp = Mathf.Max(_parameter._hp,0);
        _parameter._hp = Mathf.Min(_parameter._hp,_parameter._maxhp);


        _windowHpSlider.value = GetHPRatio();

        var em = _drops.emission;
        em.rateOverTime = 0;
        if (GetHPRatio() < 0.7f) 
            em.rateOverTime = (1f - GetHPRatio()) * 5.0f;
        
    }
    
    //ツボりどを計算
    public void UpdateUnTrackingPosition(float p) {
        _parameter._trackingPosition += p;
        _windowTrackingSlider.value = _parameter._trackingPosition /100f;

        var em = _questions.emission;
        if (_islostPosition) {
            em.rateOverTime = (50 / 100f)*10;
            ManipurateUser._ManipurateUser.StopUserAnimation();
            
        } else {
            em.rateOverTime = 0;
        }

    }

    public void SetGameParameter (GameParameter param) {
        _parameter = param;
    }


    public float GetHPRatio() {
        return _parameter._hp / _parameter._maxhp;
    }



}
