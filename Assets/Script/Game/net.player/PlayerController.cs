using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace net.bigdog.game.player
{
    public class PlayerController : CharacterController
    {

        private float joystickMultiplier = 1f;

        private int lastframecrouch = 0;
        private int lastframesprint = 0;

        public override void Update()
        {
            base.Update();
        }

        public override void FixedUpdate()
        {
            base.FixedUpdate();
        }

        public override void Movement()
        {
            base.Movement();

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

            grounded = false;

            //Mouse
            pitch = input.pitch;
            yaw = input.yaw;
        }
    }
}