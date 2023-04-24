using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossDataManager : MonoBehaviour
{
    public static BossDataManager Instance { get; private set; }

    public Dictionary<string, List<BossData>> BossDatas { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this);
        }

        LoadMobDatas();
    }

    #region Generate Deafult Boss Data
    [ContextMenu("TEST")]
    public void GenerateDefaultBossData()
    {
        BossDataList newDataList = new BossDataList();

        List<BossData> newBossDataList = new();
        BossData newBossData = new();
        string savedBossName;
        RewardData newReward = new();

        // ������Ÿ���� �Ǹ� ������
        newBossDataList = new();

        // ������Ÿ���� �Ǹ� (����) ������
        newBossData = new();

        newBossData.MobData = new()
        {
            MobTitle = "������ Ÿ���� �Ǹ�",
            MobName = "DevilOfPrototype",
            MobLevel = MobLevel.Easy,
        };

        savedBossName = newBossData.MobData.MobName;

        newBossData.StatData = new()
        {
            Hp = 1000,
            OriginHp = 3000,
            Mp = 200,
            OriginMp = 200,
            OriginDmg = 30,
            OriginDef = 5,
            Barrier = 0,
            OriginAS = 1f,
            OriginMS = 2f,
            AttackRange_Angle2_x = -2f,
            AttackRange_Angle2_y = 2f,
        };

        newReward = new()
        {
            itemID = "00000000",
            rewardName = "��Ƽ ���� ����Ʈ",
            rewardAmout = 5,
        };

        newBossData.RewardDatas.Add(newReward);

        newBossData.AdditionalDescriptions = new()
        {
            "�߰� ��ȭ ����",
        };

        newBossDataList.Add(newBossData);

        // ������Ÿ���� �Ǹ� (����) ������
        newBossData = new();

        newBossData.MobData = new()
        {
            MobTitle = "������ Ÿ���� �Ǹ�",
            MobName = "DevilOfPrototype",
            MobLevel = MobLevel.Normal,
        };

        newBossData.StatData = new()
        {
            Hp = 1000,
            OriginHp = 3000,
            Mp = 200,
            OriginMp = 200,
            OriginDmg = 3,
            OriginDef = 5,
            Barrier = 0,
            AttackSpeed = 1f,
            MoveSpeed = 2f,
            Dmg = 30,
            AttackRange_Angle2_x = -2f,
            AttackRange_Angle2_y = 2f,
        };

        newReward = new()
        {
            itemID = "00000000",
            rewardName = "��Ƽ ���� ����Ʈ",
            rewardAmout = 5,
        };

        newBossData.RewardDatas.Add(newReward);

        newBossData.AdditionalDescriptions = new()
        {
            "���� �ɷ�ġ ��ȭ",
            "����ȭ"
        };

        newBossDataList.Add(newBossData);

        newDataList.Datas.Add(savedBossName, newBossDataList);

        // ������Ÿ���� �Ǹ� ������
        newBossDataList = new();

        // ���̷��� ŷ (����) ������
        newBossData = new();

        newBossData.MobData = new()
        {
            MobTitle = "���̷��� ŷ",
            MobName = "SkeletonKing",
            MobLevel = MobLevel.Easy,
        };

        savedBossName = newBossData.MobData.MobName;

        newBossData.StatData = new()
        {
            Hp = 1000,
            OriginHp = 3000,
            Mp = 200,
            OriginMp = 200,
            OriginDmg = 3,
            OriginDef = 5,
            Barrier = 0,
            AttackSpeed = 1f,
            MoveSpeed = 2f,
            Dmg = 30,
            AttackRange_Angle2_x = -2f,
            AttackRange_Angle2_y = 2f,
        };

        newReward = new()
        {
            itemID = "00000000",
            rewardName = "��Ƽ ���� ����Ʈ",
            rewardAmout = 5,
        };

        newBossData.RewardDatas.Add(newReward);

        newBossData.AdditionalDescriptions = new()
        {
            "�߰� ��ȭ ����",
        };

        newBossDataList.Add(newBossData);

        // ���̷��� ŷ (����) ������
        newBossData = new();

        newBossData.MobData = new()
        {
            MobTitle = "���̷��� ŷ",
            MobName = "SkeletonKing",
            MobLevel = MobLevel.Normal,
        };

        newBossData.StatData = new()
        {
            Hp = 1000,
            OriginHp = 3000,
            Mp = 200,
            OriginMp = 200,
            OriginDmg = 3,
            OriginDef = 5,
            Barrier = 0,
            AttackSpeed = 1f,
            MoveSpeed = 2f,
            Dmg = 30,
            AttackRange_Angle2_x = -2f,
            AttackRange_Angle2_y = 2f,
        };

        newReward = new()
        {
            itemID = "00000000",
            rewardName = "��Ƽ ���� ����Ʈ",
            rewardAmout = 5,
        };

        newBossData.RewardDatas.Add(newReward);

        newBossData.AdditionalDescriptions = new()
        {
            "���� �ɷ�ġ ��ȭ",
            "�߰� �ذ� ��ȯ",
            "����ȭ",
        };

        newBossDataList.Add(newBossData);

        newDataList.Datas.Add(savedBossName, newBossDataList);

        JsonManager.ToJson(newDataList, "BossDefaultDatas");
    }

    #endregion

    [ContextMenu("LoadData")]
    public void LoadMobDatas()
    {
        BossDataList loadedMobDataList = JsonManager.FromJson<BossDataList>("BossDatas");

        if (loadedMobDataList == null)
        {
            GenerateDefaultBossData();
            loadedMobDataList = JsonManager.FromJson<BossDataList>("BossDefaultDatas");
        }

        BossDatas = loadedMobDataList.Datas;
    }

    public void SaveMobDatas()
    {
        BossDataList savedMobDataList = new BossDataList()
        {
            Datas = BossDatas,
        };

        JsonManager.ToJson(savedMobDataList, "BossDatas");
    }

    public void BossIsHunted(MobInfo bossInfo)
    {
        List<BossData> targetList = BossDatas[bossInfo.MobData.MobName];

        targetList[(int)bossInfo.MobData.MobLevel].MobData.isHunted = true;
        SaveMobDatas();
    }
}

[Serializable]
public class BossDataList
{
    public Dictionary<string, List<BossData>> Datas = new();
}

[Serializable]
public class BossData
{
    public CharacterStatData StatData;
    public MobData MobData;
    public List<RewardData> RewardDatas = new();
    public List<string> AdditionalDescriptions = new();
}
