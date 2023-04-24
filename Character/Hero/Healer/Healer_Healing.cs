using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Healer_Healing : HeroSkill
{
    private int healingAmount = 30;
    private float healingFactor = 1f;

    [SerializeField]
    private GameObject effect;

    public override void UseSkill(Action _endSkillCallback = null, Action _endCastingCallback = null)
    {
        base.UseSkill(_endSkillCallback);

        targetedHero.Healing(skillOwner, ConvertDamage(healingAmount, healingFactor));

        StartCoroutine(SkillAction(activationTime));

        EndSkill();
    }
}
