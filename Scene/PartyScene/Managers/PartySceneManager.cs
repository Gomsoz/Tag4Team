using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PartySceneManager : MonoBehaviour
{
    private static PartySceneManager instance = null;

    public static PartySceneManager Instance { get => instance; }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }
}
