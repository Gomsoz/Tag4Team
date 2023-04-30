using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DevilOfPrototype_Rush : MobSkill
{
    [SerializeField]
    private SkillRangeController rangeController;

    private Coroutine tracePlayerCoroutine;
    private int charagingTime = 2;
    private int skillDamage = 300;
    private Vector3 finalDirection;

    private void Update()
    {
        //finalDirection = test.position - transform.position;

        //float degree = Mathf.Atan2(finalDirection.x, finalDirection.y) * Mathf.Rad2Deg;
        //transform.rotation = Quaternion.Euler(new Vector3(0, 0, -degree));
    }

    public override void SkillInit(MobBehavior owner)
    {
        thisSkill = this;
        minSkillRange = 5;
        maxSkillRange = 12;

        skillCooldownInfo = new()
        {
            skillName = "Rush",
            totalCooldown = 15,
        };

        base.SkillInit(owner);
    }

    public override void UseSkill(Action _endSkillCallback = null)
    {
        base.UseSkill(_endSkillCallback);

        tracePlayerCoroutine = StartCoroutine(TracePlayer());
        rangeController.StartFill(charagingTime, FullSkillRange);
    }

    private IEnumerator TracePlayer()
    {
        while (true)
        {
            finalDirection = PlayerController.Instance.transform.position - transform.position;

            float degree = Mathf.Atan2(finalDirection.x, finalDirection.y) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(new Vector3(0, 0, -degree));
            yield return null;
        }
    }


    public void FullSkillRange()
    {
        StopCoroutine(tracePlayerCoroutine);

        Vector3 pointA = transform.position + (transform.right * -1);
        Vector3 pointB = transform.position + (finalDirection.normalized * 8) + (transform.right.normalized * 1);

        Collider2D[] colls = Physics2D.OverlapAreaAll(pointA, pointB);

        if (colls != null)
        {
            foreach (var item in colls)
            {
                if (item.CompareTag(Utils_Tag.Player) == false && item.CompareTag(Utils_Tag.Hero) == false)
                    continue;

                item.GetComponent<CharacterBehavior>().Damaged(skillOwner, skillDamage);
                PlayerController.Instance.UpdateControllerPosition();
            }
        }

        StartCoroutine(SkillAction());

        skillOwner.transform.position += finalDirection.normalized * 8;
    }
}
