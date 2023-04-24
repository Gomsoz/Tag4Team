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
    private TMP_Text txt_day;

    [SerializeField]
    private TMP_Text txt_hour;

    [SerializeField]
    private TMP_Text txt_minute;

    private void Start()
    {
        TimeListener();

        btn_back.onClick.AddListener(OnClickBackBtn);
        btn_setting.onClick.AddListener(OnClickSettingBtn);

        GameManager.Instance.tenMinutesEvent += TimeListener;
    }

    public void OnClickBackBtn()
    {

    }

    public void OnClickSettingBtn()
    {

    }

    public void TimeListener()
    {
        txt_day.text = $"{GameManager.Instance.CurTime.Day.ToString()} ¿œ";
        txt_hour.text = GameManager.Instance.CurTime.Hour.ToString();
        txt_minute.text = GameManager.Instance.CurTime.Minute.ToString();
    }
}
