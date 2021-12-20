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


    public GameParameter(){
        
    }

    public GameParameter(float maxhp, int speed, int stamina, int intelligence, int guts) {
        _maxhp = maxhp;
        _speed = speed;
        _stamina = stamina;
        _intelligence = intelligence;
        _guts = guts;

        
        _hp = _maxhp;
        _trackingPosition = 0;
    }

// とりあえずレベルあたり1 上がる
    public void addSingleParam(GameUserParameter type,int value) {
        switch(type) {
            case GameUserParameter.speed:
                _speed+=value;
            break;
            case GameUserParameter.stamina:
                _stamina+=value;
            break;
            case GameUserParameter.intelligence:
                _intelligence+=value;
            break;
            case GameUserParameter.guts:
                _guts+=value;
            break;
            case GameUserParameter.maxhp:
                _maxhp+=(float)value;
            break;
        }
    }

    public int GetSingleParam(GameUserParameter type) {
        int res=0;
                switch(type) {
            case GameUserParameter.speed:
                res = _speed;
            break;
            case GameUserParameter.stamina:
                res =_stamina;
            break;
            case GameUserParameter.intelligence:
                res = _intelligence;
            break;
            case GameUserParameter.guts:
                res = _guts;
            break;
            case GameUserParameter.maxhp:
                res = (int)_maxhp;
            break;
        }
        return res;
    }

    public static int CalcParamperLV(GameUserParameter type,int value){
        int res = 0;
                switch(type) {
            case GameUserParameter.speed:
                res = value * 1;
            break;
            case GameUserParameter.stamina:
                res = value * 1;
            break;
            case GameUserParameter.intelligence:
                res = value * 1;
            break;
            case GameUserParameter.guts:
                res = value * 1;
            break;
            case GameUserParameter.maxhp:
                res = value * 10;
            break;
        }
        return res;
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





