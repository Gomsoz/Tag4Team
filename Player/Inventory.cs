using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.Tilemaps;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public static Inventory Instance { get; private set; }

    public int Coins { get; private set; }

    [SerializeField]
    private GameObject pnl_inventory;

    [SerializeField]
    private List<UISet_PlayerInventoryItem> items;

    [SerializeField]
    private TMP_Text txt_itemName;

    [SerializeField]
    private TMP_Text txt_itemDescription;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this);
        }
        else
        {
            Destroy(gameObject);
        }

        LoadInventoryData();
    }

    public void LoadInventoryData()
    {
        InventoryItemData datas = JsonManager.FromJson<InventoryItemData>("InventoryDatas");

        if (datas == null)
        {
            Coins = 500;
            SaveDefaultInventoryData();
        }
        else
        {
            Coins = datas.Coins;

            foreach(var item in items)
            {
                if(datas.ItemAmount.TryGetValue(item.ItemCode, out int amount))
                {
                    item.SetInventoryData(amount, OnClickItem);
                }
            }
        }
    }

    public void OnClickItem(string itemName, string itemDescription)
    {
        txt_itemName.text = itemName;
        txt_itemDescription.text = itemDescription;
    }

    public void SaveDefaultInventoryData()
    {
        InventoryItemData datas = new InventoryItemData();

        datas.ItemAmount.Add("0000", 0);
        datas.ItemAmount.Add("0001", 0);
        datas.ItemAmount.Add("0002", 0);

        datas.Coins = 500;

        JsonManager.ToJson(datas, "InventoryDatas");
    }

    public void SaveInventoryData()
    {
        InventoryItemData datas = new InventoryItemData();

        JsonManager.ToJson(datas, "InventoryDatas");
    }

    public void ToggleInventory(bool isOn)
    {
        pnl_inventory.SetActive(isOn);

        txt_itemName.text = string.Empty;
        txt_itemDescription.text = string.Empty;
    }
}

[Serializable]
public class InventoryItemData
{
    public Dictionary<string, int> ItemAmount = new();
    public int Coins = 0;
}