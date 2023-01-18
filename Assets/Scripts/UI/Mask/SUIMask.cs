using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pixelplacement;
public class SUIMask : MonoBehaviour
{
    public RectTransform maskRect;

    public void TriggerCloseAnimation(float duration)
    {
        Tween.LocalScale(maskRect, Vector3.zero, duration, 0, Tween.EaseInOutStrong);
    }

    public void TriggerOpenAnimation(float duration)
    {
        Tween.LocalScale(maskRect, Vector3.one * 50, duration, 0, Tween.EaseInOutStrong);
    }
}
