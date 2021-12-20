using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;





public class GameGlobalClasses : MonoBehaviour
{
    public static GameGlobalClasses _gameglobal;
    public TerrainsData _terrains;  
    public CupsData _cups;

    
    void Start() {
        _gameglobal = this.GetComponent<GameGlobalClasses>();
        SaveLoadJson.Load<TerrainsData>(ref _terrains,"GameData/Terrains");
        foreach(var item in _terrains.terrains)
            item.geology.convertFromDataToDictionary();

        SaveLoadJson.Load<CupsData>(ref _cups,"GameData/Cups");

        Debug.Log("====== GameLoad =======");
    }
}



[Serializable]
public class TerrainData
{
    public int id;
    public string name;
    public Geology geology;

    public String geology_string;

}


[Serializable]
public class TerrainsData
{
    public TerrainData[] terrains;
}



[Serializable]
public class CupData
{
    public int id;
    public string name;
    
    public int terrain;
    public int distance;
    public int difficulty;
    public int targetrecord;
}


[Serializable]
public class CupsData
{
    public CupData[] cups;
}



public enum GameState
{
    PreStart,
    CountDown,
    Playing,
    Pause,
    Finish,
    NotPlaying,
    WalkToStart

}
