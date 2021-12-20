using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class StargeRecord
{
    public float _BestRecord;
    public int _PlayTimes;
    public float _TotalDistance;

    public StargeRecord(){
        _BestRecord = -1;
        _PlayTimes = 0;
        _TotalDistance = 0;
    }
}
