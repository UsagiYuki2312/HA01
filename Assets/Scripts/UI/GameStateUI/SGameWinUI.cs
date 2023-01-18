using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using Pixelplacement;
using TMPro;
public class SGameWinUI : SGameUI
{
    public Button noThank;
    //public SAnimAppearance[] appearedObjects;
    public UnityAction OnReturnClick;

    private void Awake()
    {
        //appearedObjects = GetComponentsInChildren<SAnimAppearance>();
        //for (int i = 0; i < appearedObjects.Length; i++) appearedObjects[i].Hide();
    }

    private void Start()
    {
        StartCoroutine(DisplayObjects());
    }

    public void ReturnToMain()
    {
        OnReturnClick?.Invoke();
    }

    private IEnumerator DisplayObjects()
    {
        //for (int i = 0; i < appearedObjects.Length; i++)
        //{
        //    appearedObjects[i].Display();
        //   if (i < appearedObjects.Length - 1) yield return new WaitForSeconds(0.3f);
        //}
        yield return new WaitForSeconds(1f);
        noThank.gameObject.SetActive(true);
    }
}
