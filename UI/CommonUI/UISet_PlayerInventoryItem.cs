using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UISet_PlayerInventoryItem : MonoBehaviour
{
    [SerializeField]
    private TMP_Text txt_itemCnt;

    [SerializeField]
    private string itemCode;
    public string ItemCode { get { return itemCode; } }

    public string ItemName { get; private set; }
    public string ItemDescription { get; private set; }
    private int itemAmount;

    private Action<string, string> onClickItem;

    private void Start()
    {
        if (ItemCode == string.Empty)
            return;

        ItemData data = ItemDataManager.Instance.ItemDataList[ItemCode];

        ItemName = data.ItemName;
        ItemDescription = data.ItemDescription;


    }

    public void SetInventoryData(int amount, Action<string, string> onClickCallback)
    {
        itemAmount += amount;
        txt_itemCnt.text = itemAmount.ToString();
        onClickItem = onClickCallback;
    }

    public void OnClickItem()
    {
        onClickItem(ItemName, ItemDescription);
    }
}
