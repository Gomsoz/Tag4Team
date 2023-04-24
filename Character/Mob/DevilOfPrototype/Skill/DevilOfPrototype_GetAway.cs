using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DevilOfPrototype_GetAway : MobSkill
{
    [SerializeField]
    private SkillRangeController rangeController;

    private Vector3 centerPos;
    private float radius = 3f;
    private float pushTime = 0.1f;
    private int skillDamage = 200;

    public override void SkillInit(MobBehavior owner)
    {
        thisSkill = this;
        minSkillRange = 0;
        maxSkillRange = 2;

        skillCooldownInfo = new()
        {
            skillName = "GetAway",
            totalCooldown = 20,
        };

        base.SkillInit(owner);
    }

    public override void UseSkill(Action _endSkillCallback = null)
    {
        base.UseSkill(_endSkillCallback);

        rangeController.StartFill(3, FullSkillRange);     
    }

    public void FullSkillRange()
    {
        Collider2D[] colls = Physics2D.OverlapCircleAll(skillOwner.transform.position, radius);
        centerPos = skillOwner.transform.position;
        Vector3 pushDirection = Vector3.zero;

        if (colls != null)
        {
            foreach (var item in colls)
            {
                if (item.CompareTag(Utils_Tag.Player) == false && item.CompareTag(Utils_Tag.Hero) == false)
                    continue;

                pushDirection = (item.transform.position - centerPos).normalized;
                Vector3 goalPosition = centerPos + (pushDirection * radius);

                StartCoroutine(PushOutCharacter(item.transform, goalPosition));
            }
        }

        StartCoroutine(SkillAction());
    }

    private IEnumerator PushOutCharacter(Transform target, Vector3 goalPosition)
    {
        float leftTime = pushTime;
        Vector3 startPos = target.transform.position;
        while (leftTime >= 0)
        {
            target.transform.position = Vector3.Lerp(goalPosition, startPos, leftTime / pushTime);
            leftTime -= Time.deltaTime;
            yield return null;
        }

        target.GetComponent<CharacterBehavior>().Damaged(skillOwner, skillDamage);
        PlayerController.Instance.UpdateControllerPosition();
    }
}
