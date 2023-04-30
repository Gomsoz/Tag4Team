using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Panel_CommonPopup : MonoBehaviour
{
    [SerializeField]
    private TMP_Text txt_contents;

    [SerializeField]
    private Button btn_apply;

    [SerializeField]
    private Button btn_cancel;

    [SerializeField]
    private Button btn_done;

    private Action applyCabllback;
    private Action cancelCallback;
    private Action doneCallback;

    private void Start()
    {
        btn_apply.onClick.AddListener(ClickApplyBtn);
        btn_cancel.onClick.AddListener(ClickCancelBtn);
        btn_done.onClick.AddListener(ClickDoneBtn);
    }

    public void OpenApplyCancelPopup(string contents, Action _applyCallback, Action _cancelCallback)
    {
        txt_contents.text = contents;

        applyCabllback = _applyCallback;
        cancelCallback = _cancelCallback;

        btn_apply.gameObject.SetActive(true);
        btn_cancel.gameObject.SetActive(true);
        btn_done.gameObject.SetActive(false);
    }

    public void OpenDonePopup(string contents, Action _doneCallback)
    {
        txt_contents.text = contents;

        doneCallback = _doneCallback;

        btn_apply.gameObject.SetActive(false);
        btn_cancel.gameObject.SetActive(false);
        btn_done.gameObject.SetActive(true);
    }

    public void ClickApplyBtn()
    {
        if (applyCabllback != null)
            applyCabllback();

        GameManager.Instance.GamePause();
        Destroy(gameObject);
    }

    public void ClickCancelBtn()
    {
        if (cancelCallback != null)
            cancelCallback();

        GameManager.Instance.GamePause();
        Destroy(gameObject);
    }

    public void ClickDoneBtn()
    {
        if (doneCallback != null)
            doneCallback();

        GameManager.Instance.GamePause();
        Destroy(gameObject);
    }
}
