using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameScene : BaseScene
{
    private static GameScene instance;
    public static GameScene Instance { get { return instance; } }

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

        SceneName = "GameScene";
        TransferData();
    }

    private void TransferData()
    {
        PlayerController.Instance.CreateHero(GameManager.Instance.RegisteredHero[0]);
        PlayerController.Instance.CreateHero(GameManager.Instance.RegisteredHero[1]);
        PlayerController.Instance.CreateHero(GameManager.Instance.RegisteredHero[2]);
        PlayerController.Instance.CreateHero(GameManager.Instance.RegisteredHero[3]);

        PlayerController.Instance.TagHero(0);

        MobInfo mobInfo = GameManager.Instance.SelectedBoss;
        string bossPrefabPath = $"Mob/{mobInfo.MobData.MobName}/{mobInfo.MobData.MobName}_{mobInfo.MobData.MobLevel}";
        GameObject go = Instantiate(Resources.Load<GameObject>(bossPrefabPath));
        go.GetComponent<MobBehavior>().MobInit(mobInfo);
    }
}
