using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public enum PartySlotState
{
    Empty,
    Registered,
}
public class UISet_SelectedHero : MonoBehaviour
{
    private PartySlotState slotState = PartySlotState.Empty;
    public PartySlotState SlotState { get { return slotState; } }

    [field: SerializeField]
    public int SlotNum { get; private set; }

    [SerializeField]
    private Button btn;

    [SerializeField]
    private Image image_hero;

    [SerializeField]
    private HpBar hpbar;

    [SerializeField]
    private HpBar mpbar;

    [SerializeField]
    private GameObject selectMark;
    private bool isSelect = false;

    // 이미지가 없기 때문에 임시로 텍스트를 넣음
    [SerializeField]
    private TMP_Text[] heroSkillImages = new TMP_Text[4];

    private HeroInfo curHeroData;
    public HeroJobs CurHeroJob { get; private set; }

    private void Start()
    {
        btn.onClick.AddListener(OnClickUI);
    }

    public void setHeroData(HeroJobs job)
    {
        if (curHeroData != null)
            curHeroData.UpdateImprovementAbilityStatPublisher -= UpdateHeroStatListener;

        CurHeroJob = job;

        if (job.Equals(HeroJobs.None))
        {
            // 리스트에서 데이터를 비움
            hpbar.SetMaxHp(0);
            hpbar.SetCurHp(0);

            mpbar.SetMaxHp(0);
            mpbar.SetCurHp(0);

            image_hero.sprite = null;

            for (int i = 0; i < 4; i++)
            {
                heroSkillImages[i].text = string.Empty;
            }

            slotState = PartySlotState.Empty;
        }
        else
        {
            slotState = PartySlotState.Registered;

            curHeroData = HeroDataManager.Instance.GetHerodata(job);
            GameManager.Instance.SetRegisteredHeroFromLobby(SlotNum, curHeroData);

            image_hero.sprite = curHeroData.CharacterImage;

            hpbar.SetMaxHp(curHeroData.Stat.MaxHp);
            hpbar.SetCurHp(curHeroData.Stat.Hp);

            mpbar.SetMaxHp(curHeroData.Stat.MaxMp);
            mpbar.SetCurHp(curHeroData.Stat.Mp);

            string[] heroSkillData = curHeroData.Herodata.SkillData;

            int idx = 0;
            foreach (var skillID in heroSkillData)
            {
                SkillData data = curHeroData.skill.GetSkillData(skillID);
                heroSkillImages[idx].text = data.Name;
                idx++;
            }

            curHeroData.UpdateImprovementAbilityStatPublisher -= UpdateHeroStatListener;
            curHeroData.UpdateImprovementAbilityStatPublisher += UpdateHeroStatListener;
        }
    }

    public void UpdateHeroStatListener()
    {
        hpbar.SetCurHp(curHeroData.Stat.Hp);
        hpbar.SetMaxHp(curHeroData.Stat.MaxHp);

        mpbar.SetCurHp(curHeroData.Stat.Mp);
        mpbar.SetMaxHp(curHeroData.Stat.MaxMp);
    }

    public void OnClickUI()
    {
        Panel_HeroList heroListUI = PartySceneUIManager.Instance.Panel_HeroList;

        if (heroListUI.CurSelectedHeroJob.Equals(HeroJobs.None))
        {
            // 이 리스트에 등록된 데이터가 없을 경우 아무런 행동을 하지 않음.
            if (CurHeroJob.Equals(HeroJobs.None))
                return;

            // 영웅 인벤토리에서 선택된 영웅이 없을 경우
            if (isSelect)
            {
                // 이 아이템이 선택 되었다면 선택을 해제한다.
                selectMark.SetActive(false);
                isSelect = false;
            }
            else
            {
                // 이 아이템이 선택된 상태가 아닐 경우
                if (heroListUI.CurSelectedHeroJobInList.Equals(HeroJobs.None))
                {
                    // 리스트에서 선택된 아이템이 없을 경우
                    // 이 아이템을 선택된 상태로 만든다.
                    selectMark.SetActive(true);
                    isSelect = true;
                    heroListUI.SetCurSelectedHeroInList(this);
                }
                else
                {
                    // 리스트에서 선택된 아이템이 있을 경우
                    // 이 아이템의 정보와 선택된 아이템의 정보를 교환한다.

                    heroListUI.ChangeListedHero(this);
                }
            }
        }
        else
        {
            if (heroListUI.ChkListedHero())
            {
                // 리스트에 이미 해당 영웅이 있는 경우
                // 해당 영웅이 있는 리스트의 데이터를 지운다.
                heroListUI.ClearOverlappedHero();
            }

            HeroJobs job = heroListUI.CurSelectedHeroJob;
            setHeroData(job);
            heroListUI.ReleaseSelectedHero();

            PartyScene.Instance.SaveSceneData();
        }
    }

    public void ReleaseSelectMark()
    {
        isSelect = false;
        selectMark.SetActive(false);
    }
}
