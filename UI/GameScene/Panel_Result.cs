using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Panel_Result : MonoBehaviour
{
    [SerializeField]
    private GameObject rewardItemPrefabs;

    [SerializeField]
    private Transform rewardItemParent;

    [SerializeField]
    private Button btn_goToRobby;

    private void Start()
    {
        btn_goToRobby.onClick.AddListener(OnClickGoToLobbyBtn);
    }

    public void OpenPanel(List<RewardData> rewardDatas = null)
    {
        gameObject.SetActive(true);

        if (rewardDatas == null)
            return;

        foreach (var item in rewardDatas)
        {
            SetRewardList(item);
        }
    }

    public void SetRewardList(RewardData rewardDatas)
    {
        GameObject go = Instantiate(rewardItemPrefabs, rewardItemParent);
        go.GetComponent<UISet_RewardItem>().SetRewardItem(rewardDatas);
        RewardManager.ApplyReward(rewardDatas);
    }

    public void OnClickGoToLobbyBtn()
    {
        SceneManager.LoadScene("LobbyScene");
    }
}
