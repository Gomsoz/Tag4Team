using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UISet_PartySummaryHeroInfo : MonoBehaviour
{
    private HeroJobs curHeroJob;
    private HeroInfo curHeroData;

    [SerializeField]
    private GameObject registeredSlot;

    [SerializeField]
    private GameObject emptySlot;

    [SerializeField]
    private Image image_hero;

    [SerializeField]
    private HpBar hpBar;

    [SerializeField]
    private HpBar mpBar;

    [SerializeField]
    private HpBar fatigueBar;

    public void SetHeroData(HeroJobs job)
    {
        curHeroJob = job;

        if (job.Equals(HeroJobs.None))
        {
            emptySlot.SetActive(true);
            registeredSlot.SetActive(false);

            // 리스트에서 데이터를 비움
            hpBar.SetMaxHp(0);
            hpBar.SetCurHp(0);

            mpBar.SetMaxHp(0);
            mpBar.SetCurHp(0);
        }
        else
        {
            emptySlot.SetActive(false);
            registeredSlot.SetActive(true);

            curHeroData = HeroDataManager.Instance.GetHerodata(job);

            image_hero.sprite = curHeroData.CharacterImage;

            hpBar.SetMaxHp(curHeroData.Stat.MaxHp);
            hpBar.SetCurHp(curHeroData.Stat.Hp);

            mpBar.SetMaxHp(curHeroData.Stat.MaxMp);
            mpBar.SetCurHp(curHeroData.Stat.Mp);
        }
    }
}
