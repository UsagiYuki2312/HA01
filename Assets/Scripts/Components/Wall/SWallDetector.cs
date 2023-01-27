using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SWallDetector : MonoBehaviour
{
    public SPlayerMovementController playerMovementController;
    private string UP_WALL = "Up Wall";
    private string DOWN_WALL = "Down Wall";
    private string LEFT_WALL = "Left Wall";
    private string RIGHT_WALL = "Right Wall";
    private string OBSTACLE = "Obstacle";


    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag(UP_WALL)) playerMovementController.isCollidedWithUpWall = true;
        if (other.CompareTag(DOWN_WALL)) playerMovementController.isCollidedWithDownWall = true;
        if (other.CompareTag(LEFT_WALL)) playerMovementController.isCollidedWithLeftWall = true;
        if (other.CompareTag(RIGHT_WALL)) playerMovementController.isCollidedWithRightWall = true;
        if (other.CompareTag(OBSTACLE))
        {
              Debug.Log("trigger0");
            playerMovementController.isCollidedWithObstacle = true;
            // Vector3 dir = other.transform.position - transform.position;
            // if (Mathf.Abs(dir.x) >= Mathf.Abs(dir.y))
            // {
            //     playerMovementController.isCollidedWithUpWall = true;
            // }
            // if (Mathf.Abs(dir.x) < Mathf.Abs(dir.y))
            // {
            //     playerMovementController.isCollidedWithUpWall = true;
            // }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        Debug.Log("trigger0");
        if (other.CompareTag(UP_WALL)) playerMovementController.isCollidedWithUpWall = false;
        if (other.CompareTag(DOWN_WALL)) playerMovementController.isCollidedWithDownWall = false;
        if (other.CompareTag(LEFT_WALL)) playerMovementController.isCollidedWithLeftWall = false;
        if (other.CompareTag(RIGHT_WALL)) playerMovementController.isCollidedWithRightWall = false;
        if (other.CompareTag(OBSTACLE)) playerMovementController.isCollidedWithObstacle = false;
    }
}
