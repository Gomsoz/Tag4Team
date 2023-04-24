using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PartySceneUIManager : MonoBehaviour
{
    private static PartySceneUIManager instance = null;

    public static PartySceneUIManager Instance { get => instance; }

    [field: SerializeField]
    public Panel_HeroList Panel_HeroList { get; private set; }

    [field: SerializeField]
    public Panel_PartyBuff Panel_PartyBuff { get; private set; }

    [field:SerializeField]
    public GameObject Panel_Character { get; private set; }

    [field:SerializeField]
    public Panel_FeatureSetting Panel_FeatureSetting { get; private set; }

    [field:SerializeField]
    public Panel_CharacterInfos Panel_CharacterInfos { get; private set; }

    [field: SerializeField]
    public Panel_SelectCharacter Panel_SelectCharacter { get; private set; }

    private void Awake()
    {
        if (instance == null)
            instance = this;
    }
}
