using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UISet_PartyBuff : MonoBehaviour
{
    [SerializeField]
    private Button btn_up;
    [SerializeField]
    private Button btn_down;

    [SerializeField]
    private List<Image> progressBar = new List<Image>();
    private int buffCounting = 0;
    private int maxBuffCounting = 20;

    [SerializeField]
    private PartyBuffName buffName;

    private void Start()
    {
        btn_up.onClick.AddListener(CountUp);
        btn_down.onClick.AddListener(CountDown);
    }

    public void SetCounting(int level)
    {
        buffCounting = level;

        for(int i = 0; i < level; i++)
        {
            progressBar[i].color = Color.red;
        }
    }

    public void CountUp()
    {
        if (PartyScene.Instance.Panel_PartyBuff.isAdjustableBuffPoint == false)
            return;

        if (buffCounting >= 20)
            return;

        progressBar[buffCounting].color = Color.red;
        buffCounting++;
        PartyScene.Instance.Panel_PartyBuff.AdjustBuffPoint(buffName, 1);
    }

    public void CountDown()
    {
        if (buffCounting <= 0)
            return;

        buffCounting--;
        progressBar[buffCounting].color = Color.white;
        PartyScene.Instance.Panel_PartyBuff.AdjustBuffPoint(buffName, -1);
    }

    public void ClearCouting()
    {
        for(int i = 0; i < buffCounting; i++)
        {
            progressBar[buffCounting].color = Color.white;
        }

        PartyScene.Instance.Panel_PartyBuff.AdjustBuffPoint(buffName, buffCounting);
        buffCounting = 0;
    }
}
