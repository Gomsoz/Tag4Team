using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mage_FireBall : HeroSkill
{
    [SerializeField]
    private GameObject fireBall;
    private float fireBallDistance = 5;
    private int damageBase = 50;
    private float damageFactor = 0.8f;
    private float fireBallSpeed = 1f;
    public override void EndCasting()
    {
        base.EndCasting();

        GameObject go = Instantiate(fireBall, runningPosition, Quaternion.identity);
        Vector3 goalPosition = go.transform.position + new Vector3(-skillOwner.transform.localScale.x * fireBallDistance, 0, 0);

        go.GetComponent<Mage_FireBall_FireBall>().Init(goalPosition, fireBallSpeed, InflictDamage);

        EndSkill();
    }

    public void InflictDamage(CharacterBehavior target)
    {
        target?.Damaged(skillOwner, ConvertDamage(damageBase, damageFactor));   
    }
}
