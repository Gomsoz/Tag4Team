using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using TMPro.Examples;
using UnityEngine;
using UnityEngine.UI;

public class UISet_FeatureSettingListItem : MonoBehaviour
{
    private const int itemHeight = 50;
    private const int itemMaxHeight = 210;

    [SerializeField]
    RectTransform rect;

    [SerializeField]
    private Toggle tgl_dropDown;

    [SerializeField]
    private Image img_dropDown;

    [SerializeField]
    private TMP_Text txt_featureName;

    [SerializeField]
    private TMP_Text txt_point;

    [SerializeField]
    private TMP_Text txt_description;

    [SerializeField]
    private GameObject panel_description;

    [SerializeField]
    private Button btn_chkFeature;

    [SerializeField]
    private Image img_chkFeature;

    private Feature_Base feature;
    public string FeatureName { get => feature.Name; }
    private bool isSelectedFeature = false;
    private bool IsChanged = false;

    private Action<bool> toggleDescriptionCallback;
    private Action<Feature_Base, bool> chkFeatureCallback;

    private void Start()
    {
        btn_chkFeature.onClick.AddListener(ChkFeature);
        tgl_dropDown.onValueChanged.AddListener(ToggleDescriptionPanel);
    }

    public void Initiate(Feature_Base featureData, Action<bool> _toggleDescriptionCallback, Action<Feature_Base, bool> _chkFeactureCallback)
    {
        feature = featureData;
        toggleDescriptionCallback = _toggleDescriptionCallback;
        chkFeatureCallback = _chkFeactureCallback;

        txt_featureName.text = featureData.Name;
        txt_point.text = featureData.Point.ToString();
        txt_description.text = featureData.Description;
    }

    public void ToggleDescriptionPanel(bool isOn)
    {
        if (isOn)
        {
            tgl_dropDown.transform.rotation = Quaternion.Euler(new Vector3(0, 0, 90));
            rect.sizeDelta = new Vector2(rect.sizeDelta.x, itemMaxHeight);
        }
        else
        {
            tgl_dropDown.transform.rotation = Quaternion.identity;
            rect.sizeDelta = new Vector2(rect.sizeDelta.x, itemHeight);
        }

        toggleDescriptionCallback(isOn);
        panel_description.SetActive(isOn);
    }

    public void ChkFeature()
    {
        if (isSelectedFeature == false)
        {
            if (PartySceneUIManager.Instance.Panel_FeatureSetting.FeaturePoint < feature.Point)
                return;

            isSelectedFeature = true;
            IsChanged = !IsChanged;
        }
        else
        {
            isSelectedFeature = false;
            IsChanged = !IsChanged;
        }

        chkFeatureCallback(feature, isSelectedFeature);
        img_chkFeature.gameObject.SetActive(isSelectedFeature);
    }

    public void SetChk(bool isChk)
    {
        isSelectedFeature = isChk;
        img_chkFeature.gameObject.SetActive(isChk);
    }

    public void Reset(HeroInfo hero)
    {
        if (isSelectedFeature)
        {
            hero.RevertFeature(feature);
            isSelectedFeature = false;
            IsChanged = false;
        }

        img_chkFeature.gameObject.SetActive(false);
    }

    public void ApplyData(HeroInfo hero)
    {
        if (IsChanged == false)
            return; 

        if (isSelectedFeature)
            hero.ApplyFeature(feature);
        else
            hero.RevertFeature(feature);

        IsChanged = false;
    }
}
