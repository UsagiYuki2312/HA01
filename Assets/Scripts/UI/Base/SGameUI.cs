using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SGameUI : MonoBehaviourCore
{
    public virtual void Display()
    {
        gameObject.SetActive(true);
    }

    public virtual void Display(float delay)
    {
        DelayCall(() => { gameObject.SetActive(true); }, new WaitForSeconds(delay));
    }

    public virtual void Hide()
    {
        gameObject.SetActive(false);
    }

    public virtual void Destroy()
    {
        Destroy(gameObject);
    }
}
