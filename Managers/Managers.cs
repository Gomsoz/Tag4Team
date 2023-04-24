using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Managers : MonoBehaviour
{
    private static Managers instance = null;

    public static Managers Instance { get => instance; }

    [field: SerializeField]
    public TagManager Tag { get; private set; }

    [field: SerializeField]
    public BattleManager Battle { get; private set; }

    [field:SerializeField]
    public InputManager Input { get; private set; }

    [field: SerializeField]
    public UIManager UI { get; private set; }

    private void Awake()
    {
        if(instance == null)
            instance = this;    
    }
}
