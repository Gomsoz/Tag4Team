using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LobbyScene : BaseScene
{
    private static LobbyScene instance;
    public static LobbyScene Instance { get { return instance; } }

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

        SceneName = "LobbyScene";
        GameManager.Instance.SetExitBtn(false);
    }

    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene($"{sceneName}Scene");
    }
}
