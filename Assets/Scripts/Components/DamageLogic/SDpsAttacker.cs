using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SDpsAttacker : MonoBehaviourCore
{
    [HideInInspector] public CharacterProperties characterProperties;
    private static DamageReceiver targetDamageReceiver;
    public static bool isAttackable = true;
    private float attackTime;

    private void OnTriggerStay(Collider other)
    {
        if (isAttackable && Time.time >= attackTime)
        {
            if (targetDamageReceiver == null) targetDamageReceiver = other.gameObject.GetComponent<SPlayer>().damageReceiver;
            targetDamageReceiver.TakeDamage(characterProperties.damage);
            attackTime = Time.time + 0.2f;
        }
    }

    private void OnDestroy()
    {
        targetDamageReceiver = null;
    }
}
