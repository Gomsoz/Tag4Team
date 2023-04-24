using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tanker_RisingShield : HeroSkill
{
    [SerializeField]
    private GameObject skillEffect;
    private GameObject skillEffectInstance;

    public override void UseSkill(Action _endSkillCallback = null, Action _endCastingCallback = null)
    {
        base.UseSkill();

        Buff_RisingShield buff = new Buff_RisingShield();
        buff.Init(thisSkillData.BuffDuration, thisSkillData.Name, skillOwner, EndBuffCallback);

        skillEffectInstance = Instantiate(skillEffect, runningPosition + new Vector3(0, 0.3f, 0), Quaternion.identity, skillOwner.transform);

        skillOwner.AddBuffOnPlaying(buff);

        StartCoroutine(SkillAction(activationTime));

        EndSkill();
    }

    public void EndBuffCallback()
    {
        Destroy(skillEffectInstance);
    }
}

public class Buff_RisingShield : BuffOnPlaying
{
    public override void Apply()
    {
        base.Apply();

        buffOwner.AdjustExtraStatData(CharacterExtraStat.ReductionDamage, 0.1f);
    }

    public override void Revert()
    {
        base.Revert();

        buffOwner.AdjustExtraStatData(CharacterExtraStat.ReductionDamage, -0.1f);
    }
}
