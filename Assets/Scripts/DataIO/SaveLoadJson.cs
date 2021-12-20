using System;
using UnityEngine;


public class SaveLoadJson : MonoBehaviour
{
    public static void Load<T>(ref T obj, String filepath) {
        string inputString = Resources.Load<TextAsset>(filepath).ToString();
        // Debug.Log(inputString);
        obj = JsonUtility.FromJson<T>(inputString);
        // Debug.Log(JsonUtility.ToJson( obj ));
    }

    public static void SaveToPlayerPrefs<T>(string key, T obj) {
        var json = JsonUtility.ToJson( obj );
        // Debug.Log(json);
        PlayerPrefs.SetString( key, json );
    }

    public static T LoadFromToPlayerPrefs<T>(string key) {
        var json = PlayerPrefs.GetString( key );
        var obj = JsonUtility.FromJson<T>( json );
        return obj;
    }
    
}

