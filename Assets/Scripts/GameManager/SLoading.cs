using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Pixelplacement;
using TMPro;
using UnityEngine.Video;

public class SLoading : MonoBehaviour
{
    private Coroutine coroutine;
    private static readonly object delayLoading = new WaitForSeconds(3f);

    private void Awake()
    {
        this.SetTimeScale(1);
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
        AsyncOperation operation = SceneManager.LoadSceneAsync("Level1");
    }
}
