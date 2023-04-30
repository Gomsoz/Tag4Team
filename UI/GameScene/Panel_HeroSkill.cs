using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Panel_HeroSkill : MonoBehaviour
{
    [SerializeField]
    private UI_HeroSkill[] heroSkills;

    private const int maxSkill = 4;

    public void SetSkillInformation(HeroSkill info, int skillNum)
    {
        heroSkills[skillNum].SetSkillInformation(info);
    }
}
