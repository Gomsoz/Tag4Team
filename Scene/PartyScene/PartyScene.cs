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

        PartySceneUIManager.Instance.Panel_HeroList.LoadPartySlotData(partySceneData.SlotDatas.ToList<PartySlotData>());
        PartySceneUIManager.Instance.Panel_PartyBuff.LoadPartyBuffData(partySceneData.PartyBuff);
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

        partySceneData.SlotDatas = PartySceneUIManager.Instance.Panel_HeroList.SavePartySlotData().ToArray();
        partySceneData.PartyBuff = data;

        JsonManager.ToJson(partySceneData, "PartyDatas");
        return partySceneData;
    }

    public override void SaveSceneData()
    {
        base.SaveSceneData();

        PartySceneData partySceneData = new PartySceneData();

        partySceneData.SlotDatas = PartySceneUIManager.Instance.Panel_HeroList.SavePartySlotData().ToArray();
        partySceneData.PartyBuff = PartySceneUIManager.Instance.Panel_PartyBuff.PartyBuffData;

        JsonManager.ToJson(partySceneData, "PartyDatas");
    }
}

[Serializable]
public class PartySceneData
{
    public PartySlotData[] SlotDatas;
    public PartyBuffData PartyBuff;
}
