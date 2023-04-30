using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Panel_HeroManagement : MonoBehaviour
{
    [SerializeField]
    private Button btn_heroInfo;

    [SerializeField]
    private GameObject pnl_heroInfo;

    [SerializeField]
    private Button btn_heroSkill;

    [SerializeField]
    private GameObject pnl_heroSkill;

    [SerializeField]
    private Button btn_heroFeature;

    [SerializeField]
    private GameObject pnl_heroFeature;

    private void Start()
    {
        btn_heroInfo.onClick.AddListener(OpenHeroInfoPanel);
        // btn_heroSkill.onClick.AddListener(OpenHeroSkillPanel);
        btn_heroFeature.onClick.AddListener(OpenHeroFeaturePanel);
    }

    public void CloseAllPanel()
    {
        pnl_heroInfo.gameObject.SetActive(false);
        // pnl_heroSkill.gameObject.SetActive(false);
        pnl_heroFeature.gameObject.SetActive(false);
    }

    public void OpenHeroInfoPanel()
    {
        CloseAllPanel();
        pnl_heroInfo.gameObject.SetActive(true);
    }

    public void OpenHeroSkillPanel()
    {
        CloseAllPanel();
        pnl_heroSkill.gameObject.SetActive(true);
    }

    public void OpenHeroFeaturePanel()
    {
        CloseAllPanel();
        pnl_heroInfo.gameObject.SetActive(false);
        pnl_heroFeature.gameObject.SetActive(true);
    }
}
