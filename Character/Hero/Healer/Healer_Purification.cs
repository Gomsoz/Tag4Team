using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Healer_Purification : HeroSkill
{
    [SerializeField]
    private GameObject effectPrefab;

    private float radius = 2f;
    private Vector3 centerPos;
    private float pushTime = 10f;

    private int damageBase = 10;
    private float damageFactor = 0.3f;

    private float skillDuration = 0.5f;

    public override void UseSkill(Action _endSkillCallback = null, Action _endCastingCallback = null)
    {
        base.UseSkill(_endSkillCallback, _endCastingCallback);

        Instantiate(effectPrefab, transform);

        Collider2D[] colls = Physics2D.OverlapCircleAll(runningPosition, 2f);
        centerPos = transform.position;
        Vector3 pushDirection = Vector3.zero;

        if (colls != null)
        {
            foreach (var item in colls)
            {
                if (item.CompareTag(Utils_Tag.Mob) == false)
                    continue;

                pushDirection = (item.transform.position - centerPos).normalized;
                Vector3 goalPosition = centerPos + (pushDirection * radius);

                StartCoroutine(PushOutCharacter(item.transform, goalPosition));
            }
        }

        StartCoroutine(SkillAction(skillDuration));
    }

    private IEnumerator PushOutCharacter(Transform target, Vector3 goalPosition)
    {
        Vector3 direction = (goalPosition - target.position).normalized;
        float disPerFrame = (goalPosition - target.position).magnitude * Time.deltaTime;
        float leftDis = (goalPosition - target.position).magnitude;

        while (leftDis > 0)
        {
            if ((target.position - centerPos).magnitude > radius)
            {
                target.position = goalPosition;
                break;
            }          

            target.position += direction * disPerFrame * pushTime;
            leftDis = (goalPosition - target.position).magnitude;
            yield return null;
        }

        target.GetComponent<CharacterBehavior>().Damaged(skillOwner, ConvertDamage(damageBase, damageFactor));
    }
}
