using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BuffBase
{
    protected string buffName;
    public string BuffName { get => buffName; }
    public abstract void Apply();
    public abstract void Revert();
}
