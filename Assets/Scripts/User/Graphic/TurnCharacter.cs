using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class TurnCharacter : MonoBehaviour {

    public float _direction = 0;
    private bool _isRight = true;

    [SerializeField] private static int _speed = 20;

    [SerializeField]
    private UnityEngine.ParticleSystemRenderer _drops;


    void FixedUpdate() {
        if (_isRight == true && _direction > 0) {
            _direction -= _speed;
            if (_direction <= 0)
                _direction = 0;
            this.transform.localRotation = Quaternion.Euler(0, _direction, 0);
        }
        
        if (_isRight == false && _direction < 180) {
            _direction += _speed;
            if (_direction >= 180)
                _direction = 180;
            this.transform.localRotation = Quaternion.Euler(0, _direction, 0);
        }
    }

    public void ChangeDirection(bool isRight){
        if (_isRight != isRight){
            _isRight = isRight;


            var em =  _drops.flip;
            em.x = Convert.ToInt32(!_isRight);
            _drops.flip = em;

        }
    }

}
