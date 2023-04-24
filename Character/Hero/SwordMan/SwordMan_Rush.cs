using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordMan_Rush : HeroSkill
{
    [SerializeField]
    private GameObject skillEffect;

    private float rushTime = 0.2f;
    private float rushDistance = 3f;

    private int damageBase = 50;
    private float damageFactor = 0.7f;

    private float collisionHeight = 0.5f;
    private float collisionRadius = 0.5f;

    public override void UseSkill(Action _endSkillCallback = null, Action _endCastingCallback = null)
    {
        base.UseSkill(_endSkillCallback, _endCastingCallback);

        StartCoroutine(StartRush());
    }

    private IEnumerator StartRush()
    {
        Vector3 startPosition = runningPosition;
        Vector2 centerPos = Vector3.zero;

        float leftTime = rushTime;
        while (leftTime >= 0)
        {
            centerPos = PlayerController.Instance.transform.position + new Vector3(collisionHeight, 0);
            Collider2D[] hits = Physics2D.OverlapCircleAll(centerPos, collisionRadius);
            if (hits.Length != 0)
            {
                bool isHitMob = false;

                foreach (var item in hits)
                {
                    if (item.CompareTag(Utils_Tag.Hero) || item.CompareTag(Utils_Tag.Player))
                        continue;

                    CharacterBehavior target = item.GetComponent<CharacterBehavior>();
                    if (target == null)
                        continue;

                    isHitMob = true;
                    target.Damaged(target, ConvertDamage(damageBase, damageFactor));

                    Instantiate(skillEffect, target.transform.position, Quaternion.identity);
                }

                if(isHitMob)
                    break;
            }

            float movePosition = Mathf.Lerp(rushDistance, 0, leftTime / rushTime);
            PlayerController.Instance.transform.position =
                startPosition + new Vector3(movePosition * -PlayerController.Instance.FlipValue, 0);
            leftTime -= Time.deltaTime;
            yield return null;
        }

        yield return null;
    }
}
