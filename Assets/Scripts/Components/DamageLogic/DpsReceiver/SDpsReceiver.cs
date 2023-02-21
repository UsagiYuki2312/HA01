using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SDpsReceiver : MonoBehaviour
{
    public DamageReceiver damageReceiver;
    public UnityAction OnSlowdownTriggered;
    public UnityAction OnSlowdownFinished;
    // private static SMagneticFX magneticFX;
    // private float magneticFieldTime = 0;
    // private float burningFieldTime = 0;

    // private void OnTriggerEnter(Collider other)
    // {
    //     if (other.CompareTag(Tag.MAGNETIC_FIELD))
    //     {
    //         if (magneticFX == null) magneticFX = other.GetComponent<SMagneticFX>();
    //         if (magneticFX.isSlowDownTriggered) OnSlowdownTriggered?.Invoke();
    //     }
    // }
    // private void OnTriggerStay(Collider other)
    // {
    //     if (Time.time >= magneticFieldTime && other.CompareTag(Tag.MAGNETIC_FIELD))
    //     {
    //         damageReceiver.TakeDamage(magneticFX.power);
    //         magneticFieldTime = Time.time + 0.2f;
    //     }
    //     if (Time.time >= burningFieldTime && other.CompareTag(Tag.BURNING_FIELD))
    //     {
    //         damageReceiver.TakeDamage(SChaosMeteor.power);
    //         burningFieldTime = Time.time + 0.2f;
    //     }
    // }

    // private void OnTriggerExit(Collider other)
    // {
    //     if (other.CompareTag(Tag.MAGNETIC_FIELD)) OnSlowdownFinished?.Invoke();
    // }

    // private void OnDestroy()
    // {
    //     magneticFX = null;
    // }
}
