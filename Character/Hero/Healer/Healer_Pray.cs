using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Healer_Pray : HeroSkill
{
    [SerializeField]
    private GameObject skillEffectPrefab;
    private List<GameObject> skillEffects = new();
    Buff_Pray buff;
    

    public override void UseSkill(Action _endSkillCallback = null, Action _endCastingCallback = null)
    {
        base.UseSkill(_endSkillCallback, _endCastingCallback);

        buff = new Buff_Pray();
        
        for (int i = 0; i < 4; i++)
        {
            HeroBehavior hero = PlayerController.Instance.GetHero(i);
            buff.Init(thisSkillData.BuffDuration, thisSkillData.Name, hero, EndBuffCallback);

            hero.AddBuffOnPlaying(buff);
            hero.FlipPublisher += FlipListener;

            GameObject effect = Instantiate(skillEffectPrefab, hero.transform);
            skillEffects.Add(effect);
        }

        StartCoroutine(SkillAction(activationTime));

        EndSkill();
    }

    public void FlipListener(int value)
    {
        Transform tr = skillEffects[PlayerController.Instance.CurHeroIdx].transform;

        Vector3 scale = tr.localScale;
        tr.localScale = new Vector3(scale.y * value, scale.y, scale.z);
    }

    public void EndBuffCallback()
    {
        if(skillEffects.Count != 0)
        {
            GameObject[] effects = new GameObject[skillEffects.Count];
            effects = skillEffects.ToArray();
            foreach (var item in effects)
            {
                Destroy(item);
            }

            skillEffects.Clear();

            for (int i = 0; i < 4; i++)
            {
                HeroBehavior hero = PlayerController.Instance.GetHero(i);
                hero.FlipPublisher -= FlipListener;
            }
        }
    }
}

public class Buff_Pray : BuffOnPlaying
{
    public override void Apply()
    {
        base.Apply();

        buffOwner.AdjustStatData(ImprovementAbilityStat.PercentageDamage, 0.1f);
    }

    public override void Revert()
    {
        base.Revert();

        buffOwner.AdjustStatData(ImprovementAbilityStat.PercentageDamage, -0.1f);
    }
}
