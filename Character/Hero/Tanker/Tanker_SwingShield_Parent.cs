using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tanker_SwingShield_Parent : MonoBehaviour
{
    [SerializeField]
    private Tanker_SwingShield_Object leftSwingShield;

    [SerializeField]
    private Tanker_SwingShield_Object rightSwingShield;

    public void Update()
    {
        transform.position = PlayerController.Instance.transform.position;
    }

    public void SetData(CharacterBehavior _attacker, int _damage)
    {
        leftSwingShield.SetData(_attacker, _damage);
        rightSwingShield.SetData(_attacker, _damage);
    }
}
