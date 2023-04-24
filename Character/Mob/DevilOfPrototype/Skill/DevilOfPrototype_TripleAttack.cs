using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DevilOfPrototype_TripleAttack : MobSkill
{
    public override void SkillInit(MobBehavior owner)
    {
        thisSkill = this;
        minSkillRange = 1;
        maxSkillRange = 2;

        skillCooldownInfo = new()
        {
            skillName = "TripleAttack",
            totalCooldown = 30,
        };

        base.SkillInit(owner);
    }

    public override void UseSkill(Action _endSkillCallback = null)
    {
        base.UseSkill(_endSkillCallback);

        EndSkill();
    }
}
