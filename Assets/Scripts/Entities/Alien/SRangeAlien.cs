using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SRangeAlien : SAlien
{
    protected override void SettingSAlien()
    {
        base.SettingSAlien();
        movement.typeMove = 3;
    }
}
