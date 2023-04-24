using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Panel_PartyBuffSummary : MonoBehaviour
{
    [SerializeField]
    private TMP_Text txt_hpPercentage;

    [SerializeField]
    private TMP_Text txt_mpPercentage;

    [SerializeField]
    private TMP_Text txt_dmgPercentage;

    [SerializeField]
    private TMP_Text txt_defensePercentage;

    public void SetData(PartyBuffData data)
    {
        txt_hpPercentage.text = $"{((data.HpLevel * data.HpPerLevel) * 100).ToString()} %";
        txt_mpPercentage.text = $"{((data.MpLevel * data.MpPerLevel) * 100).ToString()} %";
        txt_dmgPercentage.text = $"{((data.DmgLevel * data.DmgPerLevel) * 100).ToString()} %";
        txt_defensePercentage.text = $"{((data.AmorLevel * data.AmorPerLevel) * 100).ToString()} %";
    }
}
