using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;
using TMPro;

public enum HealingServiceState
{
    None,
    Healing,
    Done,
}

public class UISet_HealingServiceItem : MonoBehaviour, IDropHandler
{
    private HealingServiceState state = HealingServiceState.None;

    private HeroInfo registeredHero;

    private DateInGame startDate;
    private int numberOfHealing = 0;
    private float healingAmount = 0.05f;

    private int startHp;
    private int hpAfterHealing;
    private int startMp;
    private int mpAfterHealing;
    private int fatigueAfterHealing;

    private bool isFullHp = false;
    private bool isFullMp = false;
    private bool isFullFatigue = false;

    #region UI

    [SerializeField]
    private GameObject emptySlot;
    [SerializeField]
    private GameObject healingSlot;
    [SerializeField]
    private int slotNum;

    [SerializeField]
    private Image heroImage;

    [SerializeField]
    private HpBar hpBar;

    [SerializeField]
    private HpBar mpBar;

    [SerializeField]
    private HpBar fatigueBar;

    [SerializeField]
    private Button btn_state;

    [SerializeField]
    private TMP_Text txt_state;

    #endregion

    public void InitSlot(Action<int> callback)
    {
        //onMouseCallback = callback;
    }

    private void Start()
    {
        btn_state.onClick.AddListener(OnClick);
    }

    public void TEST()
    {
        HealingServiceSlotData data = JsonManager.FromJson<HealingServiceSlotData>("SlotData");
        LoadSlotData(data);
    }

    public void LoadSlotData(HealingServiceSlotData data)
    {
        if (data.State.Equals(HealingServiceState.None))
        {
            emptySlot.SetActive(true);
            healingSlot.SetActive(false);
        }
        else
        {
            emptySlot.SetActive(false);
            healingSlot.SetActive(true);

            startDate = data.Date;
            state = data.State;
            numberOfHealing = data.NumOfHealing;
            registeredHero = HeroDataManager.Instance.GetHerodata(data.Job);

            HospitalScene.Instance.SuccessRegisterHero(registeredHero.HeroJob);
            SetHeroData();

            startHp = registeredHero.Stat.Hp;
            startMp = registeredHero.Stat.Mp;

            hpAfterHealing = startHp + (int)(registeredHero.Stat.MaxHp * (healingAmount * numberOfHealing));
            mpAfterHealing = startMp + (int)(registeredHero.Stat.MaxMp * (healingAmount * numberOfHealing));

            hpBar.SetCurHp(hpAfterHealing);
            mpBar.SetCurHp(mpAfterHealing);

            if (data.State.Equals(HealingServiceState.Healing))
            {
                txt_state.text = "치유중";

                GameManager.Instance.tenMinutesEvent -= Healing;
                GameManager.Instance.tenMinutesEvent += Healing;
            }
            else
            {
                txt_state.text = "치유완료";
            }
        }
    }

    [ContextMenu("TEST")]
    public void TEST2()
    {
        JsonManager.ToJson(SaveSlotData(), "SlotData");
    }

    public HealingServiceSlotData SaveSlotData()
    {
        HealingServiceSlotData newSlotData = new HealingServiceSlotData();

        if (state.Equals(HealingServiceState.None) == false)
        {
            newSlotData.Date = startDate;
            newSlotData.NumOfHealing = numberOfHealing;
            newSlotData.Job = registeredHero.HeroJob;
        }
        
        newSlotData.State = state;    

        return newSlotData;
    }

    public void OnClick()
    {
        switch (state)
        {
            case HealingServiceState.Healing:
                GameManager.Instance.OpenCommonPopup(CommonPopup.ApplyCancel, "치유를 중단합니까?", StopHealingService_ApplyCallback);
                break;
            case HealingServiceState.Done:
                GameManager.Instance.OpenCommonPopup(CommonPopup.Done, "완벽히 치유 되었습니다.", DoneHealingService);
                break;
        }
    }

