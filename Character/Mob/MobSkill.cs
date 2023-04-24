using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobSkill : MonoBehaviour
{
    protected float minSkillRange;
    protected float maxSkillRange;
    protected MobBehavior skillOwner;
    protected MobSkill thisSkill;
    protected Action endSkillCallback;

    protected SkillCooldownInfo skillCooldownInfo;
    public SkillCooldownInfo SkillCooldownInfo { get => skillCooldownInfo; }
    protected bool isCooldown { get => skillCooldownInfo.isCooldown; }
    protected string skillName { get => SkillCooldownInfo.skillName; }

    public virtual void SkillInit(MobBehavior owner)
    {
        skillOwner = owner;
        skillOwner.ChangeDistancePublisher += ChangeDistanceListener;
    }

    public virtual void UseSkill(Action _endSkillCallback = null)
    {
        gameObject.SetActive(true);
        endSkillCallback = _endSkillCallback;
        skillOwner.RemoveSkill(this);
        skillOwner.StartSkillCooldown(skillCooldownInfo);
    }

    public virtual void EndSkill()
    {
        if (endSkillCallback != null)
            endSkillCallback();

        gameObject.SetActive(false);
    }

    public void ChangeDistanceListener(float distance)
    {
        if (isCooldown)
            return;

        if (skillOwner.UsableSkills.Contains(thisSkill))
            return;

        if (distance >= minSkillRange && distance <= maxSkillRange)
        {
            skillOwner.AddSkill(thisSkill);
            return;
        }
        else
        {
            skillOwner.RemoveSkill(thisSkill);
        }
    }

    public IEnumerator ChargeSkill(float time, Action callback)
    {
        skillOwner.Anim.SetBool("ReadyToChargAttack", true);

        float leftTime = time;
        while (leftTime >= 0)
        {
            leftTime -= Time.deltaTime;
            yield return null;
        }

        callback();

    }

    protected IEnumerator SkillAction()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.2f);
            break;
        }

        EndSkill();
    }
}
