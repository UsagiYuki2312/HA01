using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SDamageTrigger : MonoBehaviour
{
    public UnityAction OnAlienTouched;
    public float power;
    private SAlien targetedAlien;
    private PlayerProperties playerProperties;
    public bool deadzone;

    private void Awake()
    {
        if (playerProperties == null)
            playerProperties = SGameInstance.Instance.player.playerProperties;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Alien"))
        {
            targetedAlien = SGameInstance.Instance.GetAlienReference(other.transform.GetInstanceID());
            // if (deadzone)
            // {
            //     float health = targetedAlien.alienProperties.health;
            //     targetedAlien.damageReceiver.TakeDamage(health + 2);
            //     return;
            // }

            power = CalculatePower(power);
            targetedAlien.damageReceiver.TakeDamage(power);
            OnAlienTouched?.Invoke();
        }
    }

    private float CalculatePower(float basePower)
    {
        float result = basePower;
        return result;
    }
}
