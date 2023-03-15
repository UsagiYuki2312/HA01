using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SBullet : MonoBehaviour
{
    public float speed; Vector3 dir;
    private Vector3 startPosition;
    public SDamageTrigger damageTrigger;
    protected virtual void Awake()
    {
        speed = 10;
        startPosition = transform.position;
        damageTrigger = GetComponent<SDamageTrigger>();
        //damageTrigger.OnAlienTouched = OnAlienTouched;
    }
    protected virtual void Update()
    {
        transform.Translate(transform.up * speed * Time.deltaTime, Space.World);
        TriggerAutoHide();
    }
    public void TriggerAutoHide()
    {
        float distance = Vector3.Distance(gameObject.transform.position, startPosition);
        if (distance > 5)
        {
            gameObject.SetActive(false);
        }
    }

    private void OnEnable()
    {
        startPosition = transform.position;
    }

    protected virtual void OnAlienTouched()
    {
        gameObject.SetActive(false);
    }
}
