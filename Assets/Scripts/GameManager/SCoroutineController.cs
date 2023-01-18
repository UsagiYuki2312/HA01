using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Pixelplacement;

public class SCoroutineController : MonoBehaviourCore
{
    private static SCoroutineController instance;
    public static SCoroutineController Instance
    {
        get { return instance; }
    }

    private void Awake()
    {
        if (instance != null && instance != this) Destroy(gameObject);
        else instance = this;
    }

    public Coroutine StartACoroutine(IEnumerator routine)
    {
        Coroutine coroutine = null;
        RestartCoroutine(ref coroutine, routine);
        return coroutine;
    }
}
