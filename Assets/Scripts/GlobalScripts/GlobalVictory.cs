using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalVictory : MonoBehaviour
{
    public static GlobalVictory instance;
    public bool IsVictory;
    private void Awake()
    {
        if (instance != null) Destroy(this.gameObject);
        instance = this;

        DontDestroyOnLoad(this.gameObject);
    }
}
