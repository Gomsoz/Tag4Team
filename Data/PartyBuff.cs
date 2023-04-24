using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class PartyBuffData
{
    public int Point;
    public int DefaultPoint = 10;

    public int HpLevel;
    public float HpPerLevel;

    public int MpLevel;
    public float MpPerLevel;

    public int DmgLevel;
    public float DmgPerLevel;

    public int AmorLevel;
    public float AmorPerLevel;
}
