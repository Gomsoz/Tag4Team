using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Panel_MobState : MonoBehaviour
{
    [SerializeField]
    private HpBar hpbar;

    [SerializeField]
    private Image mobImage;

    private MobBehavior curMobInformation;

    public void SetMob(MobBehavior mob)
    {
        mobImage.sprite = mob.CharacterImage;
        UpdateMobHp(mob);
    }

    public void UpdateMobHp(MobBehavior mob)
    {
        hpbar.SetMaxHp(mob.MaxHp);
        hpbar.SetCurHp(mob.Hp);
    }
}
