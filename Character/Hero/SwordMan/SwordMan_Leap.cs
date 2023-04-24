using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordMan_Leap : HeroSkill
{
    [SerializeField]
    private GameObject skillEffect;

    private float risingTime = 0.3f;
    private float risingDistance = 10f;
    private float fallingTime = 0.3f;

    private float collisionRadius = 1f;
    private int damageBase = 150;
    private float damageFactor = 1f;

    public override void UseSkill(Action _endSkillCallback = null, Action _endCastingCallback = null)
    {
        base.UseSkill(_endSkillCallback, _endCastingCallback);

        StartCoroutine(StartLeap());
    }

    private IEnumerator StartLeap()
    {
        Vector3 startPosition = PlayerController.Instance.transform.position;

        float leftTime = risingTime;
        while (leftTime >= 0)
        {
            float movePosition = Mathf.Lerp(risingDistance, 0, leftTime / risingTime);
            PlayerController.Instance.transform.position =
                startPosition + new Vector3(0, movePosition);
            leftTime -= Time.deltaTime;
            yield return null;
        }

        startPosition = LocatedlPosition + new Vector3(0, risingDistance);
        leftTime = fallingTime;
        while (leftTime >= 0)
        {
            float movePosition = Mathf.Lerp(risingDistance, 0, leftTime / risingTime);
            PlayerController.Instance.transform.position =
                startPosition - new Vector3(0, movePosition);
            leftTime -= Time.deltaTime;
            yield return null;
        }

        Instantiate(skillEffect, runningPosition, Quaternion.identity);

        Collider2D[] hits = Physics2D.OverlapCircleAll(runningPosition, collisionRadius);
        if (hits.Length != 0)
        {
            foreach (var item in hits)
            {
                if (item.CompareTag(Utils_Tag.Hero) || item.CompareTag(Utils_Tag.Player))
                    continue;

                CharacterBehavior target = item.GetComponent<CharacterBehavior>();
                if (target == null)
                    continue;

                target.Damaged(target, ConvertDamage(damageBase, damageFactor));
            }
            yield return null;
        }

        EndSkill();
    }
}
