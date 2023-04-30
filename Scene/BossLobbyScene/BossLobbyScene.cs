using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BossLobbyScene : BaseScene
{
    private static BossLobbyScene instance;
    public static BossLobbyScene Instance { get { return instance; } }

    [field: SerializeField]
    public Panel_PartySummary Panel_PartySummary { get; private set; }

    [field: SerializeField]
    public Panel_PartyBuffSummary Panel_PartyBuffSummary { get; private set; }

    [field: SerializeField]
    public Panel_SelectBoss Panel_SelectBoss { get; private set; }

    [field: SerializeField]
    public Panel_BossList Panel_BossList { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            instance = this;
        }
    }

    protected override void InitScene()
    {
        base.InitScene();

        SceneName = "BossLobbyScene";
        fixedPreScene = "LobbyScene";
    }

    public override void LoadSceneData()
    {
        base.LoadSceneData();

        PartySceneData partyDatas = JsonManager.FromJson<PartySceneData>("PartyDatas");

        if (partyDatas == null)
            return;

        Panel_PartySummary.LoadHeroInfos(partyDatas.SlotDatas.ToList<PartySlotData>());
        Panel_PartyBuffSummary.SetData(partyDatas.PartyBuff);

        if (GameManager.Instance.SelectedBoss != null)
            Panel_SelectBoss.SetSelectedBossData(GameManager.Instance.SelectedBoss);
    }

    public override void SaveSceneData()
    {
        base.SaveSceneData();
    }

    public void LoadLobbyScene()
    {
        SaveSceneData();

        SceneManager.LoadScene("LobbyScene");
    }

    public void LoadPartyScene()
    {
        SaveSceneData();

        SceneManager.LoadScene("PartyScene");
    }

    public void LoadGameScene()
    {
        SaveSceneData();

        if (GameManager.Instance.SelectedBoss.MobData.isHunted)
        {
            GameManager.Instance.OpenCommonPopup(CommonPopup.Done, "토벌이 완료된 보스입니다.", null);
            return;
        }

        SceneManager.LoadScene("GameScene");
    }
}
