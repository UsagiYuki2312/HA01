using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SPlayerMovement : MonoBehaviourCore
{
    [SerializeField] private FixedJoystick joystick;
     [SerializeField] private Rigidbody rigidbody;
    
    void FixedUpdate()
    {
        rigidbody.velocity = new Vector3(joystick.Horizontal * 5, 
        joystick.Vertical * 5);
    }
}
