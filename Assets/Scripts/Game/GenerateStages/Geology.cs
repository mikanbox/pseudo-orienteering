using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Geology {
    // public int _forest_amount;
    // public int _watercourse_amount; //水系
    // public int _boulder_cluster_amount; // 礫地
    // public int _open_land_amount; // open
    // public int _road_amount;
    // public int _vegetation_amount; //ハッチ

    public Dictionary<MapCode,int> MaptipRatio;
    
    public Geology () 
    {
        MaptipRatio = new Dictionary<MapCode, int>();
    }

    public int TotalAmmount ()
    {
        int a = 0;
        foreach (KeyValuePair<MapCode, int> item in MaptipRatio) {
            a += item.Value;
        }
        return a;
    }

}