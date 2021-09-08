using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class GameParameter 
{
    public int _speed;
    public int _stamina;
    public int _intelligence;
    public int _guts;

    public float _hp;
    public float _maxhp;

    public float _trackingPosition;

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

public enum GameUserParameter {
    speed,
    stamina,
    intelligence,
    guts
}



