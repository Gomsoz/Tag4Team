using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAnimationEvent : MonoBehaviour
{
    public Action callback_death;
    public Action callback_normalAttack;
    public Action callback_dodge;

    public void EndDeathAnim()
    {
        callback_death();
    }

    public void EndNormalAttackAnim()
    {
        callback_normalAttack();
    }

    public void EndDodgekAnim()
    {
        callback_dodge();
    }
}
