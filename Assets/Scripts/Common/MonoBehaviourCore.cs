using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class MonoBehaviourCore : MonoBehaviour
{
    public SDataController DataController => SDataController.Instance;
    public SDataFactory DataFactory => SDataFactory.Instance;
    public SGameInstance GameInstance => SGameInstance.Instance;
    public MessageManager MessageManager => MessageManager.Instance;
    public void RestartCoroutine(ref Coroutine coroutine, IEnumerator routine)
    {
        if (coroutine != null) StopCoroutine(coroutine);
        coroutine = StartCoroutine(routine);
    }

    public void DelayCallAction(Action action, object delay)
    {
        StartCoroutine(DelayAction(action, delay));
    }

    public void DelayCall(UnityAction action, object delay)
    {
        StartCoroutine(DelayAction(action, delay));
    }

    public virtual IEnumerator DelayAction(Action action, object delay)
    {
        yield return delay;
        action();
    }

    public virtual IEnumerator DelayAction(UnityAction action, object delay)
    {
        yield return delay;
        action();
    }
}




