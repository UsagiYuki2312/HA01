using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CharacterProperties : ClassInstanceCore
{
    [Header("Basic attributes")]
    public int level;
    public float health;
    public float speed;
    public float damage;
    public float armor;
}
