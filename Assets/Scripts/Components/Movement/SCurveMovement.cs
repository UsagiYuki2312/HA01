using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SCurveMovement : MonoBehaviour
{
    public UnityAction OnTargetReached;
    public Transform target;
    private Quaternion lookRotation;
    private Vector3 direction;
    private float distance;
    public float delay;
    public float rotateSpeed;
    public float speed;

    protected virtual void Update()
    {
        direction = target.position - transform.position;
        distance = Vector3.SqrMagnitude(direction);
        if (distance > 1.5f)
        {
            lookRotation = Quaternion.LookRotation(direction, Vector3.up);
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, rotateSpeed * Time.deltaTime);
            transform.Translate(transform.forward * speed * Time.deltaTime, Space.World);
        }
        else transform.forward = direction;
        transform.Translate(transform.forward * speed * Time.deltaTime, Space.World);

        if (Vector3.SqrMagnitude(direction) <= 0.1f)
        {
            OnTargetReached?.Invoke();
            gameObject.SetActive(false);
        }
    }
}
