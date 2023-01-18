using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SBullet : MonoBehaviour
{
    public float speed; Vector3 dir;
    protected virtual void Awake()
    {
        speed = 10;
    }
    protected virtual void Update()
    {
        transform.Translate(transform.up * speed * Time.deltaTime, Space.World);
        TriggerAutoHide();
    }
    public void TriggerAutoHide()
    {
        float distance = Vector3.Distance(gameObject.transform.position, SGameInstance.Instance.player.transform.position);
        if (distance > 5)
        {
            gameObject.SetActive(false);
        }
    }

}
