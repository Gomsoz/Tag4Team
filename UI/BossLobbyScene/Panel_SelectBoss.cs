using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public enum MobDifficulty
{
    Easy,
    Normal,
    Hard,
    Cnt,
}
public class Panel_SelectBoss : MonoBehaviour
{
    private MobDifficulty level = MobDifficulty.Easy;
    private MobInfo mobInfo;

    private List<BossData> selectedBossDatas = new();
    private int bossLevelIdx = 0;

    public BossData CurBossData
    {
        get
        {
            return selectedBossDatas[bossLevelIdx];
        }
    }

    #region UI

    [SerializeField]
    private Button btn_openBossList;

    [SerializeField]
    private TMP_Text txt_mobTitle;

    [SerializeField]
    private Button btn_bossLevelDown;

    [SerializeField]
    private Button btn_bossLevelUp;

    [SerializeField]
    private TMP_Text txt_bossLevel;

    [SerializeField]
    private List<TMP_Text> txt_bossAdditionalDescriptions = new();

    [SerializeField]
    private GameObject pnl_huntedMark;

    [SerializeField]
    private Image img_selectedBoss;

    #endregion

    private void Start()
    {
        btn_openBossList.onClick.AddListener(OpenBossList);
        btn_bossLevelDown.onClick.AddListener(OnClickLevelDown);
        btn_bossLevelUp.onClick.AddListener(OnClickLevelUp);
    }

    public void OpenBossList()
    {
        BossLobbyScene.Instance.Panel_BossList.gameObject.SetActive(true);
    }

    public void SetSelectedBossData(MobInfo bossInfo)
    {
        List<BossData> bossDatas =
            BossDataManager.Instance.BossDatas[bossInfo.MobData.MobName];

        SetSelectedBossData(bossDatas, bossInfo.MobData.MobDifficulty);
    }

    public void SetSelectedBossData(List<BossData> _selectedBossData, MobDifficulty level = 0)
    {
        selectedBossDatas = _selectedBossData;

        string bossName = selectedBossDatas[0].MobData.MobName;
        string IdleImagePath = $"Mob/{bossName}/Sprites/CharacterImage_{bossName}";
        Sprite bossImage = Resources.Load<Sprite>(IdleImagePath);

        img_selectedBoss.sprite = bossImage;
        bossLevelIdx = (int)level;

        txt_mobTitle.text = selectedBossDatas[bossLevelIdx].MobData.MobTitle;

        SetBossInfo();
    }

    public void OnClickLevelDown()
    {
        if (bossLevelIdx <= 0)
            return;

        bossLevelIdx--;

        SetBossInfo();
    }

    public void OnClickLevelUp()
    {
        if (bossLevelIdx >= selectedBossDatas.Count - 1)
            return;

        bossLevelIdx++;

        SetBossInfo();
    }

    private void SetBossInfo()
    {
        txt_bossLevel.text = selectedBossDatas[bossLevelIdx].MobData.MobDifficulty.ToString();

        SetAdditionalDescription();
        GameManager.Instance.SetBossFromLobby(CurBossData);

        pnl_huntedMark.SetActive(CurBossData.MobData.isHunted);
    }

    private void SetAdditionalDescription()
    {
        for (int i = 0; i < txt_bossAdditionalDescriptions.Count; i++)
        {
            txt_bossAdditionalDescriptions[i].text = string.Empty;
        }

        List<string> additionalDescriptions = selectedBossDatas[bossLevelIdx].AdditionalDescriptions;
        for (int i = 0; i < additionalDescriptions.Count; i++)
        {
            txt_bossAdditionalDescriptions[i].text = additionalDescriptions[i];
        }
    }
}
