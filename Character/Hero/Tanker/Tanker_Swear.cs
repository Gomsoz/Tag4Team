using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tanker_Swear : HeroSkill
{
    public override void UseSkill(Action _endSkillCallback = null, Action _endCastingCallback = null)
    {
        base.UseSkill(_endSkillCallback, _endCastingCallback);

        Debug.Log("Swear");
        Buff_Swear_Guest guestBuff = new();
        guestBuff.Init(thisSkillData.BuffDuration, thisSkillData.Name, targetedHero);

        targetedHero.AddBuffOnPlaying(guestBuff);

        Buff_Swear_Host hostBuff = new();
        hostBuff.Init(thisSkillData.BuffDuration, thisSkillData.Name, skillOwner, targetedHero);

        skillOwner.AddBuffOnPlaying(hostBuff);

        EndSkill();
    }

    public override void EndSkill()
    {
        base.EndSkill();
    }
}

public class Buff_Swear_Host : BuffOnPlaying
{
    public override void Apply()
    {
        base.Apply();

        target.DamagedPublisher -= DamagedInstead;
        target.DamagedPublisher += DamagedInstead;
    }

    public override void Revert()
    {
        base.Revert();

        target.DamagedPublisher -= DamagedInstead;
    }

    public void DamagedInstead(CharacterBehavior Attacker, int value)
    {
        Debug.Log("Instead");
        buffOwner.Damaged(Attacker, value);
    }
}

public class Buff_Swear_Guest : BuffOnPlaying
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
