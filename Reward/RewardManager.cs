using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class RewardManager
{
    // 12345678 
    // 12 : 보상 번호
    // 34 : 분류 번호
    // 5678 : 아이템 번호

    public static void ApplyReward(RewardData reward)
    {
        string rewardNum = reward.itemID.Substring(0, 2);
        if(rewardNum == "00")
        {
            PartySceneData loadData = JsonManager.FromJson<PartySceneData>("PartyDatas");
            PartyBuffData partyBuffData = loadData.PartyBuff;

            partyBuffData.DefaultPoint += reward.rewardAmout;
            partyBuffData.Point += reward.rewardAmout;

            JsonManager.ToJson(loadData, "PartyDatas");
        }
        else
        {

        }
    }
}

[Serializable]
public class RewardData
{
    public string itemID;
    public string rewardName;
    public int rewardAmout;
}
