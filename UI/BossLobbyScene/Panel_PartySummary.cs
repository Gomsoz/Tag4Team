using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Panel_PartySummary : MonoBehaviour
{
    [SerializeField]
    private List<UISet_PartySummaryHeroInfo> heroinfos = new();

    public void LoadHeroInfos(List<PartySlotData> slotData)
    {
        for(int i = 0; i < 4; i++)
        {
            heroinfos[i].SetHeroData(slotData[i].job);
        }
    }
}
