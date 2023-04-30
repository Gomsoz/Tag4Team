using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PartyScene : BaseScene
{
    private static PartyScene instance;
    public static PartyScene Instance { get { return instance; } }

    #region

    [SerializeField]
    private GameObject pnl_viewMenu;

    [field: SerializeField]
    public Panel_HeroList Panel_HeroList { get; private set; }

    [field: SerializeField]
    public Panel_PartyBuff Panel_PartyBuff { get; private set; }

    [field: SerializeField]
    public GameObject Panel_Character { get; private set; }

    #endregion

    private void Awake()
    {
        if (Instance == null)
        {
            instance = this;
        }
    }

    protected override void InitScene()
    {
        base.InitScene();

        SceneName = "PartyScene";
    }

    public override void LoadSceneData()
    {
        PartySceneData partySceneData = JsonManager.FromJson<PartySceneData>("PartyDatas");

        if (partySceneData == null)
        {
            partySceneData = SaveDefaultSceneData();
        }

        Panel_HeroList.LoadPartySlotData(partySceneData.SlotDatas.ToList<PartySlotData>());
        Panel_PartyBuff.LoadPartyBuffData(partySceneData.PartyBuff);
        base.LoadSceneData();
    }

    private PartySceneData SaveDefaultSceneData()
    {
        PartySceneData partySceneData = new PartySceneData();

        PartyBuffData data = new()
        {
            Point = 10,
            DefaultPoint = 10,
            HpLevel = 0,
            HpPerLevel = 0.01f,
            MpLevel = 0,
            MpPerLevel = 0.01f,
            DmgLevel = 0,
            DmgPerLevel = 0.05f,
            AmorLevel = 0,
            AmorPerLevel = 0.05f,
        };

        partySceneData.SlotDatas = Panel_HeroList.SavePartySlotData().ToArray();
        partySceneData.PartyBuff = data;

        JsonManager.ToJson(partySceneData, "PartyDatas");
        return partySceneData;
    }

    public override void SaveSceneData()
    {
        base.SaveSceneData();

        PartySceneData partySceneData = new PartySceneData();

        partySceneData.SlotDatas = Panel_HeroList.SavePartySlotData().ToArray();
        partySceneData.PartyBuff = Panel_PartyBuff.PartyBuffData;

        JsonManager.ToJson(partySceneData, "PartyDatas");
    }

    public void CloseAllPanel()
    {
        pnl_viewMenu.SetActive(false);
        Panel_PartyBuff.gameObject.SetActive(false);
        Panel_HeroList.gameObject.SetActive(false);
        Panel_Character.gameObject.SetActive(false);
    }

    public void OpenPartyBuffPanel()
    {
        CloseAllPanel();
        pnl_viewMenu.SetActive(true);
        Panel_PartyBuff.gameObject.SetActive(true);
    }

    public void OpenHeroListPanel()
    {
        CloseAllPanel();
        pnl_viewMenu.SetActive(true);
        Panel_HeroList.gameObject.SetActive(true);
    }

    public void OpenCharacterPanel()
    {
        CloseAllPanel();
        pnl_viewMenu.SetActive(true);
        Panel_Character.gameObject.SetActive(true);
    }
}

[Serializable]
public class PartySceneData
{
    public PartySlotData[] SlotDatas;
    public PartyBuffData PartyBuff;
}
