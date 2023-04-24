using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordMan_Strength : HeroSkill
{
    private int damageBuffDuration = 10;
    private int invincibilityBuffDuration = 3;

    [SerializeField]
    private GameObject skillEffect;

    public override void UseSkill(Action _endSkillCallback = null, Action _endCastingCallback = null)
    {
        base.UseSkill(_endSkillCallback, _endCastingCallback);

        Instantiate(skillEffect, runningPosition, Quaternion.identity);
        BuffOnPlaying buff = new Buff_Strength_DamageAndSpeed();
        buff.Init(damageBuffDuration, "Strenth : Damage", skillOwner);

        skillOwner.AddBuffOnPlaying(buff);

        buff = new Buff_Strength_Invincibility();
        buff.Init(invincibilityBuffDuration, "Strenth : Invincibility", skillOwner);

        skillOwner.AddBuffOnPlaying(buff);
        activationTime = 0.1f;

        StartCoroutine(SkillAction(activationTime));
    }
}

public class Buff_Strength_DamageAndSpeed : BuffOnPlaying
{

    public override void Apply()
    {
        base.Apply();

        buffOwner.SetCharacterState(CharacterState.Berserk, true);
        buffOwner.AdjustStatData(ImprovementAbilityStat.PercentageDamage, 0.5f);
        buffOwner.AdjustStatData(ImprovementAbilityStat.PercentageMs, 0.2f);
    }

    public override void Revert()
    {
        base.Revert();

        buffOwner.SetCharacterState(CharacterState.Berserk, false);
        buffOwner.AdjustStatData(ImprovementAbilityStat.PercentageDamage, -0.5f);
        buffOwner.AdjustStatData(ImprovementAbilityStat.PercentageMs, -0.2f);
    }
}

public class Buff_Strength_Invincibility : BuffOnPlaying
{
    public override void Apply()
    {
        base.Apply();

        buffOwner.SetCharacterState(CharacterState.Invincibility, true);
    }

    public override void Revert()
    {
        base.Revert();

        buffOwner.SetCharacterState(CharacterState.Invincibility, false);
    }
}
