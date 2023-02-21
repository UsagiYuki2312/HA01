using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SMagneticfieldDpsReceiver : SBaseDpsReceiver
{
    //protected override string ZoneTag => Tag.MAGNETIC_FIELD;
    public SMovement movement;
    // private static SMagneticFX magneticFX;

    // protected override void Reset()
    // {
    //     movement = GetComponent<SMovement>();
    // }

    // protected override void OnZoneEnter(Collider other)
    // {
    //     if (magneticFX == null) magneticFX = other.GetComponent<SMagneticFX>();
    //     if (magneticFX.isSlowDownTriggered) movement.OnSlowdownTriggered();
    // }

    // protected override void OnZoneStay()
    // {
    //     power = magneticFX.power;
    //     base.OnZoneStay();
    // }

    // protected override void OnZoneExit()
    // {
    //     if (magneticFX.isSlowDownTriggered) movement.OnSlowdownFinished();
    // }
}
