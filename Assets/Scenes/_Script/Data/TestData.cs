using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class TestData
{
    public int a;
    [System.NonSerialized]
    public GameObject b;
    [System.NonSerialized]
    public BattleChar c;

    public TestData(int a)
    {
        this.a = a;
    }
}
