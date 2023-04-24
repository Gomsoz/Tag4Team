using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UISet_BossListItem : MonoBehaviour
{
    private string bossName;

    #region UI

    [SerializeField]
    private Button btn_select;

    [SerializeField]
    private Image img_boss;

    [SerializeField]
    private TMP_Text txt_title;

    #endregion

    public void SetItem(BossData bossData)
    {
        bossName = bossData.MobData.MobName;

        string IdleImagePath = $"Mob/{bossName}/Sprites/CharacterImage_{bossName}";
        img_boss.sprite = Resources.Load<Sprite>(IdleImagePath);

        txt_title.text = bossData.MobData.MobTitle;

        btn_select.onClick.AddListener(OnClickSelectBtn);
    }

    public void OnClickSelectBtn()
    {
        BossLobbyScene.Instance.Panel_SelectBoss.SetSelectedBossData(BossDataManager.Instance.BossDatas[bossName]);
        BossLobbyScene.Instance.Panel_BossList.gameObject.SetActive(false);
    }
}
