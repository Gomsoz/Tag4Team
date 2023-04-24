using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class Panel_SelectHero : MonoBehaviour
{
    [SerializeField]
    private UI_SelectHero[] selectHeroItems;

    private Action<HeroBehavior> callback = null;

    public void Start()
    {
        int idx = 0;
        foreach (var item in selectHeroItems)
        {
            item.SetHeroImage(PlayerController.Instance.GetHero(idx));
            idx++;
        }
    }

    public void OpenPanel(Action<HeroBehavior> _callback)
    {
        callback = _callback;
        gameObject.SetActive(true);

        Managers.Instance.Input.DirectionKeyPublisher -= DirectionKeyListener;
        Managers.Instance.Input.DirectionKeyPublisher += DirectionKeyListener;
    }

    private void DirectionKeyListener(Direction direction)
    {
        callback.Invoke(PlayerController.Instance.GetHero((int)direction - 1));
        ClosePanel();
    }

    public void ClosePanel()
    {
        callback = null;
        Managers.Instance.Input.DirectionKeyPublisher -= DirectionKeyListener;
        gameObject.SetActive(false);
    } 
}
