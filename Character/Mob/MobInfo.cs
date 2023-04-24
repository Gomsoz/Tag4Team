using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobInfo : CharacterInfo
{
    public MobInfo(BossData bossData)
    {
        mobData= bossData.MobData;
        stat = bossData.StatData;
        mobRewards = bossData.RewardDatas;
    }

    private MobData mobData;
    public MobData MobData { get { return mobData; } }

    private List<RewardData> mobRewards;
    public List<RewardData> MobRewards { get { return mobRewards; } }
}
