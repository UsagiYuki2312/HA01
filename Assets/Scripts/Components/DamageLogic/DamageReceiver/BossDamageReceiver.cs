using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossDamageReceiver : DamageReceiver
{
    private PlayerProperties playerProperties;
    public BossDamageReceiver(CharacterProperties characterProperties) : base(characterProperties)
    {
        playerProperties = SGameInstance.Instance.player.playerProperties;
    }

    public override void TakeDamage(float damage)
    {
        damage += damage * playerProperties.baseDamage;
        base.TakeDamage(damage);
    }
}
