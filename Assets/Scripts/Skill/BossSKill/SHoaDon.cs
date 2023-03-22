using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SHoaDon : MonoBehaviour
{
    public float speed; Vector3 dir;
    private Vector3 startPosition;
    public SDamagePlayer damagePlayer;
    private float time = 0;
    protected virtual void Awake()
    {
        speed = 10;
    }
    protected virtual void Update()
    {
        time += Time.deltaTime;
        if (time >= 1.5f)
        {
            gameObject.SetActive(false);
            time = 0;
        }

    }
    private void OnEnable()
    {
        startPosition = transform.position;
    }
}
