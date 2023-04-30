using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDataManager : MonoBehaviour
{
    public static ItemDataManager Instance { get; private set; }

    public Dictionary<string, ItemData> ItemDataList { get; private set; }

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

        LoadGameData();
    }

    public void LoadGameData()
    {
        ItemDataList = new();

        ItemDataList datas = JsonManager.FromJson<ItemDataList>("ItemDatas");

        if (datas == null)
        {
            SaveDefaultGameData();
            datas = JsonManager.FromJson<ItemDataList>("ItemDatas");
        }

        foreach(var item in datas.ItemList)
        {
            ItemDataList.Add(item.Key, item.Value);
        }
    }

    public void SaveDefaultGameData()
    {
        ItemDataList datas = new ItemDataList();

        ItemData item = new ItemData()
        {
            ItemName = "Red Potion",
            itemCooldown = 10,
            itemPrice = 100,
            ItemDescription = "체력을 30% 회복합니다.",
        };

        datas.ItemList.Add("0000", item);

        item = new ItemData()
        {
            ItemName = "Orange Potion",
            itemCooldown = 10,
            itemPrice = 200,
            ItemDescription = "체력을 50% 회복합니다.",
        };

        datas.ItemList.Add("0001", item);

        item = new ItemData()
        {
            ItemName = "Blue Potion",
            itemCooldown = 10,
            itemPrice = 200,
            ItemDescription = "마나을 50% 회복합니다.",
        };

        datas.ItemList.Add("0002", item);

        JsonManager.ToJson(datas, "ItemDatas");
    }

    public void SaveGameData()
    {
        ItemDataList datas = new ItemDataList();

        JsonManager.ToJson(datas, "ItemDatas");
    }
}

[Serializable]
public class ItemDataList
{
    public Dictionary<string, ItemData> ItemList = new();
}

[Serializable]
public class ItemData
{
    public string ItemName;
    public int itemCooldown;
    public int itemPrice;
    public string ItemDescription;
}