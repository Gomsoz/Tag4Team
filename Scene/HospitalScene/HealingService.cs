using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Panel_HealingService : MonoBehaviour
{
    public UISet_HealingServiceItem curSlot;

    [SerializeField]
    private List<UISet_HealingServiceItem> slotList = new List<UISet_HealingServiceItem>();

    public void LoadSlotData(List<HealingServiceSlotData> slotDatas)
    {
        int idx = 0;
        foreach(var item in slotDatas)
        {
            slotList[idx].LoadSlotData(item);
            idx++;
        }
    }

    public List<HealingServiceSlotData> SaveSlotData()
    {
        List<HealingServiceSlotData> data = new List<HealingServiceSlotData>();
        foreach(var item in slotList)
        {
            data.Add(item.SaveSlotData());
        }

        return data;    
    }
}
