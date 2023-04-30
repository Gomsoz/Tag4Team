using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class UI_HeroState : MonoBehaviour
{
    #region UI

    [SerializeField]
    private GameObject cooldownGroup;
    [SerializeField]
    private Image cooldownImage;
    [SerializeField]
    private TMP_Text cooldownText;

    [SerializeField]
    private Image img_hero;

    #endregion

    [SerializeField]
    private GameObject deathMark;

    private float totalTime;
    private Action callback;

    public void SetHeroImage(Sprite sprite)
    {
        img_hero.sprite = sprite;
    }

    public void StartTagCooldown(float _totalTime, Action _callback)
    {
        if (totalTime > 0)
            return;

        cooldownGroup.SetActive(true);
        totalTime = _totalTime;
        callback = _callback;
        StartCoroutine(TagDelayTime());
    }

    public void SetCooldownData(float leftTime)
    {
        int leftTimeInteger = (int)leftTime;

        cooldownImage.fillAmount = leftTime / totalTime;
        cooldownText.text = leftTimeInteger.ToString();
    }

    private IEnumerator TagDelayTime()
    {
        float delayTime = totalTime;
        while (delayTime >= 0)
        {
            delayTime -= Time.deltaTime;
            SetCooldownData(delayTime);
            yield return null;
        }

        callback();
        totalTime = 0;
        callback = null;
        cooldownGroup.SetActive(false);
    }

    public void ToggleDeathMark(bool isOn)
    {
        deathMark.SetActive(isOn);
    }
}
