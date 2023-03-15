using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonAttackController : ClassInstanceCore
{
    private float distanceMaxToMove = 40f;
    private float distanceMinToMove = 10f;
    public ButtonAttackController()
    {
    }
    public void GoToClosetTarget()
    {
        SAlien target;
        Vector3 direction;
        Vector3 targetPosition;
        target = GameInstance.player.alienSensor.closestAliens;

        if (target == null || !target.gameObject.activeInHierarchy)
        {
            //Normal attack
        }
        if (target != null && GameInstance.player.alienSensor.closestDistance <= distanceMaxToMove
                && GameInstance.player.alienSensor.closestDistance > distanceMinToMove)
        {
            targetPosition = target.transform.position;
            direction = targetPosition - GameInstance.player.transform.position;

            GameInstance.player.movementComponent.isClickGoTo = true;
            GameInstance.player.movementComponent.targetGoTo = direction;
        }
        if (target != null && GameInstance.player.alienSensor.closestDistance <= 3f)
        {
            //Normal attack
        }

    }
    public void OnClickAttackButton()
    {
        //GoToClosetTarget();
        SGameInstance.Instance.player.skillComponent.UseNormalAttack();
        Debug.Log("Attack button click");
    }
    public void OnClickDialogueButton()
    {

    }
}
