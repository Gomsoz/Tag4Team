using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[Serializable]
public class SkillData
{
    public string Name;
    public string Id;
    public int Mana;
    public int Cooldown;
    public List<int> Datas= new List<int>();

    public bool IsContinuously = false;
    public int SkillDuration = 0;
    public bool IsBuff = false;
    public int BuffDuration = 0;
    public bool IsCasting = false;
    public int CastingTime = 0;
    public bool isTargeting = false;
    public bool isSelectLocation = false;
}
