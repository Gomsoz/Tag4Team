using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterInfo
{
    protected CharacterStatData stat;
    public CharacterStatData Stat
    {
        get
        {
            isViewedData = true;
            return stat;
        }
        set
        {
            stat = value;
        }
    }

    protected bool isViewedData = true;

    protected CharacterExtraStatData extraStat = new();
    public CharacterExtraStatData ExtraStat { get => extraStat; }

    public Action UpdateImprovementAbilityStatPublisher = null;

    public Sprite CharacterImage;

    public void UpdateImprovementAbilityStatData()
    {
        if (isViewedData == false)
            return;

        stat.MaxHp = (int)Math.Round(((stat.OriginHp + stat.FixedHp) * (1 + stat.PercentageHp)));
        stat.MaxMp = (int)Math.Round(((stat.OriginMp + stat.FixedMp) * (1 + stat.PercentageMp)));
        stat.Dmg = (int)Math.Round(((stat.OriginDmg + stat.FixedDmg) * (1 + stat.PercentageDmg)));
        stat.Def = (int)Math.Round(((stat.OriginDef + stat.FixedDef) * (1 + stat.PercentageDef)));
        stat.AttackSpeed = ((stat.OriginAS + stat.FixedAS) * (1 + stat.PercentageAS));
        stat.MoveSpeed = ((stat.OriginMS + stat.FixedMS) * (1 + stat.PercentageMS));

        UpdateImprovementAbilityStatPublisher?.Invoke();
        isViewedData = false;
    }
}
