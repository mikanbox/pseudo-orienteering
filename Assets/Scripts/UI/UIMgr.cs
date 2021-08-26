using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIMgr : MonoBehaviour
{
    public ScreenSimpleText _screenSimpleText;
    public FadeScreen _fadescreen;

    
    public static UIMgr _uimgr;
    void Awake()
    {
        _uimgr = (UIMgr)this;
    }

}
