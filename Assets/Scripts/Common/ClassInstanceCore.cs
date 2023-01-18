using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ClassInstanceCore
{
    public SDataController DataController => SDataController.Instance;
    public SDataFactory DataFactory => SDataFactory.Instance;
    public SGameInstance GameInstance => SGameInstance.Instance;
    public SCoroutineController CoroutineController => SCoroutineController.Instance;

    public Coroutine StartCoroutine(IEnumerator routine)
    {
        Coroutine coroutine = CoroutineController.StartCoroutine(routine);
        return coroutine;
    }

    public void StopCoroutine(Coroutine routine)
    {
        CoroutineController.StopCoroutine(routine);
    }

    public void RestartCoroutine(ref Coroutine coroutine, IEnumerator routine)
    {
        CoroutineController.RestartCoroutine(ref coroutine, routine);
    }
}



