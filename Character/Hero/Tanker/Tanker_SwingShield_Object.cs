using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tanker_SwingShield_Object : MonoBehaviour
{
    private int damage;
    private float moveSpeed;
    private CharacterBehavior attacker;

    public void SetData(CharacterBehavior _attacker, int _damage)
    {
        damage = _damage;
        attacker = _attacker;
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log(collision.name);
        if (collision.CompareTag(Utils_Tag.Mob))
        {
            collision.GetComponent<CharacterBehavior>().Damaged(attacker, damage);
        }
    }
}
