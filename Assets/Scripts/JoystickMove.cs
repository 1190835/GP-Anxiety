using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StarterAssets;

public class JoystickMove : MonoBehaviour
{
    public float speed;
    public VariableJoystick variableJoystick;
    public StarterAssetsInputs starterAssetsInputs;

    public void FixedUpdate()
    {
        //Vector3 direction = Vector3.forward * variableJoystick.Vertical + Vector3.right * variableJoystick.Horizontal;
        Vector2 movePosition = new Vector2(variableJoystick.Horizontal,variableJoystick.Vertical);
        starterAssetsInputs.MoveInput(movePosition*speed*Time.fixedDeltaTime);
        //Debug.Log(variableJoystick.Vertical)
    }
}