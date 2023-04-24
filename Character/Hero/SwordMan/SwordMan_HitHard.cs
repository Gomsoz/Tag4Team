using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;

public class SwordMan_HitHard : HeroSkill
{
    [SerializeField]
    private GameObject skillEffect;

    private Vector3 attackRange = new Vector3(1, 0.8f);
    private int damageBase = 10;
    private float damageFactor = 0.5f;
    private int hitCount = 3;
    private float attackDelay = 0.15f;

    public override void UseSkill(Action _endSkillCallback = null, Action _endCastingCallback = null)
    {
        base.UseSkill(_endSkillCallback, _endCastingCallback);
        
        StartCoroutine(Attack());
    }

    private IEnumerator Attack()
    {
        int leftHit = 0;
        float[] damageRatio = new float[3] {1f, 1.2f, 1.8f};

        Vector3 angle1 = runningPosition + new Vector3(0, 0);
        Vector3 angle2 = runningPosition + new Vector3(attackRange.x * skillOwner.FlipValue, attackRange.y);

        while (leftHit < hitCount)
        {
            skillOwner.Anim.SetTrigger("NormalAttack");

            Collider2D[] hits = Physics2D.OverlapAreaAll(angle1, angle2);

            foreach (var item in hits)
            {
                if (item.CompareTag(Utils_Tag.Hero) || item.CompareTag(Utils_Tag.Player))
                    continue;

                CharacterBehavior target = item.GetComponent<CharacterBehavior>();
                if (target == null)
                    continue;

                target.Damaged(target, (int)(ConvertDamage(damageBase, damageFactor) * damageRatio[leftHit]));

                Instantiate(skillEffect,target.transform.position, Quaternion.identity);
            }

            yield return new WaitForSeconds(attackDelay);
            leftHit++;
        }

        EndSkill();
    }
}
