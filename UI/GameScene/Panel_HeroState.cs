using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Panel_HeroState : MonoBehaviour
{
    [SerializeField]
    private UI_HeroState[] heroStates;

    public void SetHeroImage(int heroNum, Sprite sprite)
    {
        heroStates[heroNum].SetHeroImage(sprite);
    }

    public void StartTagCooldown(int heroNum, float totalTime, Action callback)
    {
        heroStates[heroNum].StartTagCooldown(totalTime, callback);
    }

    public void SetDeathMark(int heroNum, bool isOn)
    {
        heroStates[heroNum].ToggleDeathMark(isOn);
    }
}
