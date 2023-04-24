using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System;

public class UI_HeroSkill : MonoBehaviour
{
    private SkillCooldownInfo skillCooldownInfo;

    [SerializeField]
    private TMP_Text text_skillName;

    [SerializeField]
    private GameObject cooldownGroup;
    [SerializeField]
    private Image cooldownImage;
    [SerializeField]
    private TMP_Text cooldownText;

    private Action callback;

    public void SetSkillInformation(SkillCooldownInfo info)
    {
        if (skillCooldownInfo != null)
        {
            cooldownGroup.SetActive(false);
            skillCooldownInfo.LeftCooldownPublisher -= LeftCooldownListener;
        }

        skillCooldownInfo = info;

        text_skillName.text = skillCooldownInfo.skillName;

        info.LeftCooldownPublisher -= LeftCooldownListener;
        info.LeftCooldownPublisher += LeftCooldownListener;
    }

    public void LeftCooldownListener(float leftTime)
    {
        if (leftTime == -1)
        {
            cooldownGroup.SetActive(false);
            return;
        }

        cooldownGroup.SetActive(true);
        int leftTimeInteger = (int)leftTime;

        cooldownImage.fillAmount = leftTime / skillCooldownInfo.totalCooldown;
        cooldownText.text = leftTimeInteger.ToString();
    }
}
