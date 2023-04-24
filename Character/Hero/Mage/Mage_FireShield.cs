using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mage_FireShield : HeroSkill
{
    [SerializeField]
    private GameObject skillEffect;
    private GameObject skillEffectInstance;

    private float reflectionRatio = 0.3f;

    public override void EndCasting()
    {
        base.EndCasting();

        Buff_FireShield buff = new Buff_FireShield();
        buff.Init(thisSkillData.BuffDuration, thisSkillData.Name, skillOwner, EndBuffCallback);
        skillOwner.AddBuff_Reflection(buff, reflectionRatio);

        skillEffectInstance = Instantiate(skillEffect, runningPosition + new Vector3(0, 0.3f, 0) , Quaternion.identity, skillOwner.transform);

        StartCoroutine(SkillAction(activationTime));

        EndSkill();
    }

    public void EndBuffCallback()
    {
        Destroy(skillEffectInstance);
    }
}

public class Buff_FireShield : BuffOnPlaying
{
    public override void Apply()
    {
        base.Apply();

        buffOwner.SetCharacterState(CharacterState.Reflection, true);
    }

    public override void Revert()
    {
        base.Revert();

        buffOwner.SetCharacterState(CharacterState.Reflection, false);
    }
}
