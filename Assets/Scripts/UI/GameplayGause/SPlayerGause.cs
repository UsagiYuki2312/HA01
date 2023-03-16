using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SPlayerGause : MonoBehaviour
{
    private Transform player;
    public Slider healthBar;
    public Image healthImage;
    public Sprite fullHealth;
    public Sprite midHealth;
    public Sprite lowHealth;
    private Camera mainCamera;
    private float maxHealth;
    public float MaxHealth
    {
        get { return maxHealth; }
        set
        {
            maxHealth = value;
            healthBar.maxValue = maxHealth;
        }
    }

    [Header("Update position realtime")]
    public bool updatePosition;
    public float offsetY;
    private Vector3 position;

    private void Awake()
    {
        mainCamera = Camera.main;
    }

    public void SetupHealthBar(float maxHealth, Transform player)
    {
        MaxHealth = maxHealth;
        healthBar.value = maxHealth;
        this.player = player;
    }

    public void SetHealth(float health)
    {
        healthBar.value = health;
        SetupHealthColor();
    }

    private void SetupHealthColor()
    {
        float division = healthBar.value / maxHealth;
        if (division > 0.7f) healthImage.sprite = fullHealth;
        else if (division > 0.3) healthImage.sprite = midHealth;
        else healthImage.sprite = lowHealth;
    }

    public void SetPosition()
    {
        position = mainCamera.WorldToScreenPoint(player.position, Camera.MonoOrStereoscopicEye.Mono);
        position.y += offsetY;
        transform.position = position;
    }

    private void Update()
    {
        if (updatePosition && player != null) SetPosition();
    }
}
