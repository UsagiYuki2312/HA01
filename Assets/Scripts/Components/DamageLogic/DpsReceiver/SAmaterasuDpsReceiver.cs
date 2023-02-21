using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SAmaterasuDpsReceiver : SBaseDpsReceiver
{
    //protected override string ZoneTag => Tag.AMATERATSU;
    protected override float DamageInterval => 0.04f;

    protected override void Awake()
    {
        //power = SAmaterasuItem.power;
    }
}
