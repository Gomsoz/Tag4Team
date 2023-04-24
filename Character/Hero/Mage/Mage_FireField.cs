using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mage_FireField : HeroSkill
{
    [SerializeField]
    private GameObject effectPrefab;

    [SerializeField]
    private GameObject fireField;

    private float fieldRadius = 1.5f;
    private int ticCount = 5;
    private float ticDuration = 1;
    private int damageBase = 30;
    private float damageFactor = 0.5f;


    public override void EndCasting()
    {
        base.EndCasting();

        GameObject go = Instantiate(fireField, runningPosition, Quaternion.identity);
        Mage_FireField_Object field = go.GetComponent<Mage_FireField_Object>();
        field.Init(ticDuration, ticCount, fieldRadius, TicDamage);
        field.StartLanding();

        EndSkill();
    }

    public void TicDamage(CharacterBehavior target)
    {
        target.Damaged(skillOwner, ConvertDamage(damageBase, damageFactor));
        Instantiate(effectPrefab, target.transform);
    }
}
