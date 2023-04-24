using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Healer_WideAreaHealing : HeroSkill
{
    [SerializeField]
    private GameObject effect;
    private int healingAmount = 10;
    private float healingFactor = 1f;

    private List<HeroBehavior> targetHeroes = new List<HeroBehavior>();

    private int skillTime = 7;


    public override void UseSkill(Action _endSkillCallback = null, Action _endCastingCallback = null)
    {
        base.UseSkill(_endSkillCallback);

        StartCoroutine(SkillAction());
    }

    private IEnumerator SkillAction()
    {
        float elapsedTime = 0;
        while (elapsedTime < skillTime)
        {
            effect.SetActive(true);
            yield return new WaitForSeconds(1);

            if(targetHeroes.Count > 0)
            {
                foreach (var item in targetHeroes)
                {
                    item.Healing(skillOwner, ConvertDamage(healingAmount, healingFactor));
                }
            }

            elapsedTime += 1;
        }

        EndSkill();
    }

    public override void EndSkill()
    {
        base.EndSkill();
        StopAllCoroutines();

        targetHeroes.Clear();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(Utils_Tag.Player))
        {
            targetHeroes.Add(collision.GetComponent<HeroBehavior>());
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag(Utils_Tag.Player))
        {
            targetHeroes.Remove(collision.GetComponent<HeroBehavior>());
        }
    }
}
