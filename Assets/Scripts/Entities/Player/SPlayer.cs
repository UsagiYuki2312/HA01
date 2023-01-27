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
    public SFollower follower;
    public SAlienSensor alienSensor;
    public Action OnMovementDelayEnd;
    private void Awake()
    {
        movementComponent = GetComponent<SPlayerMovementController>();
        skillComponent = GetComponent<SPlayerSkillController>();
        //alienSensor = GetComponentInChildren<SAlienSensor>();
        playerRigid = GetComponent<Rigidbody2D>();
        //movementComponent.playerProperties = playerProperties;
    }

    // private void Start()
    // {
    //     playerProperties.CalculateProperties();
    //     SGameInstance.Instance.gameEvent.OnPlayerUseSkill = StopMovement;
    //     OnMovementDelayEnd = EnableMovement;
    // }
    // private void Update()
    // {
    //     if (alienSensor.closestDistance <= 2 || alienSensor.closestAliens == null || alienSensor.closestDistance >= 40f)
    //     {
    //         movementComponent.isClickGoTo = false;
    //         movementComponent.targetGoTo = Vector3.zero;
    //     }
    // }
    // private void StopMovement()
    // {
    //     movementComponent.enabled = false;
    //     DelayCallAction(OnMovementDelayEnd, 0.5f);
    // }
    // private void EnableMovement()
    // {
    //     movementComponent.enabled = true;
    // }
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
        transform.Translate(dir*2);
        isDashing = false;
        //playerRigid.velocity = Vector3.zero;
        yield return new WaitForSeconds(dashingCooldown);
        canDash = true;

    }
    public void GoDash(Vector3 dir){
        StartCoroutine(Dash(dir));
    }
}
