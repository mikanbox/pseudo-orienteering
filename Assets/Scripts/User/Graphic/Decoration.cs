using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Decoration 
{
    private SpriteRenderer _main;
    private SpriteRenderer _head;
    private SpriteRenderer _leg;
    private SpriteRenderer _ornaments;

    Decoration(SpriteRenderer main, SpriteRenderer head, SpriteRenderer leg, SpriteRenderer ornaments) {
        _main = main;
        _head = head;
        _leg = leg;
        _ornaments = ornaments;
    }

    protected void SetSprites(Sprite main, Sprite head, Sprite leg, Sprite ornaments){
        if (main != null)
            _main.sprite = main;

        if (head != null)
            _head.sprite = head;

        if (leg != null)
            _leg.sprite = leg;

        if (ornaments != null)
            _ornaments.sprite = ornaments;

    }

}
