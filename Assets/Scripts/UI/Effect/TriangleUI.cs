using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


[RequireComponent(typeof(CanvasRenderer))]
[RequireComponent(typeof(RectTransform))]
public class TriangleUI : Graphic
{
    [SerializeField]
    private float x1,y1;

    [SerializeField]
    private float x2,y2;
    [SerializeField]
    private float x3,y3;
    [SerializeField]
    private Color _color;

    protected override void OnPopulateMesh(VertexHelper vh)
    {
        // vn がメッシュオブジェクト。これに追加することで描画
        vh.Clear();
        UIVertex v = UIVertex.simpleVert;
        // v.color = this.transform.parent.GetComponent<Image>().color + new Color(0.1f,0.1f,0.1f);
        v.color = _color;

        v.position = CreatePos(x1, y1);
        vh.AddVert(v);
        v.position = CreatePos(x2, y2);
        vh.AddVert(v);
        v.position = CreatePos(x3, y3);
        vh.AddVert(v);



        vh.AddTriangle(0, 1, 2);
    }

    
    // ワールド座標を[ 0 - 1 ]の範囲に限定する
    private Vector2 CreatePos(float x, float y)
    {
        Vector2 p = Vector2.zero;
        p.x -= rectTransform.pivot.x;
        p.y -= rectTransform.pivot.y;
        p.x += x;
        p.y += y;
        p.x *= rectTransform.rect.width;
        p.y *= rectTransform.rect.height;
        return p;
    }
}
