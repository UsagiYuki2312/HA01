using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SBossHealthBar : MonoBehaviour
{
    public Slider healthBar;

    private void Reset()
    {
        healthBar = GetComponent<Slider>();
    }

    public void SetMaxValue(float value)
    {
        healthBar.maxValue = value;
    }

    public void SetValue(float value)
    {
        healthBar.value = value;
    }
}