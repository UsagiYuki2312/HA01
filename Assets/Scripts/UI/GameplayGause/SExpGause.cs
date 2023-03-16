using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SExpGause : MonoBehaviour
{
    public Slider expBar;
    public TMP_Text levelDisplay;

    private void Reset()
    {
        expBar = GetComponent<Slider>();
        levelDisplay = GetComponentInChildren<TMP_Text>();
    }

    public void SetupExpBar(float maxExp)
    {
        expBar.maxValue = maxExp;
    }

    public void SetExp(float exp)
    {
        expBar.value = exp;
    }

    public void SetLevel(int level)
    {
        levelDisplay.SetText("{0}", level);
    }
}
