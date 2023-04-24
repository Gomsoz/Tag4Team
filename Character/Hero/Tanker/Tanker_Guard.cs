using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;

public class Tanker_Guard : HeroSkill
{
    [SerializeField]
    private GameObject skillEffect;

    private float provokeRadius = 2f;
    private int duration = 5;

    private List<CharacterBehavior> provokedMob = new List<CharacterBehavior>();
    public override void UseSkill(Action _endSkillCallback = null, Action _endCastingCallback = null)
    {
        base.UseSkill(_endSkillCallback, _endCastingCallback);

        StartCoroutine(Provoke());
    }

    private IEnumerator Provoke()
    {
        float leftTime = duration;
        GameObject go = Instantiate(skillEffect, runningPosition, Quaternion.identity);

        while (leftTime >= 0)
        {
            Collider2D[] hits = Physics2D.OverlapCircleAll(runningPosition, provokeRadius);
            
            foreach(var item in hits)
            {
                if (item.CompareTag(Utils_Tag.Mob))
                {
                    CharacterBehavior target = item.GetComponent<CharacterBehavior>();
                    if (provokedMob.Contains(target))
                        continue;

                    provokedMob.Add(target);
                    target.SetProvocativeUnit(skillOwner);
                    target.SetCharacterState(CharacterState.Provoked, true);
                }
            }

            leftTime -= Time.deltaTime;
            yield return null;
        }

        foreach (var item in provokedMob)
        {
            item.SetCharacterState(CharacterState.Provoked, false);
        }

        Destroy(go);

        provokedMob.Clear();
        EndSkill();
    }
}
