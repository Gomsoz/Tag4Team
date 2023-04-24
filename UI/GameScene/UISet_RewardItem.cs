using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UISet_RewardItem : MonoBehaviour
{
    [SerializeField]
    private TMP_Text txt_rewardName;

    [SerializeField]
    private TMP_Text txt_rewardAmout;

    public void SetRewardItem(RewardData data)
    {
        txt_rewardName.text = data.rewardName;

        string amout = string.Empty;

        if(data.rewardAmout > 0)
        {
            amout = $"+ {data.rewardAmout}";
        }
        else if(data.rewardAmout < 0)
        {
            amout = $"- {data.rewardAmout}";
        }

        txt_rewardAmout.text = amout;
    }
}
