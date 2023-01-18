using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SBossCrash : SBoss
{
    private GameObject redlineObject;
    private ParticleSystem redline;
    private SPlayer player;
    public SPlayer Player
    {
        get
        {
            if (player == null) player = GameInstance.player;
            return player;
        }
    }
    public bool isAimToPlayer;
}
