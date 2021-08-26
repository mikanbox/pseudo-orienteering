using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour  {

    public float _starttime = 0;
    public float _nowtime;
    void FixedUpdate() {
        _nowtime = Time.time;
    }

    public void RecordStart()
    {
        _starttime = Time.time;
    }

    public float GetElapsedTime()
    {
        return Time.time - _starttime;
    }


}
