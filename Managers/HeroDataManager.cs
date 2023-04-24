using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum HeroJobs
{
    None,
    Tanker,
    Healer,
    Mage,
    SwordMaster,
    Cnt,
}

public class HeroDataManager : MonoBehaviour
{
    public static HeroDataManager Instance { get; private set; }

    private Dictionary<int, HeroInfo> heroDatas = new();

    private PartyBuff curAppliedPartyBuff = new PartyBuff();

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this);
        }

        // 임시로 데이터를 넣음.
        // 데이터를 로드 했다고 가정함.
        LoadHeroDatas();
        //tp_AddTankerData();
        //tp_AddHealerData();
        //tp_MageData();
        //tp_SwordMasterData();
    }

    public void ChangedPartyBuffListener(PartyBuff changedPartyBuff)
    {
        PartyBuff adjustedPartyBuff = changedPartyBuff - curAppliedPartyBuff;
        foreach (var key in heroDatas.Keys)
        {
            HeroInfo data = heroDatas[key];
            data.Stat.PercentageHp += adjustedPartyBuff.PercentageHp;
            data.Stat.PercentageMp += adjustedPartyBuff.PercentageMp;
            data.Stat.PercentageDmg += adjustedPartyBuff.PercentageDmg;
            data.Stat.PercentageDef += adjustedPartyBuff.PercentageAmor;
            data.UpdateImprovementAbilityStatData();
        }

        curAppliedPartyBuff = changedPartyBuff;
    }

    #region Generate Hero Default Data
    [ContextMenu("Generate Default Data")]
    public void GenerateDefaultHeroData()
    {
        HeroDataList newHeroDataList = new HeroDataList();

        List<CharacterStatData> characterStatList = new List<CharacterStatData>();
        List<HeroData> heroDataList = new List<HeroData>();

        // 탱커
        CharacterStatData stat = new()
        {
            Hp = 500,
            OriginHp = 500,
            Mp = 100,
            OriginMp = 100,
            OriginDmg = 30,
            OriginDef = 20,
            Barrier = 0,
            OriginAS = 1,
            OriginMS = 1f,
        };
        characterStatList.Add(stat);

        HeroData heroData = new()
        {
            HeroCode = 1,
            isNormalAttack = true,

            SkillData = new string[4] { "000000", "000100", "000200", "000300" },
        };
        heroDataList.Add(heroData);

        // 힐러
        stat = new()
        {
            Hp = 300,
            OriginHp = 300,
            Mp = 500,
            OriginMp = 500,
            OriginDmg = 5,
            OriginDef = 10,
            Barrier = 0,
            OriginAS = 1,
            OriginMS = 2f,
            AttackRange_Angle2_x = -2f,
            AttackRange_Angle2_y = 0.8f,
        };
        characterStatList.Add(stat);

        heroData = new()
        {
            HeroCode = 2,
            isNormalAttack = false,

            SkillData = new string[4] { "010000", "010100", "010200", "010300" },

        };
        heroDataList.Add(heroData);

        // 마법사
         stat = new()
        {
            Hp = 350,
            OriginHp = 350,
            Mp = 1000,
            OriginMp = 1000,
            OriginDmg = 100,
            OriginDef = 0,
            Barrier = 0,
            OriginAS = 1,
            OriginMS = 2f,
        };
        characterStatList.Add(stat);

        heroData = new()
        {
            HeroCode = 3,
            isNormalAttack = false,

            SkillData = new string[4] { "020000", "020100", "020200", "020300" },
        };
        heroDataList.Add(heroData);

        // 검사
        stat = new()
        {
            Hp = 500,
            OriginHp = 500,
            Mp = 200,
            OriginMp = 200,
            OriginDmg = 50,
            OriginDef = 10,
            Barrier = 0,
            OriginAS = 1,
            OriginMS = 2f,
        };
        characterStatList.Add(stat);

        heroData = new()
        {
            HeroCode = 4,
            isNormalAttack = true,

            SkillData = new string[4] { "030000", "030100", "030200", "030300" },
        };
        heroDataList.Add(heroData);

        newHeroDataList.CharacterStatDatas = characterStatList.ToArray();
        newHeroDataList.HeroDatas = heroDataList.ToArray();

        JsonManager.ToJson(newHeroDataList, "DefaultHeroDatas");
    }
    #endregion

    public void SaveHeroDatas()
    {
        HeroDataList newHeroDataList = new HeroDataList();

        List<CharacterStatData> characterStatList = new List<CharacterStatData>();
        List<HeroData> heroDataList = new List<HeroData>();

        foreach(var item in heroDatas)
        {
            characterStatList.Add(item.Value.Stat);
            heroDataList.Add(item.Value.Herodata);
        }

        newHeroDataList.CharacterStatDatas = characterStatList.ToArray();
        newHeroDataList.HeroDatas = heroDataList.ToArray();

        JsonManager.ToJson(newHeroDataList, "HeroDatas");
    }

    public void LoadHeroDatas()
    {
        HeroDataList newHeroDataList = JsonManager.FromJson<HeroDataList>("HeroDatas");

        if(newHeroDataList == null)
        {
            GenerateDefaultHeroData();
            newHeroDataList = JsonManager.FromJson<HeroDataList>("DefaultHeroDatas");
        }

        HeroInfo newHeroInfo = new HeroInfo();

        for (int idx = 0; idx < (int)HeroJobs.Cnt - 1; idx++)
        {
            newHeroInfo = new HeroInfo();

            newHeroInfo.Stat = newHeroDataList.CharacterStatDatas[idx];
            newHeroInfo.Herodata = newHeroDataList.HeroDatas[idx];

            string heroName = Enum.GetName(typeof(HeroJobs), idx + 1);
            string IdleImagePath = $"Hero/{heroName}/Sprites/CharacterImage_{heroName}";
            newHeroInfo.CharacterImage = Resources.Load<Sprite>(IdleImagePath);

            newHeroInfo.UpdateImprovementAbilityStatData();
            heroDatas.Add(newHeroInfo.Herodata.HeroCode, newHeroInfo);
        }

        SkillData data;
        data = new()
        {
            Name = "방패 휘두르기",
            Id = "000000",
            Mana = 10,
            Cooldown = 10,
        };
        heroDatas[(int)HeroJobs.Tanker].skill.skilldatas.Add(data.Id, data);

        data = new()
        {
            Name = "방패 올리기",
            Id = "000100",
            Mana = 50,
            Cooldown = 20,
            IsBuff = true,
            BuffDuration = 10,
        };
        heroDatas[(int)HeroJobs.Tanker].skill.skilldatas.Add(data.Id, data);

        data = new()
        {
            Name = "맹약",
            Id = "000200",
            Mana = 30,
            Cooldown = 20,
            isTargeting = true,
            IsBuff = true,
            BuffDuration = 10,
        };
        heroDatas[(int)HeroJobs.Tanker].skill.skilldatas.Add(data.Id, data);

        data = new()
        {
            Name = "수호",
            Id = "000300",
            Mana = 100,
            Cooldown = 20,
            IsContinuously = true,
            SkillDuration = 10,
            BuffDuration = 10,
        };
        heroDatas[(int)HeroJobs.Tanker].skill.skilldatas.Add(data.Id, data);

        data = new()
        {
            Name = "정화",
            Id = "010000",
            Mana = 5,
            Cooldown = 5,
        };
        heroDatas[(int)HeroJobs.Healer].skill.skilldatas.Add(data.Id, data);

        data = new()
        {
            Name = "치유",
            Id = "010100",
            Mana = 10,
            Cooldown = 5,
            isTargeting = true,
        };
        heroDatas[(int)HeroJobs.Healer].skill.skilldatas.Add(data.Id, data);

        data = new()
        {
            Name = "광역 치유",
            Id = "010200",
            Mana = 50,
            Cooldown = 10,
            IsContinuously = true,
            BuffDuration = 7,
        };
        heroDatas[(int)HeroJobs.Healer].skill.skilldatas.Add(data.Id, data);

        data = new()
        {
            Name = "기도",
            Id = "010300",
            Mana = 200,
            Cooldown = 30,
            IsBuff = true,
            BuffDuration = 10,
        };
        heroDatas[(int)HeroJobs.Healer].skill.skilldatas.Add(data.Id, data);

        data = new()
        {
            Name = "파이어 볼",
            Id = "020000",
            Mana = 5,
            Cooldown = 3,
            IsCasting = true,
            CastingTime = 1,
        };
        heroDatas[(int)HeroJobs.Mage].skill.skilldatas.Add(data.Id, data);

        data = new()
        {
            Name = "파이어 쉴드",
            Id = "020100",
            Mana = 10,
            Cooldown = 15,
            IsCasting = true,
            CastingTime = 1,
            BuffDuration = 5,
        };
        heroDatas[(int)HeroJobs.Mage].skill.skilldatas.Add(data.Id, data);

        data = new()
        {
            Name = "파이어 필드",
            Id = "020200",
            Mana = 50,
            Cooldown = 20,
            IsCasting = true,
            CastingTime = 3,
        };
        heroDatas[(int)HeroJobs.Mage].skill.skilldatas.Add(data.Id, data);

        data = new()
        {
            Name = "메테오",
            Id = "020300",
            Mana = 200,
            Cooldown = 30,
            IsCasting = true,
            CastingTime = 3,
            isSelectLocation = true,
        };
        heroDatas[(int)HeroJobs.Mage].skill.skilldatas.Add(data.Id, data);

        data = new()
        {
            Name = "강타",
            Id = "030000",
            Mana = 5,
            Cooldown = 5,
        };
        heroDatas[(int)HeroJobs.SwordMaster].skill.skilldatas.Add(data.Id, data);

        data = new()
        {
            Name = "신체 강화",
            Id = "030100",
            Mana = 10,
            Cooldown = 20,
        };
        heroDatas[(int)HeroJobs.SwordMaster].skill.skilldatas.Add(data.Id, data);

        data = new()
        {
            Name = "돌진",
            Id = "030200",
            Mana = 50,
            Cooldown = 10
        };
        heroDatas[(int)HeroJobs.SwordMaster].skill.skilldatas.Add(data.Id, data);

        data = new()
        {
            Name = "도약",
            Id = "030300",
            Mana = 200,
            Cooldown = 10,
            isSelectLocation = true,
        };
        heroDatas[(int)HeroJobs.SwordMaster].skill.skilldatas.Add(data.Id, data);
    }

    public HeroInfo GetHerodata(HeroJobs job)
    {
        return heroDatas[(int)job];
    }

    public void SetHeroData(HeroJobs job, HeroInfo _heroInfo)
    {
        heroDatas[(int)job] = _heroInfo;
    }
}

[Serializable]
public class HeroDataList
{
    public CharacterStatData[] CharacterStatDatas;
    public HeroData[] HeroDatas;
}
