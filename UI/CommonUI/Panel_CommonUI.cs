using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Panel_CommonUI : MonoBehaviour
{
    [SerializeField]
    private Button btn_back;

    [SerializeField]
    private Button btn_setting;

    [SerializeField]
    private Toggle tgl_inventory;

    [SerializeField]
    private TMP_Text txt_day;

    [SerializeField]
    private TMP_Text txt_hour;

    [SerializeField]
    private TMP_Text txt_minute;

    [SerializeField]
    private TMP_Text txt_coinAmount;

    private void Start()
    {
        TimeListener();

        btn_back.onClick.AddListener(OnClickBackBtn);
        btn_setting.onClick.AddListener(OnClickSettingBtn);
        tgl_inventory.onValueChanged.AddListener(ToggleInventoryBtn);

        GameManager.Instance.tenMinutesEvent += TimeListener;

        txt_coinAmount.text = Inventory.Instance.Coins.ToString();
    }

    public void OnClickBackBtn()
    {

    }

    public void OnClickSettingBtn()
    {

    }

    public void ToggleInventoryBtn(bool isOn)
    {
        Inventory.Instance.ToggleInventory(isOn);
    }

    public void TimeListener()
    {
        txt_day.text = $"{GameManager.Instance.CurTime.Day.ToString()} ¿œ";
        txt_hour.text = GameManager.Instance.CurTime.Hour.ToString();
        txt_minute.text = GameManager.Instance.CurTime.Minute.ToString();
    }
}
