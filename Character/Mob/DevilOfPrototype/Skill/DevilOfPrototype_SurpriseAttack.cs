using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DevilOfPrototype_SurpriseAttack : MobSkill
{
    private float attackDelay = 1.5f;
    private int damage = 150;

    private Vector3 attackRange = new Vector3(-4, 3f);
    public override void SkillInit(MobBehavior owner)
    {
        thisSkill = this;
        minSkillRange = 3;
        maxSkillRange = 10;

        skillCooldownInfo = new()
        {
            skillName = "SurpriseAttack",
            totalCooldown = 20,
        };

        base.SkillInit(owner);
    }

    public override void UseSkill(Action _endSkillCallback = null)
    {
        base.UseSkill(_endSkillCallback);

        PlayerController target = PlayerController.Instance;

        float gap = 1.5f;
        Vector3 appearanceLocation = target.transform.position + new Vector3(gap * target.FlipValue, 0, 0);

        skillOwner.transform.position = appearanceLocation;

        StartCoroutine(ChargeSkill(3, Charge));
    }

    public void Charge()
    {
        skillOwner.Anim.SetBool("ReadyToChargAttack", false);
        skillOwner.Anim.SetTrigger("ChargeAttack");
        Vector3 angle1 = transform.position + new Vector3(0, 0);
        Vector3 angle2 = transform.position + new Vector3(attackRange.x * skillOwner.FlipValue, attackRange.y);
        Collider2D[] hits = Physics2D.OverlapAreaAll(angle1, angle2);

        foreach (var item in hits)
        {
            if (item.CompareTag(Utils_Tag.Mob))
                continue;

            CharacterBehavior target = item.GetComponent<HeroBehavior>();
            target?.Damaged(skillOwner, damage);
        }

        StartCoroutine(AttackDelay());   
    }

    private IEnumerator AttackDelay()
    {
        while (true)
        {
            yield return new WaitForSeconds(1.5f);
            break;
        }

        EndSkill();
    }
}
