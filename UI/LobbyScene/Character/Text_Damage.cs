using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Text_Damage : MonoBehaviour
{
    [SerializeField]
    private TMP_Text txt_damage;

    private float showTextTime;
    private bool isShowing;

    public void SetDamage(int value)
    {
        txt_damage.text = value.ToString();
        showTextTime = 0.5f;

        if(isShowing == false)
        {
            txt_damage.gameObject.SetActive(true);
            isShowing = true;
            StartCoroutine(TextTimer());
        }
    }

    private IEnumerator TextTimer()
    {
        while (showTextTime >= 0)
        {
            showTextTime -= Time.deltaTime;
            yield return null;
        }

        txt_damage.gameObject.SetActive(false);
        isShowing = false;
    }
}
