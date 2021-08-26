using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using System.Reflection;
using System;

public class StageMgr  
{
    public Stage _stage;

    private int _random_seed = 0;
    public static int _margin = 10;


    public static StageMgr _stagemgr;

    // これを外部から参照することでゲームを生成する
    public List<List<Maptip>> stageArray;

    public static void GenerateMapArray(){
        _stagemgr = new StageMgr();
        
    }

    public StageMgr ()
    {
        LoadStageData();
        GenerateStageFromData(_random_seed);

    }


    private void LoadStageData()
    {
        _stage = new Stage();
    }

    private void GenerateStageFromData(int _random_seed)
    {
        // Generate Start
        this.stageArray = new List<List<Maptip>> (_stage._total_distance + _margin * 2);
        for (int i = 0; i < _margin; i++ ) {
            stageArray.Add(new List<Maptip>());
            if (i == _margin - 1) {
                stageArray[i].Add(new Maptip(MapCode.Start4, 0, true));
                continue;
            }
                
            if (i == _margin - 3) {
                stageArray[i].Add(new Maptip(MapCode.Start3, 0, true));
                continue;
            }
                
            if (i == _margin - 7) {
                stageArray[i].Add(new Maptip(MapCode.Start2, 0, true));
                continue;
            }
                
            if (i == _margin - 5) {
                stageArray[i].Add(new Maptip(MapCode.Start1, 0, true));
                continue;
            }
                
            stageArray[i].Add(new Maptip(MapCode.Rane, 0, true));
        }




        // Generate Maps

        Geology g = _stage._geology;
        int total_amount = g.TotalAmmount();
        int total_dist = _stage._total_distance * _margin;
        List<MapCode> array = new List<MapCode>();

        foreach (MapCode mp in Enum.GetValues(typeof(MapCode)))
        {
            if (g.MaptipRatio.ContainsKey(mp)) {
                int amount = (g.MaptipRatio[mp] * total_dist) / total_amount;
                for (int i = 0;i < amount;i++)
                    array.Add(mp);
            }
        }


        for(int i = array.Count; i < total_dist; i++) {
            array.Add(MapCode.Open);
        }
        Shuffle(array);




        int d = 10;
        for(int i = 0; i < _stage._sections.Count; i++ ) {
            Section s = _stage._sections[i];
            for(int j = 0; j < s._distance * 10; j++) {
                stageArray.Add(new List<Maptip>());

                // stageArray[d].Add(new Maptip(0, 0, true));
                stageArray[d].Add(new Maptip(array[d - 10], 0, true));

                if ( j == s._distance * 10 - 1 ) {
                    if ( d == total_dist + 10 -1) {
                        stageArray[d].Add(new Maptip(MapCode.Open, 0, true));
                        continue;
                    }
                        
                    stageArray[d].Add(new Maptip(MapCode.Post, 0, true));
                }
                    
                if ( d == 10) 
                    stageArray[d].Add(new Maptip(MapCode.PostWithRane, 0, true));
                
                d++;
            }
        }

        // Generate Goal

        for (int i = 0; i < 10; i++ ) {
            stageArray.Add(new List<Maptip>());
            if (i == 0) {
                stageArray[d+i].Add(new Maptip(MapCode.Goal1, 0, true));
                continue;
            }
            if ( i % 2 ==0) {
                stageArray[d+i].Add(new Maptip(MapCode.Goal2, 0, true));
            } else {
                stageArray[d+i].Add(new Maptip(MapCode.Goal3, 0, true));
            }
        }
    }

    private void Shuffle<T> (List<T> num) 
    {
        for (int i = 0; i < num.Count; i++)
        {
            var temp = num[i]; 
            int randomIndex = UnityEngine.Random.Range(0, num.Count); 
            num[i] = num[randomIndex]; 
            num[randomIndex] = temp; 
        }
    }

    private void TestSetUserData()
    {
        this.stageArray = new List<List<Maptip>> (100);
        for(int i = 0; i < stageArray.Capacity; ++i ) {
            stageArray.Add(new List<Maptip>());
            // stageArray[i].Add(new Maptip(1, 1, true));
        }
    }

    public bool isReachGoal(Vector3 pos){
        int x = (int)pos.x + 1;
        return (_stage._total_distance * 10  <= x - 10);
    }
}
