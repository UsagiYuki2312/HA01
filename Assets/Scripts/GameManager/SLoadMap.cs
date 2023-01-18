using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Pixelplacement;
using TMPro;

public class SLoadMap : MonoBehaviour
{
    private Coroutine coroutine;
    private static readonly object delayLoading = new WaitForSeconds(3f);

    public void LoadMap()
    {
        MonoUtility.SetTimeScale(1, this);
        Loading();
    }

    public void Loading()
    {
        if (coroutine != null)
        {
            StopCoroutine(coroutine);
        }

        coroutine = StartCoroutine(RunLoading());
    }

    IEnumerator RunLoading()
    {
        yield return delayLoading;
        AsyncOperation operation = SceneManager.LoadSceneAsync("Map1");
    }
}
