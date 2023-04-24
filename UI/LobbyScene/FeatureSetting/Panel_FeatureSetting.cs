using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Panel_FeatureSetting : MonoBehaviour
{
    private int point = 10;
    public int FeaturePoint { get => point; }

    #region UI Variable
    [SerializeField]
    private RectTransform rct_itemGroup;
    [SerializeField]
    private RectTransform rct_content;
    [SerializeField]
    private Button btn_apply;
    [SerializeField]
    private Button btn_reset;
    [SerializeField]
    private TMP_Text txt_point;
    [SerializeField]
    private Transform itemParent;

    private Vector2 additionalItemSize = new Vector2(0, 50);
    private Vector2 DescriptionItemSize = new Vector2(0, 160);
    #endregion

    public Action ResetPublisher;
    public Action ApplyPublisher;

    [SerializeField]
    private GameObject pfb_featureSettingItem;

    private List<UISet_FeatureSettingListItem> uisetList = new();
    private HeroInfo selectedHeroData;

    private void Start()
    {
        Initiate();
    }

    private void Initiate()
    {
        btn_apply.onClick.AddListener(ApplySetting);
        btn_reset.onClick.AddListener(ResetSetting);

        Feature_Base feature = new Feature_OverflowingHp();
        feature?.SetData();
        CreateFeatureSettingItem(feature);
        feature = new Feature_OverflowingMp();
        feature?.SetData();
        CreateFeatureSettingItem(feature);
        feature = new Feature_OverwhelmingHp();
        feature?.SetData();
        CreateFeatureSettingItem(feature);
        feature = new Feature_OverwhelmingMp();
        feature?.SetData();
        CreateFeatureSettingItem(feature);
    }

    public void CreateFeatureSettingItem(Feature_Base featureData)
    {
        GameObject go = Instantiate(pfb_featureSettingItem, itemParent);
        UISet_FeatureSettingListItem component = go.GetComponent<UISet_FeatureSettingListItem>();
        component.Initiate(featureData, Resizing, SelectFeature);
        uisetList.Add(component);
        rct_itemGroup.sizeDelta += additionalItemSize;
        rct_content.sizeDelta += additionalItemSize;
    }

    public void SetFeatureSettingEachHero(HeroInfo data)
    {
        selectedHeroData = data;

        List<Feature_Base> targetFeatures = selectedHeroData.Feature;

        // 체크 해제
        point = 10;
        foreach (var feature in uisetList)
        {
            feature.SetChk(false);
        }

        // 현재 체크되어 있는 특성 초기화
        foreach (var feature in uisetList)
        {
            if (targetFeatures.Count == 0)
                continue;

            foreach (var targetFeature in targetFeatures)
            {
                if (targetFeature.Name.Equals(feature.FeatureName))
                {
                    feature.SetChk(true);
                    point -= targetFeature.Point;
                    // targetFeatures.Remove(targetFeature);
                }
            }
        }

        txt_point.text = point.ToString();
    }

    public void Resizing(bool isOn)
    {
        if (isOn)
        {
            rct_itemGroup.sizeDelta += DescriptionItemSize;
            rct_content.sizeDelta += DescriptionItemSize;
        }
        else
        {
            rct_itemGroup.sizeDelta -= DescriptionItemSize;
            rct_content.sizeDelta -= DescriptionItemSize;
        }
    }

    public void SelectFeature(Feature_Base selectedFeature, bool isOn)
    {
        if (isOn)
        {
            point -= selectedFeature.Point;
        }
        else
        {
            point += selectedFeature.Point;
        }

        txt_point.text = point.ToString();
    }

    public void ApplySetting()
    {
        foreach (var item in uisetList)
        {
            item.ApplyData(selectedHeroData);
        }

        selectedHeroData.UpdateImprovementAbilityStatData();
    }

    public void ResetSetting()
    {
        point = 10;
        txt_point.text = point.ToString();

        foreach (var item in uisetList)
        {
            item.Reset(selectedHeroData);
        }

        selectedHeroData.ResetFeature();
    }
}
