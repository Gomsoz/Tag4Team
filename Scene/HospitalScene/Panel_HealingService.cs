using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealingService : MonoBehaviour
{
    public UISet_HealingServiceItem curSlot;

    [SerializeField]
    private List<UISet_HealingServiceItem> slotList = new List<UISet_HealingServiceItem>();

    private void Start()
    {
        foreach(var item in slotList)
        {
            item.InitSlot(OnMouseCallback);
        }
    }

    public void OnMouseCallback(int slotNum)
    {
        curSlot = slotList[slotNum];
    }
}
