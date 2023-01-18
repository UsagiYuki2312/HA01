using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SCanvasAutoScale : MonoBehaviour
{
    public CanvasScaler canvasScaler;

    private void Reset()
    {
        canvasScaler = GetComponent<CanvasScaler>();
    }

    private void OnEnable()
    {
        if ((float)Screen.width / (float)Screen.height >= 0.6f)
            canvasScaler.matchWidthOrHeight = 1;
        else canvasScaler.matchWidthOrHeight = 0;
    }
}
