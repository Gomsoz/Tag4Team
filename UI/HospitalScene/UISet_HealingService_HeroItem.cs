using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class UISet_HealingService_HeroItem : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    private HeroInfo thisHeroInfo;

    [SerializeField]
    private GameObject normalSlot;
    [SerializeField]
    private GameObject registeredSlot;

    [SerializeField]
    private Image image_hero;

    [SerializeField]
    private HpBar hpbar;

    [SerializeField]
    private HpBar mpbar;

    [SerializeField]
    private HpBar fatigueBar;

    private bool isRegistered = false;

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (isRegistered)
            return;

        HospitalScene.Instance.StartDragAtHeroList(thisHeroInfo);
    }

    public void OnDrag(PointerEventData eventData)
    {
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        HospitalScene.Instance.EndDragAtHeroList();
    }

    public void SetHeroData(HeroInfo info)
    {
        thisHeroInfo = info;

        image_hero.sprite = thisHeroInfo.CharacterImage;

        hpbar.SetMaxHp(thisHeroInfo.Stat.MaxHp);
        hpbar.SetCurHp(thisHeroInfo.Stat.Hp);

        mpbar.SetMaxHp(thisHeroInfo.Stat.MaxMp);
        mpbar.SetCurHp(thisHeroInfo.Stat.Mp);
    }

    public void Register()
    {
        isRegistered = true;
        registeredSlot.SetActive(true);
    }

    public void Release()
    {
        isRegistered = false;
        registeredSlot.SetActive(false);

        hpbar.SetMaxHp(thisHeroInfo.Stat.MaxHp);
        hpbar.SetCurHp(thisHeroInfo.Stat.Hp);

        mpbar.SetMaxHp(thisHeroInfo.Stat.MaxMp);
        mpbar.SetCurHp(thisHeroInfo.Stat.Mp);
    }
}
