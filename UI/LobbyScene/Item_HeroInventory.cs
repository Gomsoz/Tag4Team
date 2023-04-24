using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class Item_HeroInventory : MonoBehaviour
{
    [SerializeField]
    private Image img_heroImage;
    public Sprite HeroImage { get; private set; }

    public HeroJobs HeroJob { get; private set; }

    [SerializeField]
    private GameObject BoarderObject;

    [SerializeField]
    private Toggle toggle;
    private bool isSelect = false;

    private Action<Item_HeroInventory, bool> cahngeToggleValueCallback;

    public void Inititate(Action<Item_HeroInventory, bool> callback, HeroJobs job, ToggleGroup gorup)
    {
        HeroJob = job;
        cahngeToggleValueCallback = callback;
        toggle.onValueChanged.AddListener(ChangedToggleValue);
        SetHeroImage();
    }

    public void SetHeroImage()
    {
        img_heroImage.sprite = HeroDataManager.Instance.GetHerodata(HeroJob).CharacterImage;
    }

    public void ChangedToggleValue(bool isOn)
    {
        BoarderObject.SetActive(isOn);
        cahngeToggleValueCallback(this, isOn);
    }

    public void OffToggle()
    {
        toggle.isOn = false;
        BoarderObject.SetActive(false);
    }
}
