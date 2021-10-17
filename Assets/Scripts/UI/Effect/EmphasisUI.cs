using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmphasisUI : MonoBehaviour
{

    RectTransform ui;
    
    public float speed = 1f;
    private float time;
    [SerializeField]
    private float delay = 0f;
    
    void Start()
    {
        ui = this.gameObject.GetComponent<RectTransform>(); 
        time += delay;
    }

    void Update()
    {
        ui.localScale = GetUISize(ui.localScale);
    }


    private Vector3 GetUISize (Vector3 scale) {
        time += Time.deltaTime * 2.0f * speed;
        scale = Vector3.one * Mathf.Sin (time) * 0.01f + Vector3.one ;

        return scale;
    }
}
