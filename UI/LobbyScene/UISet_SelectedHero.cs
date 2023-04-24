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

    // �̹����� ���� ������ �ӽ÷� �ؽ�Ʈ�� ����
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
            // ����Ʈ���� �����͸� ���
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
            // �� ����Ʈ�� ��ϵ� �����Ͱ� ���� ��� �ƹ��� �ൿ�� ���� ����.
            if (CurHeroJob.Equals(HeroJobs.None))
                return;

            // ���� �κ��丮���� ���õ� ������ ���� ���
            if (isSelect)
            {
                // �� �������� ���� �Ǿ��ٸ� ������ �����Ѵ�.
                selectMark.SetActive(false);
                isSelect = false;
            }
            else
            {
                // �� �������� ���õ� ���°� �ƴ� ���
                if (heroListUI.CurSelectedHeroJobInList.Equals(HeroJobs.None))
                {
                    // ����Ʈ���� ���õ� �������� ���� ���
                    // �� �������� ���õ� ���·� �����.
                    selectMark.SetActive(true);
                    isSelect = true;
                    heroListUI.SetCurSelectedHeroInList(this);
                }
                else
                {
                    // ����Ʈ���� ���õ� �������� ���� ���
                    // �� �������� ������ ���õ� �������� ������ ��ȯ�Ѵ�.

                    heroListUI.ChangeListedHero(this);
                }
            }
        }
        else
        {
            if (heroListUI.ChkListedHero())
            {
                // ����Ʈ�� �̹� �ش� ������ �ִ� ���
                // �ش� ������ �ִ� ����Ʈ�� �����͸� �����.
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