    public void StopHealingService_ApplyCallback()
    {
        registeredHero.Stat.Hp = hpAfterHealing;
        registeredHero.Stat.Mp = mpAfterHealing;

        HeroDataManager.Instance.SaveHeroDatas();

        ResetData();
    }

    public void DoneHealingService()
    {
        registeredHero.Stat.Hp = hpAfterHealing;
        registeredHero.Stat.Mp = mpAfterHealing;

        HeroDataManager.Instance.SaveHeroDatas();

        ResetData();
    }

    private void ResetData()
    {
        registeredHero.Herodata.isRegistered = false;

        HospitalScene.Instance.ReleaseRegisteredHero(registeredHero.HeroJob);
        GameManager.Instance.tenMinutesEvent -= Healing;

        state = HealingServiceState.None;

        registeredHero = null;
        startDate = null;

        healingSlot.SetActive(false);
        emptySlot.SetActive(true);

        hpAfterHealing = 0;
        startHp = 0;
        mpAfterHealing = 0;
        startMp = 0;
        fatigueAfterHealing = 0;

        isFullHp = false;
        isFullMp = false;
        isFullFatigue = false;
    }

    public void OnDrop(PointerEventData eventData)
    {
        if (registeredHero != null)
            return;

        registeredHero = HospitalScene.Instance.SelectedHeroInfo;

        if (registeredHero == null)
            return;

        if(registeredHero.Stat.Hp == registeredHero.Stat.MaxHp)
        {
            if(registeredHero.Stat.Mp == registeredHero.Stat.MaxMp)
            {
                GameManager.Instance.OpenCommonPopup(CommonPopup.Done, "치유 대상이 아닙니다. ", null);
                return;
            }
        }

        registeredHero.Herodata.isRegistered = true;

        state = HealingServiceState.Healing;
        txt_state.text = "치유중";

        startHp = registeredHero.Stat.Hp;
        startMp = registeredHero.Stat.Mp;

        startDate = new DateInGame(GameManager.Instance.CurTime);
        GameManager.Instance.tenMinutesEvent -= Healing;
        GameManager.Instance.tenMinutesEvent += Healing;

        emptySlot.SetActive(false);
        HospitalScene.Instance.SuccessRegisterHero(registeredHero.HeroJob);
        SetHeroData();
        healingSlot.SetActive(true);
    }

    public void SetHeroData()
    {
        hpBar.SetMaxHp(registeredHero.Stat.MaxHp);
        hpBar.SetCurHp(registeredHero.Stat.Hp);

        mpBar.SetMaxHp(registeredHero.Stat.MaxMp);
        mpBar.SetCurHp(registeredHero.Stat.Mp);

        heroImage.sprite = registeredHero.CharacterImage;
    }

    private void Healing()
    {
        DateInGame date = startDate.ElapsedTime();

        if (numberOfHealing == date.Hour)
            return;

        numberOfHealing++;

        if(isFullHp == false)
        {
            hpAfterHealing = startHp + (int)(registeredHero.Stat.MaxHp * (healingAmount * numberOfHealing));
            if (hpAfterHealing >= registeredHero.Stat.MaxHp)
            {
                isFullHp = true;
                hpAfterHealing = registeredHero.Stat.MaxHp;
            }

            hpBar.SetCurHp(hpAfterHealing);
        }
        
        if(isFullMp == false)
        {
            mpAfterHealing = startMp + (int)(registeredHero.Stat.MaxMp * (healingAmount * numberOfHealing));
            if (mpAfterHealing >= registeredHero.Stat.MaxMp)
            {
                isFullMp = true;
                mpAfterHealing = registeredHero.Stat.MaxMp;
            }

            mpBar.SetCurHp(mpAfterHealing);
        }

        if(isFullHp && isFullMp)
        {
            state = HealingServiceState.Done;
            txt_state.text = "치유완료";

            GameManager.Instance.tenMinutesEvent -= Healing;
        }
    }
}

[Serializable]
public class HealingServiceSlotData
{
    public HealingServiceState State;
    public DateInGame Date;
    public HeroJobs Job;

    public int NumOfHealing;
}
