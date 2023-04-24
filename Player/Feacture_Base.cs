using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Feature_Base
{
    protected string name;
    public string Name { get { return name; } }

    protected string description;
    public string Description { get { return description; } }

    protected int point;
    public int Point { get { return point; } }

    public virtual void SetData()
    {
        // TODO Ư�� �����͸� �ҷ���
    }

    public virtual void Apply(HeroInfo data)
    {
    }

    public virtual void Revert(HeroInfo data)
    {
    }
}
