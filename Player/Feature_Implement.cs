using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Feature_OverflowingHp : Feature_Base
{
    public override void SetData()
    {
        name = "OverflowingHp";
        point = 3;
        description = "체력 10% 증가";
    }

    public override void Apply(HeroInfo data)
    {
        base.Apply(data);

        data.Stat.PercentageHp += 0.1f;
    }

    public override void Revert(HeroInfo data)
    {
        base.Revert(data);

        data.Stat.PercentageHp -= 0.1f;
    }
}

public class Feature_OverflowingMp : Feature_Base
{
    public override void SetData()
    {
        name = "OverwhelmingHp";
        point = 3;
        description = "마나 20% 감소 체력 30% 증가";
    }

    public override void Apply(HeroInfo data)
    {
        base.Apply(data);

        Debug.Log(data.Herodata.HeroCode);

        data.Stat.PercentageMp -= 0.2f;
        data.Stat.PercentageHp += 0.3f;
    }

    public override void Revert(HeroInfo data)
    {
        base.Revert(data);

        data.Stat.PercentageMp += 0.2f;
        data.Stat.PercentageHp -= 0.3f;
    }
}

public class Feature_OverwhelmingHp : Feature_Base
{
    public override void SetData()
    {
        name = "OverflowingMp";
        point = 5;
        description = "마나 10% 증가";
    }

    public override void Apply(HeroInfo data)
    {
        base.Apply(data);

        data.Stat.PercentageMp += 0.1f;
    }

    public override void Revert(HeroInfo data)
    {
        base.Revert(data);

        data.Stat.PercentageMp -= 0.1f;
    }
}

public class Feature_OverwhelmingMp : Feature_Base
{
    public override void SetData()
    {
        name = "OverwhelmingMp";
        point = 5;
        description = "체력 20% 감소 마나 30% 증가";
    }

    public override void Apply(HeroInfo data)
    {
        base.Apply(data);

        data.Stat.PercentageHp -= 0.2f;
        data.Stat.PercentageMp += 0.3f;
    }

    public override void Revert(HeroInfo data)
    {
        base.Revert(data);

        data.Stat.PercentageHp += 0.2f;
        data.Stat.PercentageMp -= 0.3f;
    }
}
