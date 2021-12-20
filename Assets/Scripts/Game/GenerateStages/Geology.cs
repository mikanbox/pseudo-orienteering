using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[System.Serializable]
public class Geology {

    public Dictionary<MapCode,List<string>> MapCode_Sprite_Mappings;
    public Dictionary<MapCode,int> MaptipRatio;
    public List<SerializableMapTipdata> MaptipRatioData;
    

    public Geology () 
    {
        MaptipRatio = new Dictionary<MapCode, int>();
        MapCode_Sprite_Mappings = new Dictionary<MapCode, List<string>>();
    }

    public int TotalRatioAmmount ()
    {
        int a = 0;
        foreach (KeyValuePair<MapCode, int> item in MaptipRatio) {
            a += item.Value;
        }
        return a;
    }

    public void convertFromDataToDictionary(){
        foreach (SerializableMapTipdata item in MaptipRatioData) {
            Debug.Log(item.name);
            Debug.Log(item.spritenames[0]);
            MaptipRatio.Add(  (MapCode)Enum.Parse(typeof(MapCode), item.name, true) , item.value);
            MapCode_Sprite_Mappings.Add( (MapCode)Enum.Parse(typeof(MapCode), item.name, true), item.spritenames);
        }
    }
}

[System.Serializable]
public class SerializableMapTipdata {
    public string name;
    public int value;
    public List<string> spritenames;
}