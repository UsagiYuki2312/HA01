using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SBaseDpsReceiver : MonoBehaviourCore
{
    public DamageReceiver damageReceiver;
    protected virtual string ZoneTag => "default";
    protected virtual float DamageInterval => 0.2f;
    protected float power;
    private float inzoneTime;

    protected virtual void Reset() {
        
    }

    protected virtual void Awake() {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(ZoneTag)) OnZoneEnter(other);
    }

    private void OnTriggerStay(Collider other)
    {
        if (Time.time >= inzoneTime && other.CompareTag(ZoneTag))
        {
            OnZoneStay();
            inzoneTime = Time.time + DamageInterval;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(ZoneTag)) OnZoneExit();
    }

    protected virtual void OnZoneEnter(Collider other)
    {

    }

    protected virtual void OnZoneStay()
    {
        damageReceiver.TakeDamage(power);
    }

    protected virtual void OnZoneExit()
    {

    }
}
