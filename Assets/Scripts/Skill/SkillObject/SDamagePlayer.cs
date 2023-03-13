using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SDamagePlayer : MonoBehaviour
{
    public UnityAction OnPlayerTouched;
    public float power;

    // attached object must has layer "Attacker" 
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            SGameInstance.Instance.player.damageReceiver.TakeDamage(power);
        }
        OnPlayerTouched?.Invoke();
    }
}
