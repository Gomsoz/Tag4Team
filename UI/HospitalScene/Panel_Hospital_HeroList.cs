using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Panel_Hospital_HeroList : MonoBehaviour
{
    [SerializeField]
    private GameObject pfb_heroListItem;
    [SerializeField]
    private Transform itemParent;
    List<UISet_HealingService_HeroItem> itemList = new();

    [SerializeField]
    private RectTransform rct_itemGroup;
    [SerializeField]
    private RectTransform rct_content;

    private Vector2 additionalItemSize = new Vector2(0,120);

    public void CreateHeroList(HeroInfo _heroInfo)
    {
        GameObject go = Instantiate(pfb_heroListItem, itemParent);
        UISet_HealingService_HeroItem component = go.GetComponent<UISet_HealingService_HeroItem>();
        component.SetHeroData(_heroInfo);
        itemList.Add(component);

        //rct_itemGroup.sizeDelta += additionalItemSize;
        rct_content.sizeDelta += additionalItemSize;
    }

    public void Register(int idx)
    {
        itemList[idx].Register();
    }

    public void Release(int idx)
    {
        itemList[idx].Release();
    }
}
