using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class SkillInfo
{
    public string skillName;

    public int damage;
    public int mana;
    public int cooldown;

    public List<int> area = new List<int>();

    public bool isCasting = false;
    public bool isContinuously = false;
    public bool isTargeting = false;

    public List<int> skillDatas = new List<int>();
}
