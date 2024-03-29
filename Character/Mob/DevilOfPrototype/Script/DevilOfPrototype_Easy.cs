using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum DOPSkills
{
    SurpriseAttack,
    GetAway,
    Rush,
}
public class DevilOfPrototype_Easy : BossBehavior
{
    private void Start()
    {
        StartBossPattern();
    }

    public void StartBossPattern()
    {
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
}
