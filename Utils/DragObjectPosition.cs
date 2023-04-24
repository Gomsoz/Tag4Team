using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragObjectPosition : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        transform.position = Input.mousePosition;
    }
}
