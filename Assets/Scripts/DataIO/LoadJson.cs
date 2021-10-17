using System;
using UnityEngine;


public class LoadJson : MonoBehaviour
{
    public static void Load<T>(ref T obj,String filepath) {

        string inputString = Resources.Load<TextAsset>(filepath).ToString();
        
        obj= JsonUtility.FromJson<T>(inputString);

    }
}

