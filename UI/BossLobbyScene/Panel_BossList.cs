using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using UnityEngine;

public class Panel_BossList : MonoBehaviour
{
    private Vector2 contentsMargin = new Vector2(10, 0);
    private Vector2 contentsWidth = new Vector2(300, 0);

    [SerializeField]
    private RectTransform contentsParent;

    [SerializeField]
    private GameObject prb_bossListItem;

    private void Start()
    {
        int bossCnt = BossDataManager.Instance.BossDatas.Keys.Count;

        contentsParent.sizeDelta += (contentsWidth * bossCnt) + (contentsMargin * (bossCnt - 1));

        foreach(var item in BossDataManager.Instance.BossDatas)
        {
            CreateBossListItem(item.Value[0]);
        } 
    }

    private void CreateBossListItem(BossData _bossData)
    {  
        GameObject go = Instantiate(prb_bossListItem, contentsParent);

        UISet_BossListItem component = go.GetComponent<UISet_BossListItem>();

        component.SetItem(_bossData);
    }
}
