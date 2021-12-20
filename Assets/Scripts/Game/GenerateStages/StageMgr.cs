using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using System.Reflection;
using System;

public class StageMgr  
{
    public Stage _stage;
    public static int _margin = 10;
    public static int DISTMAG = 3;


    public static StageMgr _stagemgr;
    // これを外部から参照することでゲームを生成する
    public List<List<Maptip>> stageArray;

    public static void GenerateMapArray(int terrainId)
    {
        _stagemgr = new StageMgr(terrainId);
    }

    public StageMgr (int terrainId)
    {
        _stage = new Stage(terrainId);
        GenerateStageArrayFromData();
        UnityEngine.Random.InitState(System.DateTime.Now.Millisecond);
    }



    private void GenerateStageArrayFromData()
    {
        // Generate Start [x axis][height]
        this.stageArray = new List<List<Maptip>> (_stage._total_distance * DISTMAG + _margin * 2);
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
        int total_ratio_amount = g.TotalRatioAmmount();
        int total_dist = _stage._total_distance * DISTMAG;
        List<MapCode> array = new List<MapCode>();

        // 割合ベースでMapCodeを配列に格納した後、順番をシャッフル
        foreach (MapCode mp in Enum.GetValues(typeof(MapCode)))
        {
            if (g.MaptipRatio.ContainsKey(mp)) {
                int amount = (g.MaptipRatio[mp] * total_dist) / total_ratio_amount;
                for (int i = 0;i < amount;i++)
                    array.Add(mp);
            }
        }
        for(int i = array.Count; i < total_dist; i++) 
            array.Add(MapCode.Open);
        
        Shuffle(array);


        // 作成した arrayを元に stageArray (MapTip) へ追加
        // 実際の距離は　距離 * DISTMAG - 2
        int d = 10;
        for(int i = 0; i < _stage._sections.Count; i++ ) {
            Section s = _stage._sections[i];
            for(int j = 0; j < s._distance * DISTMAG; j++) {
                stageArray.Add(new List<Maptip>());

                stageArray[d].Add(new Maptip(array[d - 10], 0, true));

                if ( j == s._distance * DISTMAG - 1 ) {
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

    public bool isReachGoal(int x){
        return (_stage._total_distance * DISTMAG  <= x - 10);
    }
}
