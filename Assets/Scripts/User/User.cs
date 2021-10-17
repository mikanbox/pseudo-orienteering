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
    public GameUserExp _userexp;



    void Awake()
    {
        _user = this.GetComponent<User>();
        loadUserdata();
    }

    public void loadUserdata() {
        _userexp = new GameUserExp();
    }

    

    public void UpdateHP(float minusHpAmount) {
        _parameter._hp -= minusHpAmount * 10;
        if (_parameter._hp < 0)
            _parameter._hp = 0;
        
        if (_parameter._hp > _parameter._maxhp)
            _parameter._hp = _parameter._maxhp;
        

        _windowHpSlider.value = GetHPRatio();

        if (GetHPRatio() < 0.7f) {
            var em = _drops.emission;
            em.rateOverTime = (1f - GetHPRatio()) * 5.0f;
        } else {
            var em = _drops.emission;
            em.rateOverTime = 0;
        }
    }
    


    public void UpdateExp(GameUserExp tmpexp) {
        _userexp.addExp(tmpexp);
        // CalcParameter();
    }

    public void CalcParameter() {
        _isLVUpCheckRes = _userexp.isLVUpCheck();
    }


    public void UpdateUnTrackingPosition(float p) {
        _parameter._trackingPosition += p;
        _windowTrackingSlider.value = _parameter._trackingPosition /100f;

        // if ( (_parameter._trackingPosition / 100f) > 0.1f ) {
        //     var em = _questions.emission;
        //     em.rateOverTime = (_parameter._trackingPosition / 100f)*10;
        // } else {
        //     var em = _questions.emission;
        //     em.rateOverTime = 0;
        // }

        if (_islostPosition) {
            var em = _questions.emission;
            em.rateOverTime = (50 / 100f)*10;
            ManipurateUser._ManipurateUser.StopUserAnimation();
            
        } else {
            var em = _questions.emission;
            em.rateOverTime = 0;
        }

    }


    public float GetHPRatio() {
        return _parameter._hp / _parameter._maxhp;
    }

    public void SetGameParameter (GameParameter param) {
        _parameter = param;
    }

}
