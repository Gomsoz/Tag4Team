using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Btn_SelectCharacter : MonoBehaviour
{
    public HeroJobs curHero;

    public void OnClick() 
    {
        PartySceneUIManager.Instance.Panel_SelectCharacter.gameObject.SetActive(false);
        PartySceneUIManager.Instance.Panel_CharacterInfos.SetHeroData(curHero);
    }
}
