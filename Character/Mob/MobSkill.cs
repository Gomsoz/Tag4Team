using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobSkill : MonoBehaviour
{
    protected int skillLevel;

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

        if(skillLevel == 0)
            UseLv0Skill();
        else if(skillLevel == 1)
            UseLv1Skill();
    }

    protected virtual void UseLv0Skill()
    {

    }

    protected virtual void UseLv1Skill()
    {

    }

    public void SetSkillLv(int level)
    {
        skillLevel = level;

        if (skillLevel == 0)
            SetSkillLv0();
        else if (skillLevel == 1)
            SetSkillLv1();
    }

    protected virtual void SetSkillLv0()
    {

    }

    protected virtual void SetSkillLv1()
    {

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
