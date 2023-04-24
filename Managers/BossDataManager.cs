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

        // 프로토타입의 악마 데이터
        newBossDataList = new();

        // 프로토타입의 악마 (쉬움) 데이터
        newBossData = new();

        newBossData.MobData = new()
        {
            MobTitle = "프로토 타입의 악마",
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
            rewardName = "파티 버프 포인트",
            rewardAmout = 5,
        };

        newBossData.RewardDatas.Add(newReward);

        newBossData.AdditionalDescriptions = new()
        {
            "추가 강화 없음",
        };

        newBossDataList.Add(newBossData);

        // 프로토타입의 악마 (보통) 데이터
        newBossData = new();

        newBossData.MobData = new()
        {
            MobTitle = "프로토 타입의 악마",
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
            rewardName = "파티 버프 포인트",
            rewardAmout = 5,
        };

        newBossData.RewardDatas.Add(newReward);

        newBossData.AdditionalDescriptions = new()
        {
            "보스 능력치 강화",
            "광폭화"
        };

        newBossDataList.Add(newBossData);

        newDataList.Datas.Add(savedBossName, newBossDataList);

        // 프로토타입의 악마 데이터
        newBossDataList = new();

        // 스켈레톤 킹 (쉬움) 데이터
        newBossData = new();

        newBossData.MobData = new()
        {
            MobTitle = "스켈레톤 킹",
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
            rewardName = "파티 버프 포인트",
            rewardAmout = 5,
        };

        newBossData.RewardDatas.Add(newReward);

        newBossData.AdditionalDescriptions = new()
        {
            "추가 강화 없음",
        };

        newBossDataList.Add(newBossData);

        // 스켈레톤 킹 (보통) 데이터
        newBossData = new();

        newBossData.MobData = new()
        {
            MobTitle = "스켈레톤 킹",
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
            rewardName = "파티 버프 포인트",
            rewardAmout = 5,
        };

        newBossData.RewardDatas.Add(newReward);

        newBossData.AdditionalDescriptions = new()
        {
            "보스 능력치 강화",
            "추가 해골 소환",
            "광폭화",
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
