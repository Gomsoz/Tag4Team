using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class Panel_HeroList : MonoBehaviour
{
    public HeroJobs CurSelectedHeroJob
    {
        get => unselectedGroup.SelectedHero;
    }

    public HeroJobs CurSelectedHeroJobInList
    {
        get;
        private set;
    }
    private UISet_SelectedHero selectedHeroUI;

    private HeroJobs[] selectedHeroJobs = new HeroJobs[4];
    private UISet_SelectedHero overlappedHeroInList;

    [SerializeField]
    private Panel_HeroList_Selected heroList_Selected;

    [SerializeField]
    private Group_HeroInventory unselectedGroup;

    [SerializeField]
    private UISet_SelectedHero[] ListedHeroes = new UISet_SelectedHero[4];

    public List<PartySlotData> SavePartySlotData()
    {
        List<PartySlotData> partySlotDatas = new List<PartySlotData>();

        PartySlotData data;
        foreach(var item in ListedHeroes)
        {
            data = new PartySlotData();
            data.state = item.SlotState;
            data.job = item.CurHeroJob;

            partySlotDatas.Add(data);
        }

        return partySlotDatas;
    }

    public void LoadPartySlotData(List<PartySlotData> datas)
    {
        for(int i = 0; i < 4; i++)
        {
            ListedHeroes[i].setHeroData(datas[i].job);
        }
    }

    public bool ChkListedHero()
    {
        return ChkListedHero(CurSelectedHeroJob);
    }
    public bool ChkListedHero(HeroJobs job)
    {
        for(int i = 0; i < 4; i++)
        {
            if (job.Equals(ListedHeroes[i].CurHeroJob))
            {
                overlappedHeroInList = ListedHeroes[i];
                return true;
            }
        }

        return false;
    }

    public void ClearOverlappedHero()
    {
        overlappedHeroInList.setHeroData(HeroJobs.None);
        PartyScene.Instance.SaveSceneData();
    }

    public void ReleaseSelectedHero()
    {
        unselectedGroup.ReleaseSelectedHero();
    }

    public void SetCurSelectedHeroInList(UISet_SelectedHero _selectedHeroUI)
    {     
        selectedHeroUI= _selectedHeroUI;
        CurSelectedHeroJobInList = _selectedHeroUI.CurHeroJob;
    }

    public void ChangeListedHero(UISet_SelectedHero targetHeroUI)
    {
        selectedHeroUI.ReleaseSelectMark();
        targetHeroUI.ReleaseSelectMark();
        selectedHeroUI.setHeroData(targetHeroUI.CurHeroJob);
        targetHeroUI.setHeroData(CurSelectedHeroJobInList);

        CurSelectedHeroJobInList = HeroJobs.None;
        selectedHeroUI = null;

        PartyScene.Instance.SaveSceneData();
    }
}

[Serializable]
public class PartySlotData
{
    public PartySlotState state;
    public HeroJobs job;
}
