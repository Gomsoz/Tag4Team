using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Group_LobbyButton : MonoBehaviour
{
    public void OnClickLoadSceneBtn(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}
