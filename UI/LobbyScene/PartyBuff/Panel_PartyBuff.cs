using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public enum PartyBuffName
{
    Hp,
    Mp,
    Dmg,
    Amor,
}
public class Panel_PartyBuff : MonoBehaviour
{
    private PartyBuffData partyBuffData;
    public PartyBuffData PartyBuffData { get { return partyBuffData; } }

    [SerializeField]
    private TMP_Text text_buffPoint;

    [SerializeField]
    private List<UISet_PartyBuff> partyBuffUIList = new();
    private int curBuffPoint;
    private int maxBuffPoint;
    public bool isAdjustableBuffPoint
    {
        get
        {
            if (curBuffPoint == 0 || curBuffPoint > maxBuffPoint)
                return false;

            return true;
        }
    }

    public Action<PartyBuff> AdjustedPartyBuffPublisher;

    public void LoadPartyBuffData(PartyBuffData _partyBuffData)
    {
        partyBuffData = _partyBuffData;

        curBuffPoint = partyBuffData.Point;
        maxBuffPoint = partyBuffData.DefaultPoint;

        partyBuffUIList[(int)PartyBuffName.Hp].SetCounting(partyBuffData.HpLevel);
        partyBuffUIList[(int)PartyBuffName.Mp].SetCounting(partyBuffData.MpLevel);
        partyBuffUIList[(int)PartyBuffName.Dmg].SetCounting(partyBuffData.DmgLevel);
        partyBuffUIList[(int)PartyBuffName.Amor].SetCounting(partyBuffData.AmorLevel);

        text_buffPoint.text = curBuffPoint.ToString();

        PartyBuff buff = new PartyBuff()
        {
            HpPoint = partyBuffData.HpLevel,
            MpPoint = partyBuffData.MpLevel,
            DmgPoint = partyBuffData.DmgLevel,
            AmorPoint = partyBuffData.AmorLevel,

            IncreasingHpPerPoint = partyBuffData.HpPerLevel,
            IncreasingMpPerPoint = partyBuffData.MpPerLevel,
            IncreasingDmgPerPoint = partyBuffData.DmgPerLevel,
            IncreasingAmorPerPoint = partyBuffData.AmorPerLevel,
        };

        HeroDataManager.Instance.ChangedPartyBuffListener(buff);
    }

    public void AdjustBuffPoint(PartyBuffName buffName, int value)
    {
        switch (buffName)
        {
            case PartyBuffName.Hp:
                partyBuffData.HpLevel += value;
                break;
            case PartyBuffName.Mp:
                partyBuffData.MpLevel += value;
                break;
            case PartyBuffName.Dmg:
                partyBuffData.DmgLevel += value;
                break;
            case PartyBuffName.Amor:
                partyBuffData.AmorLevel += value;
                break;
        }

        curBuffPoint -= value;
        partyBuffData.Point= curBuffPoint;
        text_buffPoint.text = curBuffPoint.ToString();

        PartyBuff buff = new PartyBuff()
        {
            HpPoint = partyBuffData.HpLevel,
            MpPoint = partyBuffData.MpLevel,
            DmgPoint = partyBuffData.DmgLevel,
            AmorPoint = partyBuffData.AmorLevel,

            IncreasingHpPerPoint = partyBuffData.HpPerLevel,
            IncreasingMpPerPoint = partyBuffData.MpPerLevel,
            IncreasingDmgPerPoint = partyBuffData.DmgPerLevel,
            IncreasingAmorPerPoint = partyBuffData.AmorPerLevel,
        };

        HeroDataManager.Instance.ChangedPartyBuffListener(buff);
        AdjustedPartyBuffPublisher?.Invoke(buff);
    }
}

public class PartyBuff
{
    public float HpPoint = 0;
    public float IncreasingHpPerPoint = 0;
    public float PercentageHp { get => HpPoint * IncreasingHpPerPoint; }

    public float MpPoint = 0;
    public float IncreasingMpPerPoint = 0;
    public float PercentageMp { get => MpPoint * IncreasingMpPerPoint; }

    public float DmgPoint = 0;
    public float IncreasingDmgPerPoint = 0;
    public float PercentageDmg { get => DmgPoint * IncreasingDmgPerPoint; }

    public float AmorPoint = 0;
    public float IncreasingAmorPerPoint = 0;
    public float PercentageAmor { get => AmorPoint * IncreasingAmorPerPoint; }

    public static PartyBuff operator -(PartyBuff changedBuff, PartyBuff appliedBuff)
    {
        PartyBuff result = new PartyBuff()
        {
            HpPoint = changedBuff.HpPoint - appliedBuff.HpPoint,
            MpPoint = changedBuff.MpPoint - appliedBuff.MpPoint,
            DmgPoint = changedBuff.DmgPoint - appliedBuff.DmgPoint,
            AmorPoint = changedBuff.AmorPoint - appliedBuff.AmorPoint,

            IncreasingHpPerPoint = changedBuff.IncreasingHpPerPoint,
            IncreasingMpPerPoint = changedBuff.IncreasingMpPerPoint,
            IncreasingDmgPerPoint = changedBuff.IncreasingDmgPerPoint,
            IncreasingAmorPerPoint = changedBuff.IncreasingAmorPerPoint,
        };

        return result;
    }
}
