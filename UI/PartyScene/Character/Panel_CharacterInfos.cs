using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Panel_CharacterInfos : MonoBehaviour
{
    // 캐릭터 변경

    private HeroJobs selectedHero;
    public HeroInfo SelectedHeroData { get; private set; }

    #region UI Variable

    [SerializeField]
    private Button btn_changeHero;
    [SerializeField]
    private TMP_Text txt_curHp;
    [SerializeField]
    private TMP_Text txt_maxHp;
    [SerializeField]
    private TMP_Text txt_curMp;
    [SerializeField]
    private TMP_Text txt_maxMp;
    [SerializeField]
    private TMP_Text txt_dmg;
    [SerializeField]
    private TMP_Text txt_amor;

    // 이미지가 없기 때문에 임시로 텍스트를 넣음
    [SerializeField]
    private Image[] heroSkillImages = new Image[4];

    [SerializeField]
    private Image img_heroImage;

    #endregion

    private void Start()
    {
        btn_changeHero.onClick.AddListener(OpenSelectCharacterPanel);
        SetHeroData(HeroJobs.Tanker);
    }

    //private void OnDisable()
    //{
    //    if (SelectedHeroData == null)
    //        return;

    //    SelectedHeroData.UpdateImprovementAbilityStatPublisher -= UpdateHeroStatUI;
    //}

    public void SetHeroData(HeroJobs job)
    {
        ResetHeroData();

        selectedHero = job;

        SelectedHeroData = HeroDataManager.Instance.GetHerodata(selectedHero);
        PartySceneUIManager.Instance.Panel_FeatureSetting.SetFeatureSettingEachHero(SelectedHeroData);

        img_heroImage.sprite = SelectedHeroData.CharacterImage;

        string[] heroSkillData = SelectedHeroData.Herodata.SkillData;

        int idx = 0;
        foreach (var skillID in heroSkillData)
        {
            SkillData data = SelectedHeroData.skill.GetSkillData(skillID);
            heroSkillImages[idx].sprite = SelectedHeroData.skill.GetSkillImage(skillID);
            idx++;
        }

        SelectedHeroData.UpdateImprovementAbilityStatPublisher -= UpdateHeroStatUI;
        SelectedHeroData.UpdateImprovementAbilityStatPublisher += UpdateHeroStatUI;

        UpdateHeroStatUI();
        SetSettingData();
    }

    private void ResetHeroData()
    {
        if (SelectedHeroData == null)
            return;

        SelectedHeroData.UpdateImprovementAbilityStatPublisher -= UpdateHeroStatUI;
    }

    private void UpdateHeroStatUI()
    {
        txt_curHp.text = SelectedHeroData.Stat.Hp.ToString();
        txt_maxHp.text = SelectedHeroData.Stat.MaxHp.ToString();
        txt_curMp.text = SelectedHeroData.Stat.Mp.ToString();
        txt_maxMp.text = SelectedHeroData.Stat.MaxMp.ToString();
        txt_dmg.text = SelectedHeroData.Stat.Dmg.ToString();
        txt_amor.text = SelectedHeroData.Stat.Def.ToString();
    }

    private void SetSettingData()
    {

    }

    public void OpenSelectCharacterPanel()
    {
        PartySceneUIManager.Instance.Panel_SelectCharacter.gameObject.SetActive(true);
    }
}
