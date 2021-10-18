using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;




public class GameUserExp {
    float _speed = 0;
    float _stamina = 0;
    float _intelligence = 0;
    float _guts = 0;
    float _maxhp = 0;



    public int _speedlv = 1;
    public int _staminalv = 1;
    public int _intelligencelv = 1;
    public int _gutslv = 1;
    public int _maxhplv = 1;


    public GameUserExp() {
        _speed = 0;
        _stamina = 0;
        _intelligence = 0;
        _guts = 0;
        _maxhp = 0;
    }

    public void addExp (float speed, float stamina, float intelligence, float guts, float maxhp) {
        _speed = speed;
        _stamina = stamina;
        _intelligence = intelligence;
        _guts = guts;
        _maxhp = maxhp;
    }

    public void addExp (GameUserExp addexp) {
        _speed += addexp._speed;
        _stamina += addexp._stamina;
        _intelligence += addexp._intelligence;
        _guts += addexp._guts;
        _maxhp += addexp._maxhp;
    }




    public void setLV (int speedlv,int staminalv,int intelligencelv,int gutslv,int maxhplv) {
        _speedlv = speedlv;
        _staminalv = staminalv;
        _intelligencelv = intelligencelv;
        _gutslv = gutslv;
        _maxhplv = maxhplv;
    }




    public Dictionary<GameUserParameter,int> isLVUpCheck() {
        Dictionary<GameUserParameter,int> lvUpList = new Dictionary<GameUserParameter, int>();
        foreach (var i in Enum.GetValues(typeof(GameUserParameter)).Cast<GameUserParameter>())
            lvUpList[i] = 0;
        

        while(Mathf.Pow(2f,   Mathf.Pow( Mathf.Log(_speedlv), 2)  ) * 100 < _speed) {
            _speedlv++;
            lvUpList[GameUserParameter.speed]++;
        }

        while(Mathf.Pow(2f,   Mathf.Pow( Mathf.Log(_staminalv), 2)  ) * 100 < _stamina) {
            _staminalv++;
            lvUpList[GameUserParameter.stamina]++;
        }

        while(Mathf.Pow(2f,   Mathf.Pow( Mathf.Log(_intelligencelv), 2)  ) * 100 < _intelligence) {
            _intelligencelv++;
            lvUpList[GameUserParameter.intelligence]++;
        }

        while(Mathf.Pow(2f,   Mathf.Pow( Mathf.Log(_gutslv), 2)  ) * 100 < _guts) {
            _gutslv++;
            lvUpList[GameUserParameter.guts]++;
        }

        while(Mathf.Pow(2f,   Mathf.Pow( Mathf.Log(_maxhplv), 2)  ) * 100 < _maxhp) {
            _maxhplv++;
            lvUpList[GameUserParameter.maxhp]++;
        }

        return lvUpList;
    }
    

    public void MoveToNewMapTip(MapCode code) {
        float stamina = 0;
        float speed = 0;
        float intelligence = 0;
        float guts = 0;
        float maxhp = 0;

        speed += 0.01f;

        switch(code) {
            case MapCode.Forest:
            break;
        }
        this.addExp(speed,stamina,intelligence,guts,maxhp);
    }

    public void GoalCup() {
        float stamina = 0;
        float speed = 0;
        float intelligence = 0;
        float guts = 0;
        float maxhp = 0;

        maxhp += 1000;
        guts += 1000f;

        this.addExp(speed,stamina,intelligence,guts,maxhp);
    }
    
}