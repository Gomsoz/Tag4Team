using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DevilOfPrototype_Normal : BossBehavior
{
    protected override void InitBoss()
    {
        base.InitBoss();

        StartCoroutine(UpdateBehavior());
    }

    private IEnumerator UpdateBehavior()
    {
        while (true)
        {
            if (behavior.IsProvked)
            {
                behavior.StartProvokedMove();
            }
            else
            {
                switch (behavior.State)
                {
                    case MobState.Normal:
                        if (behavior.ReadyToUseSkill)
                        {
                            if (behavior.IsRestTime == false)
                            {
                                behavior.UseSkill();
                            }
                        }
                        else
                        {
                            behavior.StartMove();

                            if (behavior.IsAttackable)
                            {
                                behavior.NomalAttack();
                            }
                        }
                        break;
                    case MobState.Stop:
                        break;
                }
            }

            yield return new WaitForSeconds(behavior.NextBehaviorTerm);
        }
    }

    public override void UpdateBossLevel(int level)
    {
        base.UpdateBossLevel(level);

        mobSkills[(int)DOPSkills.SurpriseAttack].SetSkillLv(level);
        mobSkills[(int)DOPSkills.GetAway].SetSkillLv(level);
        mobSkills[(int)DOPSkills.Rush].SetSkillLv(level);
    }
}
