using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameScene : BaseScene
{
    private static GameScene instance;
    public static GameScene Instance { get { return instance; } }

    #region UI

    [SerializeField]
    private GameObject notifyWarning;

    [SerializeField]
    private TMP_Text txt_warning;

    [SerializeField]
    private GameObject sceneUICanvas;

    #endregion

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
        GameManager.Instance.SetExitBtn(false);

        TransferData();
    }

    private void TransferData()
    {
        PlayerController.Instance.CreateHero(GameManager.Instance.RegisteredHero[0]);
        PlayerController.Instance.CreateHero(GameManager.Instance.RegisteredHero[1]);
        PlayerController.Instance.CreateHero(GameManager.Instance.RegisteredHero[2]);
        PlayerController.Instance.CreateHero(GameManager.Instance.RegisteredHero[3]);

        PlayerController.Instance.TagHero(0);

        StartCoroutine(StartGame());
    }

    private IEnumerator StartGame()
    {
        while (true)
        {
            yield return new WaitForSeconds(5);
            break;
        }

        notifyWarning.SetActive(true);

        int blink = 3;
        float alphaValue = 0;
        Color targetColor = txt_warning.color;
        while (blink > 0)
        {
            while (alphaValue < 1)
            {
                alphaValue += Time.deltaTime;
                txt_warning.color = new Color(targetColor.r, targetColor.g, targetColor.b, alphaValue);
                yield return null;
            }

            while (alphaValue > 0)
            {
                alphaValue -= Time.deltaTime;
                txt_warning.color = new Color(targetColor.r, targetColor.g, targetColor.b, alphaValue);
                yield return null;
            }

            yield return null;
            blink--;
        }

        notifyWarning.SetActive(false);
        sceneUICanvas.SetActive(false);

        yield return StartCoroutine(GameManager.Instance.FadeIn());

        MobInfo mobInfo = GameManager.Instance.SelectedBoss;
        string bossPrefabPath = $"Mob/{mobInfo.MobData.MobName}/{mobInfo.MobData.MobName}_{mobInfo.MobData.MobDifficulty}";
        GameObject go = Instantiate(Resources.Load<GameObject>(bossPrefabPath));
        go.GetComponent<MobBehavior>().MobInit(mobInfo);
    }

    public void EndAppearanceCutScene()
    {
        sceneUICanvas.SetActive(true);
    }
}
