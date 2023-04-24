using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Panel_HeroInfo_Control : MonoBehaviour
{
    [SerializeField]
    private Image heroImage;

    [SerializeField]
    private HpBar hpbar;

    [SerializeField]
    private HpBar mpbar;

    [SerializeField]
    private TMP_Text txt_dmg;

    [SerializeField]
    private TMP_Text txt_amor;

    private HeroBehavior targetHero;

    public void SetCharacterData(HeroBehavior _hero)
    {
        targetHero = _hero;
        // _hero.AdjustImprovementAbilityPublisher += ModifyHeroInfoUI;
        targetHero.DamagedPublisher += DamagedListener;
        targetHero.HealingPublisher += HealingListener;

        ModifyHeroInfoUI();

        heroImage.sprite = targetHero.CharacterImage;
    }

    public void ModifyHeroInfoUI()
    {
        hpbar.SetMaxHp(targetHero.MaxHp);
        hpbar.SetCurHp(targetHero.Hp);

        mpbar.SetMaxHp(targetHero.MaxMp);
        mpbar.SetCurHp(targetHero.Mp);

        txt_dmg.text = targetHero.Damage.ToString();
        txt_amor.text = targetHero.Defense.ToString();
    }

    public void DamagedListener(CharacterBehavior attacker , int value)
    {
        hpbar.SetCurHp(targetHero.Hp);
    }

    public void HealingListener(int value)
    {
        hpbar.SetCurHp(targetHero.Hp);
    }
}
