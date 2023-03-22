using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFireBall : MonoBehaviour
{
    public float speed; Vector3 dir;
    private Vector3 startPosition;
    public SDamagePlayer damagePlayer;
    protected virtual void Awake()
    {
    }
    protected virtual void Update()
    {
        transform.Translate(transform.right * speed * Time.deltaTime, Space.World);
        TriggerAutoHide();
    }
    public void TriggerAutoHide()
    {
        float distance = Vector3.Distance(gameObject.transform.position, startPosition);
        if (distance > 10)
        {
            gameObject.SetActive(false);
        }
    }
    private void OnEnable()
    {
        startPosition = transform.position;
    }
}
