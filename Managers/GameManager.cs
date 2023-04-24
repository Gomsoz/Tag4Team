using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum CommonPopup
{
    ApplyCancel,
    Done,
}

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    #region Transfered Battle Data

    public Dictionary<int, HeroInfo> RegisteredHero { get; private set; }
    public MobInfo SelectedBoss { get; private set; }

    #endregion

    public BaseScene CurScene;
    public string PreSceneName;

    [SerializeField]
    private GameObject pfb_commonPanel;

    #region Time

    private DateInGame curTime;
    public DateInGame CurTime { get => curTime; }
    public int DayInGame { get => curTime.Day; }
    private int minPerHourInGame = 2;
    public int HourInGame { get => curTime.Hour; }
    public int MinuteInGame { get => curTime.Minute; }
    public Action tenMinutesEvent = null;
    public Action<int> everyHourEvent = null;
    public Action<int> everyDayEvent = null;

    #endregion

    private static bool paused = false;
    public static bool Paused
    {
        get { return paused; }
        set
        {
            paused = value;
            Time.timeScale = value ? 0 : 1;
        }
    }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this);
            RegisteredHero = new Dictionary<int, HeroInfo>();
        }

        LoadGameData();
        // curTime = new DateInGame(0, 0, 0);
    }

    private void Start()
    {
        StartCoroutine(DayTimer());

        SceneManager.sceneUnloaded -= ResetGameManagerOnUnloaded;
        SceneManager.sceneUnloaded += ResetGameManagerOnUnloaded;
    }

    public void ResetGameManagerOnUnloaded(Scene scene)
    {
        PreSceneName = scene.name;

        tenMinutesEvent = null;
        everyDayEvent = null;
        everyHourEvent = null;
    }

    public void SaveGameData()
    {
        GameDatas datas = new GameDatas();

        datas.Date = curTime;

        JsonManager.ToJson(datas, "GameDatas");
    }

    public void LoadGameData()
    {
        GameDatas datas = JsonManager.FromJson<GameDatas>("GameDatas");

        if (datas == null)
        {
            curTime = new DateInGame(0, 0, 0);
            SaveGameData();
        }
        else
        {
            curTime = datas.Date;
        }
    }

    public void GamePause()
    {
        Paused = !Paused;
    }

    public void SetRegisteredHeroFromLobby(int listNum, HeroInfo heroData)
    {
        if (RegisteredHero.ContainsKey(listNum))
            RegisteredHero.Remove(listNum);

        RegisteredHero.Add(listNum, heroData);
    }

    public void SetBossFromLobby(BossData bossData)
    {
        MobInfo newBossInfo = new MobInfo(bossData);
        SelectedBoss = newBossInfo;
    }

    public void Defeat()
    {
        PlayerController.Instance.SaveHeroData();
        Managers.Instance.UI.Panel_Result.OpenPanel();
    }

    public void Victory()
    {
        PlayerController.Instance.SaveHeroData();
        BossDataManager.Instance.BossIsHunted(SelectedBoss);
        Managers.Instance.UI.Panel_Result.OpenPanel(SelectedBoss.MobRewards);
    }

    //private void OnApplicationQuit()
    //{
    //    CurScene.SaveSceneData();
    //    CurScene?.SaveDataPublisher?.Invoke();
    //}

    private IEnumerator DayTimer()
    {
        while (true)
        {
            yield return new WaitForSeconds(1 * minPerHourInGame);

            curTime.Minute += 10;

            if (curTime.Minute >= 60)
            {
                curTime.Hour += 1;
                curTime.Minute = 0;
                everyHourEvent?.Invoke(curTime.Hour);
            }

            if (curTime.Hour >= 24)
            {
                curTime.Day += 1;
                curTime.Hour = 0;
                everyDayEvent?.Invoke(curTime.Day);
            }

            tenMinutesEvent?.Invoke();
        }
    }

    public void OpenCommonPopup(CommonPopup popup, string contents, Action callback_1, Action callback_2 = null)
    {
        GamePause();
        GameObject go = Instantiate(pfb_commonPanel, GameObject.Find("SceneUI").transform);
        Panel_CommonPopup panel_commonPopup = go.GetComponent<Panel_CommonPopup>();

        switch (popup)
        {
            case CommonPopup.ApplyCancel:
                panel_commonPopup.OpenApplyCancelPopup(contents, callback_1, callback_2);
                break;
            case CommonPopup.Done:
                panel_commonPopup.OpenDonePopup(contents, callback_1);
                break;
        }
    }
}

[Serializable]
public class GameDatas
{
    public DateInGame Date;
}


[Serializable]
public class DateInGame
{
    public int Day;
    public int Hour;
    public int Minute;

    public DateInGame() { }

    public DateInGame(int _day, int _hour, int _min)
    {
        Day = _day;
        Hour = _hour;
        Minute = _min;
    }

    public DateInGame(DateInGame date)
    {
        Day = date.Day;
        Hour = date.Hour;
        Minute = date.Minute;
    }

    public override string ToString()
    {
        return $"{Day} : {Hour} : {Minute}";
    }

    public DateInGame ElapsedTime()
    {
        DateInGame newDate = new DateInGame(0, 0, 0);

        int correctionHour = 0;
        int correctionMinute = 0;

        if (GameManager.Instance.DayInGame > Day)
        {
            newDate.Day = GameManager.Instance.DayInGame - Day;
            correctionHour += 24;
        }

        correctionHour += GameManager.Instance.HourInGame;
        if (correctionHour > Hour)
        {
            newDate.Hour = correctionHour - Hour;
            correctionMinute += 60;
        }

        correctionMinute += GameManager.Instance.MinuteInGame;
        if (correctionMinute > Minute)
        {
            newDate.Minute = correctionMinute - Minute;
        }

        return newDate;
    }
}
