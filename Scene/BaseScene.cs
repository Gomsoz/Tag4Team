using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BaseScene : MonoBehaviour
{
    public Action SaveDataPublisher = null;
    public string SceneName { get; protected set; }

    private void Start()
    {
        InitScene();
    }

    protected virtual void InitScene()
    {
        GameManager.Instance.CurScene = this;

        LoadSceneData();
    }

    public void LoadPreScene()
    {
        SaveSceneData();

        SceneManager.LoadScene(GameManager.Instance.PreSceneName);
    }

    public virtual void LoadSceneData()
    {

    }

    public virtual void SaveSceneData()
    {
        HeroDataManager.Instance.SaveHeroDatas();
        GameManager.Instance.SaveGameData();

        // SaveDataPublisher?.Invoke();
    }

    private void OnApplicationQuit()
    {
        SaveSceneData();
        SaveDataPublisher?.Invoke();
    }
}
