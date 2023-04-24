using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectLocation : MonoBehaviour
{
    private int[] fixedMoveX = new int[5] { 0, 0, 1, 0, -1 };
    private int[] fixedMoveY = new int[5] { 0, 1, 0, -1, 0 };
    private float moveSpeed = 0.05f;

    private Action<Vector3> callbackPosition = null;

    public void StartLocationSelecter(Action<Vector3> callback)
    {
        gameObject.SetActive(true);
        transform.localPosition = Vector3.zero;
        callbackPosition = callback;    

        Managers.Instance.Input.DirectionKeyPublisher -= DirectionKeyListener;
        Managers.Instance.Input.DirectionKeyPublisher += DirectionKeyListener;

        Managers.Instance.Input.DicisionKeyPublisher -= SelectListener;
        Managers.Instance.Input.DicisionKeyPublisher += SelectListener;
    }

    private void DirectionKeyListener(Direction direction)
    {
        int moveX = fixedMoveX[(int)direction];
        int moveY = fixedMoveY[(int)direction];

        transform.Translate(new Vector3(moveX, moveY, 0) * moveSpeed);
    }

    public void SelectListener()
    {
        Managers.Instance.Input.DirectionKeyPublisher -= DirectionKeyListener;
        Managers.Instance.Input.DicisionKeyPublisher -= SelectListener;

        gameObject.SetActive(false);

        callbackPosition(transform.position);

        callbackPosition = null;
        transform.localPosition = Vector3.zero;
    }
}
