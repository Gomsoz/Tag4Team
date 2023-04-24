using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public enum CharacterBuffsOnPlaying
{
    IncreaseAttackDamgeForFixedValue,
    IncreaseAttackDamgeForPercentageValue,
    IncreaseMaxHpForFixedValue,
    IncreaseMaxHpForPercentageValue,
    IncreaseMaxMpForFixedValue,
    IncreaseMaxMpForPercentageValue,
}

public class BuffOnPlaying
{
    protected string buffName;
    public string BuffName { get { return buffName; } }
    protected CharacterBehavior appliedCharacter;
    protected float buffDuration;
    public float BuffDuration { get { return buffDuration; } }

    protected CharacterBehavior buffOwner;
    protected CharacterBehavior target;
    protected Action endBuffCallback;

    public virtual void Init(int duration, string _buffName, CharacterBehavior _buffOwner, CharacterBehavior _target = null, Action _endBuffCallback = null)
    {
        buffDuration = duration;
        buffName = _buffName;
        buffOwner = _buffOwner;
        target = _target;
        endBuffCallback = _endBuffCallback;
    }

    public virtual void Init(int duration, string _buffName, CharacterBehavior _buffOwner, Action _endBuffCallback)
    {
        Init(duration, _buffName, _buffOwner, null, _endBuffCallback);
    }

    public virtual void Apply()
    {
    }

    public virtual void Revert()
    {
        if(endBuffCallback != null)
            endBuffCallback();
    }

    public virtual void Revert(CharacterBehavior owner)
    {
        buffOwner = owner;
        Revert();
    }
}
