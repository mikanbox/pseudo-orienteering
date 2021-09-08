using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlinkText : MonoBehaviour
{
    UnityEngine.UI.Text character;
    public float speed = 0.5f;
    private float time;

    void Start()
    {
        character = this.gameObject.GetComponent<UnityEngine.UI.Text>(); 
    }

    void Update()
    {
        character.color = GetAlphaColor(character.color);
    }


    private Color GetAlphaColor (Color color) {
        time += Time.deltaTime * 2.0f * speed;
        color.a = Mathf.Sin (time) * 0.5f + 0.5f;

        return color;
    }



}
