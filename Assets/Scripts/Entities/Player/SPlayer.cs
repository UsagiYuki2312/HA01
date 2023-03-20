using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SPlayer : MonoBehaviourCore
{
    public PlayerProperties playerProperties;
    public SPlayerMovementController movementComponent;
    public SPlayerSkillController skillComponent;
    public Rigidbody2D playerRigid;
    public SPlayerGause playerGause;
    public SFollower follower;
    public SAlienSensor alienSensor;
    public DamageReceiver damageReceiver;
    public Action OnMovementDelayEnd;
    private void Awake()
    {
        damageReceiver = new DamageReceiver(playerProperties);
        damageReceiver.OnCharacterDie = OnPlayerDie;
        damageReceiver.OnCharacterTakeDamage += OnPlayerTakeDamage;

        movementComponent = GetComponent<SPlayerMovementController>();
        skillComponent = GetComponent<SPlayerSkillController>();
        alienSensor = GetComponentInChildren<SAlienSensor>();
        playerRigid = GetComponent<Rigidbody2D>();
        movementComponent.playerProperties = playerProperties;
        skillComponent.OnCharacterRecovery = UpdatePlayerHealth;
        skillComponent.OnCharacterUseSkill = EnableMovement;
    }

    private void Start()
    {
        playerProperties.CalculateProperties();
        //SGameInstance.Instance.gameEvent.OnPlayerUseSkill = StopMovement;
        //OnMovementDelayEnd = EnableMovement;
    }
    //=========================DASH================
    private bool canDash = true;
    private bool isDashing;
    private float dashingPower = 20f;
    private float dashingTime = 0.1f;
    private float dashingCooldown = 1f;

    public IEnumerator Dash(Vector3 dir)
    {
        canDash = false;
        isDashing = true;
        transform.Translate(dir * 2);
        isDashing = false;
        //playerRigid.velocity = Vector3.zero;
        yield return new WaitForSeconds(dashingCooldown);
        canDash = true;

    }
    public void GoDash(Vector3 dir)
    {
        StartCoroutine(Dash(dir));
    }

    private void OnPlayerTakeDamage(float totalDamage)
    {
        UpdatePlayerHealth(totalDamage);
    }


    private void OnPlayerDie()
    {
        CheckAndTriggerDieLogic();
    }

    public void SetupDependencies(GameStateData gameStateData, SPlayerGause playerGause)
    {

        this.playerGause = playerGause;
        //playerProperties.health = gameStateData.playerHealth == 0 ? playerProperties.maxHealth : gameStateData.playerHealth;
        Debug.Log("SetupDependencies health: " + playerProperties.health);
        Debug.Log("SetupDependencies Max health: " + playerProperties.maxHealth);
        this.playerGause.SetupHealthBar(playerProperties.maxHealth, this.transform);
        this.playerGause.SetHealth(playerProperties.health);
        this.playerGause.SetPosition();
        this.playerGause.updatePosition = true;
    }

    private void CheckAndTriggerDieLogic()
    {
        MessageManager.SendMessage(new Message(TeeMessageType.OnPlayerDie));
    }

    public void EnableBehaviours(bool isEnable)
    {
        // movementComponent.floatingJoystick.gameObject.SetActive(isEnable);
        //  movementComponent.enabled = isEnable;
        //  damageReceiver.isAttackable = isEnable;
        // skillComponent.enabled = isEnable;
    }

    private void UpdatePlayerHealth(float agr)
    {
        playerGause.SetHealth(playerProperties.health);
        DataController.GameStateData.playerHealth = playerProperties.health;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("SetupDependencies health: " + playerProperties.health);
            Debug.Log("SetupDependencies Max health: " + playerProperties.maxHealth);
        }
    }

    public void EnableMovement(bool isEnable)
    {
        // movementComponent.floatingJoystick.gameObject.SetActive(isEnable);
        movementComponent.enabled = isEnable;
        //  damageReceiver.isAttackable = isEnable;
        // skillComponent.enabled = isEnable;
    }

}
