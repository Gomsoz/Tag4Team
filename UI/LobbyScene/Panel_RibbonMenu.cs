using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Panel_RibbonMenu : MonoBehaviour
{
    public void CloseAllPanel()
    {
        PartySceneUIManager.Instance.Panel_PartyBuff.gameObject.SetActive(false);
        PartySceneUIManager.Instance.Panel_HeroList.gameObject.SetActive(false);
        PartySceneUIManager.Instance.Panel_Character.gameObject.SetActive(false);
    }
    public void OpenPartyBuffPanel()
    {
        CloseAllPanel();
        PartySceneUIManager.Instance.Panel_PartyBuff.gameObject.SetActive(true);
    }

    public void OpenHeroListPanel()
    {
        CloseAllPanel();
        PartySceneUIManager.Instance.Panel_HeroList.gameObject.SetActive(true);
    }

    public void OpenCharacterPanel()
    {
        CloseAllPanel();
        PartySceneUIManager.Instance.Panel_Character.gameObject.SetActive(true);
    }
    
}
