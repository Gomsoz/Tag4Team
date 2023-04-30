using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class HospitalScene : BaseScene
{
    private static HospitalScene instance;
    public static HospitalScene Instance { get { return instance; } }

    private HeroInfo selectedHeroInfo;
    public HeroInfo SelectedHeroInfo { get => selectedHeroInfo; }

    [SerializeField]
    private GameObject DragObject;
    [SerializeField]
    private Image DragHeroImage;

    [field : SerializeField]
    public Panel_Hospital_HeroList Panel_Hospital_HeroList { get; private set; }

    [field : SerializeField]
    public Panel_HealingService Panel_HealingService { get; private set; }

    [SerializeField]
    private Toggle tgl_openHealingPnl;

    [field: SerializeField]
    public GameObject Pnl_Healing { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            instance = this;
        }
    }

    protected override void InitScene()
    {
        for (int i = 1; i < (int)HeroJobs.Cnt; i++)
        {
            HeroInfo target = HeroDataManager.Instance.GetHerodata((HeroJobs)i);
            Panel_Hospital_HeroList.CreateHeroList(target);
        }

        SceneName = "HospitalScene";
        fixedPreScene = "LobbyScene";

        tgl_openHealingPnl.onValueChanged.AddListener(ToggleHealingPanel);

        base.InitScene();
    }

    public void StartDragAtHeroList(HeroInfo _info)
    {
        selectedHeroInfo = _info;

        DragHeroImage.sprite = selectedHeroInfo.CharacterImage;

        DragObject.transform.position = Input.mousePosition;

        DragObject.SetActive(true);
    }

    public void EndDragAtHeroList()
    {
        selectedHeroInfo = null;

        DragObject.SetActive(false);
        DragHeroImage.sprite = null;
    }

    public void SuccessRegisterHero(HeroJobs job)
    {
        Panel_Hospital_HeroList.Register((int)job - 1);
    }

    public void ReleaseRegisteredHero(HeroJobs job)
    {
        Panel_Hospital_HeroList.Release((int)job - 1);
    }

    public override void LoadSceneData()
    {
        base.LoadSceneData();

        HospitalData hospitalData = JsonManager.FromJson<HospitalData>("HospitalDatas");

        if (hospitalData == null)
            return;

        Panel_HealingService.LoadSlotData(hospitalData.SlotDatas.ToList<HealingServiceSlotData>());
    }

    public override void SaveSceneData()
    {
        base.SaveSceneData();
 
        HospitalData hospitalData = new HospitalData();

        hospitalData.SlotDatas = Panel_HealingService.SaveSlotData().ToArray();

        JsonManager.ToJson(hospitalData, "HospitalDatas");
    }

    public void ToggleHealingPanel(bool isOn)
    {
        Debug.Log(isOn);
        Pnl_Healing.gameObject.SetActive(isOn);
    }
}

[Serializable]
public class HospitalData
{
    public HealingServiceSlotData[] SlotDatas;
}
