using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class UIResultDetail : MonoBehaviour
{
    [SerializeField]
    private Text _Score;
    [SerializeField]
    private Text _BestScore;
    [SerializeField]
    private Text _TargetScore;

    [SerializeField]
    private List<Text> _paramText;
    [SerializeField]
    private List<Text> _paramexText;

    void OnEnable() {
        _Score.text = GameMgr._gamemgr._GamePlaytimer.GetHHMMSS(GameMgr._gamemgr._GamePlaytimer._recordedTime);

        // User._user._isLVUpCheckRes
        UpdateUserParam();
    }

    void UpdateUserParam(){
        _paramText[0].text = User._user._parameter._maxhp.ToString();
        _paramText[1].text = User._user._parameter._speed.ToString();
        _paramText[2].text = User._user._parameter._stamina.ToString();
        _paramText[3].text = User._user._parameter._intelligence.ToString();
        _paramText[4].text = User._user._parameter._guts.ToString();

        _paramexText[0].text = (User._user._parameter.GetSingleParam(GameUserParameter.maxhp) - User._user._PrevParams[GameUserParameter.maxhp]).ToString();
        _paramexText[1].text = (User._user._parameter.GetSingleParam(GameUserParameter.speed) - User._user._PrevParams[GameUserParameter.speed]).ToString();
        _paramexText[2].text = (User._user._parameter.GetSingleParam(GameUserParameter.stamina) - User._user._PrevParams[GameUserParameter.stamina]).ToString();
        _paramexText[3].text = (User._user._parameter.GetSingleParam(GameUserParameter.intelligence) - User._user._PrevParams[GameUserParameter.intelligence]).ToString();
        _paramexText[4].text = (User._user._parameter.GetSingleParam(GameUserParameter.guts) - User._user._PrevParams[GameUserParameter.guts]).ToString();

        
    }

    void OnDisable() {
        
    }

    
}
