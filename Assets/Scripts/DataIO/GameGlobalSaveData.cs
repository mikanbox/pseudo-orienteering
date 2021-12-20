using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[SerializeField]
public class GameGlobalSaveData
{
    public List<bool> isClearedCups;
    public List<bool> isOpenedTerrains;
    public List<bool> isOpenedCups;
    public List<StargeRecord> _records;
    public List<bool> isTutorialWatchedFlags;

    public GameGlobalSaveData(){
        isClearedCups = new List<bool>();
        isOpenedTerrains = new List<bool>();
        isOpenedCups = new List<bool>();
        _records = new List<StargeRecord>();
        isTutorialWatchedFlags = new List<bool>();
    }

}
