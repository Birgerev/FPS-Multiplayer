using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : CharacterController {
    
    public float sensitivity = 1;

    private float sensitivityX = 4.0f;
    private float sesitivityY = 4.0f;

    private float joystickMultiplier = 1f;

    public static bool showMouse = false;

    private int lastframecrouch = 0;
    private int lastframesprint = 0;
    
    public override void Update()
    {
        base.Update();


    }
    
    public override void FixedUpdate()
    {
        base.FixedUpdate();

        Movement();
        Mouse();
        CursorSettings();
    }

    private void Movement()
    {
        if (isLocalPlayer)
        {

            // Calculate how fast we should be moving
            targetVelocity = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
            
            
            // Jump
            if (grounded)
            {
                if (canJump && Input.GetButtonDown("Jump"))
                {
                    CmdJump(true);
                }
            }

            // We apply gravity manually for more tuning control
            GetComponent<Rigidbody>().AddForce(new Vector3(0, -gravity * GetComponent<Rigidbody>().mass, 0));

            grounded = false;
            
            //Crouching
            if (Input.GetButtonDown("Crouch"))
                CmdCrouch(true);
            else if (Input.GetButtonUp("Crouch"))
                CmdCrouch(false);
            else if ((Input.GetButtonDown("Joystick Crouch") && lastframecrouch > 10)) { 
                print("crouch:");
                CmdCrouch(!crouching);
                lastframecrouch = 0;
            }
            else lastframecrouch++;

            //Sprinting
            if (Input.GetButtonDown("Sprint"))
                CmdSprint(true);
            else if (Input.GetButtonUp("Sprint"))
                CmdSprint(false);
            else if ((Input.GetButtonDown("Joystick Sprint") && lastframesprint > 10))
            {
                CmdSprint(!sprinting);
                lastframesprint = 0;
            }
            else lastframesprint++;

        }
    }

    private void Mouse()
    {
        //print(Input.GetAxis("Look X"));
        if (isLocalPlayer)
        {
            yaw += sensitivityMultiplier * (sensitivityX * (Input.GetAxis("Mouse X")));// + (Input.GetAxis("Look X") * joystickMultiplier));
            pitch -= sensitivityMultiplier * (sesitivityY * (Input.GetAxis("Mouse Y")));// + (Input.GetAxis("Look Y") * joystickMultiplier));
        }
    }

    private void CursorSettings()
    {
        Cursor.visible = showMouse;
        Cursor.lockState = (showMouse) ? CursorLockMode.None : CursorLockMode.Locked;

        if (Input.GetMouseButtonDown(0))
            showMouse = false;
        if (Input.GetKeyDown(KeyCode.Escape))
            showMouse = true;
    }
}
