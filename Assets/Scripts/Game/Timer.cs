using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public class Timer : MonoBehaviour  {

    public float _starttime = 0;
    public float _nowtime;
    public float _recordedTime;

    void FixedUpdate() {
        _nowtime = Time.time;
    }

    public void RecordStart()
    {
        _starttime = Time.time;
    }

    public void RecordStop()
    {
        _recordedTime = GetElapsedTime();
    }

    public float GetElapsedTime()
    {
        return Time.time - _starttime;
    }

    public string GetHHMMSS(float seconds ){
        var span = new TimeSpan(0, 0, (int)seconds);
        var hhmmss = span.ToString(@"hh\:mm\:ss");
        
        return hhmmss;
    }

    public string GetMMSSFF(float seconds ){
        var span = TimeSpan.FromMilliseconds(seconds * 1000);
        var hhmmss = span.ToString(@"mm\:ss");
        hhmmss+="."+ string.Format("{0:D2}", seconds * 10);
        
        return hhmmss;
    }

}
