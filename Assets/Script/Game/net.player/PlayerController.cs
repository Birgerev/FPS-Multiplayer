using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace net.bigdog.game.player
{
    public class PlayerController : CharacterController
    {

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
        }

        private void Movement()
        {
            if (transform.GetComponent<Player>().networkInstance == null)
                return;

            PlayerInstanceInput.InputData input = transform.GetComponent<Player>().networkInstance.input;

            // Calculate how fast we should be moving
            targetVelocity = new Vector3(input.horizontal, 0, input.vertical);


            // Jump
            if (grounded)
            {
                if (canJump && input.space)
                {
                    Jump(true);
                }
            }

            //crouch
            Crouch(input.crouch);

            //sprint
            Sprint(input.sprint);


            // We apply gravity manually for more tuning control
            GetComponent<Rigidbody>().AddForce(new Vector3(0, -gravity * GetComponent<Rigidbody>().mass, 0));

            grounded = false;

            //Mouse
            pitch = input.pitch;
            yaw = input.yaw;


            //Crouching
            /*if (Input.GetButtonDown("Crouch"))
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
                */

        }
    }
}