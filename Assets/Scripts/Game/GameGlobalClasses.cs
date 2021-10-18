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
        LoadJson.Load<TerrainsData>(ref _terrains,"GameData/Terrains");
        LoadJson.Load<CupsData>(ref _cups,"GameData/Cups");
        Debug.Log(_terrains.terrains[0].name);
    }
}



[Serializable]
public class TerrainData
{
    public int id;
    public string name;
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
