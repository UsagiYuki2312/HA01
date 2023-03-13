using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SDpsAttacker : MonoBehaviourCore
{
    [HideInInspector] public CharacterProperties characterProperties;
    private static DamageReceiver targetDamageReceiver;
    public static bool isAttackable = true;
    private float attackTime;

    private void OnTriggerStay2D(Collider2D other)
    {

        if (isAttackable && Time.time >= attackTime)
        {
            if (targetDamageReceiver == null)
            {
                targetDamageReceiver = other.gameObject.GetComponent<SPlayer>().damageReceiver;
            }
                            targetDamageReceiver.TakeDamage(5);
            attackTime = Time.time + 0.2f;
        }
    }

    private void OnDestroy()
    {
        targetDamageReceiver = null;
    }
}
