using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentsInteraction : MonoBehaviour
{

    public void OnMouseEnter()
    {
        transform.localScale = Vector3.one * 1.2f;
    }

    public void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(0))
        {
            LobbyScene.Instance.LoadScene(transform.name);
        }
    }

    public void OnMouseExit()
    {
        transform.localScale = Vector3.one;
    }

    public void OnClick()
    {
        Debug.Log("Click");
    }
}
