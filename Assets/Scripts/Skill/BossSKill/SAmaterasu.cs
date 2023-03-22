using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SAmaterasu : MonoBehaviour
{
    private float time = 0;

    protected virtual void Update()
    {
        time += Time.deltaTime;
        transform.position = SGameInstance.Instance.player.transform.position;
        if (time >= 3f)
        {
            gameObject.SetActive(false);
            time = 0;
        }

    }

    private void OnEnable()
    {
    }
}
