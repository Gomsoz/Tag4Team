using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Group_HeroInventory : MonoBehaviour
{
    [SerializeField]
    private GameObject ItemPrefab;

    private Item_HeroInventory curItem;
    public HeroJobs SelectedHero 
    { 
        get
        {
            if (curItem == null)
                return HeroJobs.None;

            return curItem.HeroJob;
        }
    }

    private void Start()
    {
        CreateHeroInventoryItem(HeroJobs.Tanker);
        CreateHeroInventoryItem(HeroJobs.Healer);
        CreateHeroInventoryItem(HeroJobs.SwordMaster);
        CreateHeroInventoryItem(HeroJobs.Mage);
    }

    public void CreateHeroInventoryItem(HeroJobs job)
    {
        GameObject go = Instantiate(ItemPrefab, transform);
        go.GetComponent<Item_HeroInventory>().Inititate(
            ChangeSelectedItem,
            job,
            GetComponent<ToggleGroup>()
            );
    }

    public void ReleaseSelectedHero()
    {
        ChangeSelectedItem(curItem, false);
    }

    public void ChangeSelectedItem(Item_HeroInventory target, bool isSelect)
    {
        if (isSelect)
        {
            curItem?.OffToggle();
            curItem = target;
        }
        else
        {
            curItem.OffToggle();
            curItem = null;
        }
    }
}
