using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    public RectTransform rect;
    public Camera cam;

    [ContextMenu("TEST")]
    public void TEST()
    {
        
        transform.position = cam.ScreenToWorldPoint(rect.position);
    }
}
