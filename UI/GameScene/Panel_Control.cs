using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Panel_Control : MonoBehaviour
{
    [SerializeField]
    private Panel_HeroSkill heroSkill;

    [SerializeField]
    private Panel_HeroInfo_Control heroInfo;

    public void SetSkillInformation(SkillCooldownInfo info, int skillNum)
    {
        heroSkill.SetSkillInformation(info, skillNum);
    }

    public void SetHeroBehavior(HeroBehavior hero)
    {
        heroInfo.SetCharacterData(hero);
    }
}
