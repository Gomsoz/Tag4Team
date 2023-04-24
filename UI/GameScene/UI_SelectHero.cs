using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_SelectHero : MonoBehaviour
{
    [SerializeField]
    private Image image_hero;

    [SerializeField]
    private GameObject deathMark;

    public void SetHeroImage(HeroBehavior hero)
    {
        hero.DeathPublisher += OnDeathMark;
        image_hero.sprite = hero.CharacterImage;
    }

    public void OnDeathMark()
    {
        deathMark.SetActive(true);
    }
}
