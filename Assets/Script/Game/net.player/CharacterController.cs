using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;
using UnityEngine;

namespace net.bigdog.game.player {
    public class CharacterController : MonoBehaviour
    {
        public float sensitivityMultiplier = 1;

        [Header("Mouse")]
        public float yaw = 0.0f;
        public float pitch = 0.0f;

        private float maxpitch = 89.9f;
        private float minpitch = -89.9f;
        
        private float currentSpeed = 0f;
        private float accelerationMultiplier = 4f;

        [Header("Speed Values")]
        public float walkSpeed = 3.0f;
        public float crouchSpeed = 2.0f;
        public float sprintSpeed = 6.0f;

        [Header("Other Values")]
        private const float Gravity = 9.81f;
        public float jumpHeight = 1.0f;
        public float airSpeedMultiplier = 0.5f;
        public float maxVelocityChange = 10.0f;

        public Vector3 velocityInput;
        public Vector3 targetVelocity;

        [Header("States")]
        public bool canJump = true;
        public bool grounded = false;
        public bool crouching;
        public bool sprinting;
        public bool jumping;

        [Header("Abillities")]
        public bool canMove = true;
        public bool canSprint = true;
        public bool canCollide = true;
        public bool canLook = true;
        public bool forceStand = false;

        public void Jump(bool value)
        {
            jumping = value;
        }

        public void Crouch(bool value)
        {

            crouching = value;
        }
    
        public void Sprint(bool value)
        {
            sprinting = value;
        }
    
        public void VelocityInput(Vector3 value)
        {
            velocityInput = value;
        }

        void Awake()
        {
            //Disable rotation to prevent character from tipping over
            GetComponent<Rigidbody>().freezeRotation = true;
            //Disable gravity so that we can simulate gravity ourselves
            GetComponent<Rigidbody>().useGravity = false;
        }

        public virtual void Update()
        {
            if (crouching)
                sprinting = false;

            if (crouching)
                currentSpeed = crouchSpeed;
            else if (sprinting)
                currentSpeed = sprintSpeed;
            else
                currentSpeed = walkSpeed;

            if(GetComponent<RuntimeItem>().aiming)
                sensitivityMultiplier = 0.5f;
            else
                sensitivityMultiplier = 1f;
        }

        public virtual void FixedUpdate()
        {
            CheckAbillities();
            Movement();
            Face();
        }

        public void CheckAbillities()
        {
            if (!canMove)
            {
                Jump(false);
                velocityInput = Vector3.zero;
            }
            if (forceStand)
            {
                crouching = false;
            }
            if (!canSprint)
            {
                sprinting = false;
            }

            GetComponent<Collider>().enabled = canCollide;
        }

        public virtual void Movement()
        {
            // We apply gravity manually for more tuning control
            GetComponent<Rigidbody>().AddForce(new Vector3(0, -Gravity * GetComponent<Rigidbody>().mass, 0));

            if (targetVelocity.magnitude > 1)
                targetVelocity.Normalize();

            VelocityInput(targetVelocity);

            //Make target velocity local
            targetVelocity = transform.TransformDirection(targetVelocity);

            // Apply a force that attempts to reach our target velocity
            Vector3 velocity = GetComponent<Rigidbody>().velocity;
            Vector3 velocityChange = (targetVelocity * currentSpeed - velocity);

            //Clamp values (min and max)
            velocityChange.x = Mathf.Clamp(velocityChange.x, -maxVelocityChange, maxVelocityChange);
            velocityChange.z = Mathf.Clamp(velocityChange.z, -maxVelocityChange, maxVelocityChange);

            //Reset y change
            velocityChange.y = 0;

            //Apply Speed
            velocityChange *= currentSpeed;

            if(!grounded)
            velocityChange *= airSpeedMultiplier;

            //If we detect a jump, perform jump
            if (jumping)
            {
                GetComponent<Rigidbody>().velocity = new Vector3(velocity.x, CalculateJumpVerticalSpeed(), velocity.z);
                Jump(false);
            }

            //Acceleration
            velocityChange *= accelerationMultiplier * Time.fixedDeltaTime;

            //Apply velocity change
            GetComponent<Rigidbody>().AddForce(velocityChange, ForceMode.VelocityChange);
        }

        private void Face()
        {
            if (pitch > maxpitch)
                pitch = maxpitch;

            if (pitch < minpitch)
                pitch = minpitch;

            transform.GetComponent<Player>().pitch = pitch;
            if (pitch != transform.GetComponent<Player>().pitch)
                transform.GetComponent<Player>().CmdSetPitch(pitch);
            transform.GetComponent<Player>().yaw = yaw;
        }
    
        void OnCollisionStay()
        {
            if(Physics.CheckBox(transform.Find("Ground Check").position, new Vector3(0.1f, 0.2f, 0.1f), Quaternion.identity, 11))
                grounded = true;
        }

        float CalculateJumpVerticalSpeed()
        {
            // From the jump height and gravity we deduce the upwards speed 
            // for the character to reach at the apex.
            return Mathf.Sqrt(2 * jumpHeight * Gravity);
        }
    }
}