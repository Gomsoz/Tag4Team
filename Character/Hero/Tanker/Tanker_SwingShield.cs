using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tanker_SwingShield : HeroSkill
{
    [SerializeField]
    private GameObject effectPrefab;
    private float radius = 1f;
    private int damageBase = 5;
    private float damageFactor = 0.1f;
    private float duration = 5f;

    [SerializeField]
    private GameObject swingShieldPrefab;

    private GameObject swingShieldInstance;

    public override void UseSkill(Action _endSkillCallback = null, Action _endCastingCallback = null)
    {
        base.UseSkill();

        swingShieldInstance = Instantiate(swingShieldPrefab, runningPosition, Quaternion.identity);
        swingShieldInstance.GetComponent<Tanker_SwingShield_Parent>().SetData(skillOwner, ConvertDamage(damageBase, damageFactor));

        StartCoroutine(SkillAction(duration));
    }

    public override void EndSkill()
    {
        base.EndSkill();

        Destroy(swingShieldInstance);
    }
}
