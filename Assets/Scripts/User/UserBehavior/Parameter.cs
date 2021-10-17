using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;
public class GameParameter 
{
    public int _speed;
    public int _stamina;
    public int _intelligence;
    public int _guts;

    public float _hp;
    public float _maxhp;

    public float _trackingPosition;

    public static Dictionary<GameUserParameter,string> _gameUserParameterToName = new Dictionary<GameUserParameter,string>() {
        {GameUserParameter.speed,"speed"},
        {GameUserParameter.stamina,"stamina"},
        {GameUserParameter.intelligence,"intelligence"},
        {GameUserParameter.guts,"guts"},
        {GameUserParameter.maxhp,"maxhp"},
    };


    public GameParameter(int speed, int stamina, int intelligence, int guts) {
        _speed = speed;
        _stamina = stamina;
        _intelligence = intelligence;
        _guts = guts;

        _maxhp = stamina;
        _hp = _maxhp;
        _trackingPosition = 0;
    }
}




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

    public void updateLV (int speedlv,int staminalv,int intelligencelv,int gutslv,int maxhplv) {
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
    
    
}



public enum GameUserParameter {
    speed,
    stamina,
    intelligence,
    guts,
    maxhp
}



public enum GameUserSkillParameter {
    shoes,
    grobe,
    book,
    hatimaki
    
}





