using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DamageReceiver
{
    public UnityAction OnCharacterDie;
    public UnityAction<float> OnCharacterTakeDamage;
    [HideInInspector] public CharacterProperties characterProperties;
    public bool isAttackable = true;
    private float totalDamage;

    public DamageReceiver(CharacterProperties characterProperties)
    {
        this.characterProperties = characterProperties;
    }

    public virtual void TakeDamage(float damage)
    {
        if (!isAttackable) return;
        TriggerDamageLogic(damage);
    }

    protected virtual void TriggerDamageLogic(float damage)
    {
        totalDamage = damage - characterProperties.armor;
        totalDamage = Mathf.Clamp(totalDamage, 3, float.MaxValue);
        characterProperties.health -= totalDamage;
        OnCharacterTakeDamage?.Invoke(totalDamage);
        if (characterProperties.health <= 0) OnCharacterDie?.Invoke();
    }
}
