using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class HeroInfo : CharacterInfo
{
    private HeroData herodata = new HeroData();
    public HeroData Herodata { get => herodata; set { herodata = value; } }

    public HeroJobs HeroJob
    {
        get
        {
            return (HeroJobs)herodata.HeroCode;
        }
    }

    public HeroSkillData skill = new HeroSkillData();

    #region Extra
    private Dictionary<string, BuffBase> buffs = new Dictionary<string, BuffBase>();
    private List<Feature_Base> features = new List<Feature_Base>();
    public List<Feature_Base> Feature { get => features; }
    #endregion

    public void ResetFeature()
    {
        features.Clear();
    }

    public void ApplyFeature(Feature_Base feature)
    {
        if (features.Contains(feature))
            return;

        features.Add(feature);
        feature.Apply(this);
        UpdateImprovementAbilityStatData();
    }

    public void RevertFeature(Feature_Base feature)
    {
        features.Remove(feature);
        feature.Revert(this);
        UpdateImprovementAbilityStatData();
    }

    public void ApplyBuff(BuffBase _buff)
    {
        buffs.Add(_buff.BuffName, _buff);
        _buff.Apply();
    }

    public void RevertBuff(BuffBase _buff)
    {
        buffs.TryGetValue(_buff.BuffName, out BuffBase appliedBuff);
        appliedBuff?.Revert();
        buffs.Remove(_buff.BuffName);
    }
}
