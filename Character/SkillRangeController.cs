using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillRangeController : MonoBehaviour
{
    [SerializeField]
    private RectTransform fill;

    public void StartFill(float time, Action callback)
    {
        gameObject.SetActive(true);
        fill.localScale = Vector3.zero;
        StartCoroutine(Fill(time, callback));
    }

    private IEnumerator Fill(float time, Action callback)
    {
        float leftTime = time;
        while(leftTime >= 0)
        {
            float value = Mathf.Lerp(1, 0, leftTime / time);
            fill.localScale = new Vector3(value, value, 1);
            leftTime -= Time.deltaTime;
            yield return null;
        }

        gameObject.SetActive(false);
        callback();
    }
}
