using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataSaveMgr : MonoBehaviour
{

// Data list
// user exp / param
// stage data
// option data


    public static void load () { 
        User._user._parameter = SaveLoadJson.LoadFromToPlayerPrefs<GameParameter>("userParam");            
        User._user._userexp = SaveLoadJson.LoadFromToPlayerPrefs<GameUserExp>("userExp");
        GameMgr._gamemgr._gamesavedata = SaveLoadJson.LoadFromToPlayerPrefs<GameGlobalSaveData>("GameGlobalSaveData");
    }


// セーブするデータ
// コースタイム
// ユーザーパラメータ
// ユーザーexp
// ユーザーLV

    public static void save() {
        SaveLoadJson.SaveToPlayerPrefs<GameParameter>("userParam",User._user._parameter);
        SaveLoadJson.SaveToPlayerPrefs<GameUserExp>("userExp",User._user._userexp);
        SaveLoadJson.SaveToPlayerPrefs<GameGlobalSaveData>("GameGlobalSaveData",GameMgr._gamemgr._gamesavedata);
        SaveLoadJson.SaveToPlayerPrefs<string>("firstsave","true");
    }


    public static bool isDataExists() {
        return (SaveLoadJson.LoadFromToPlayerPrefs<string>("firstsave") != null);
    }


}
