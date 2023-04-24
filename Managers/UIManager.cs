using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [field: SerializeField]
    public Panel_HeroState Panel_HeroState { get; private set; }

    [field: SerializeField]
    public Panel_Control Panel_Control { get; private set; }

    [field: SerializeField]
    public Panel_MobState Panel_MobState { get; private set; }


    [field: SerializeField]
    public Panel_Result Panel_Result { get; private set; }
}
