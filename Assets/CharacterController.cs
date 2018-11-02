using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;
using UnityEngine;

public class CharacterController : NetworkBehaviour
{
    public float sensitivityMultiplier = 1;

    public float yaw = 0.0f;
    public float pitch = 0.0f;

    private float maxpitch = 89.9f;
    private float minpitch = -89.9f;

    public float maxVelocityChange = 10.0f;

    private float speed = 0f;

    private float walkSpeed = 3.0f;
    private float crouchSpeed = 2.0f;
    private float sprintSpeed = 6.0f;

    public float gravity = 9.81f;
    public bool canJump = true;
    public float jumpHeight = 1.0f;
    public float airSpeed = 4f;
    public bool grounded = false;

    public Vector3 targetVelocity;
    
    [SyncVar]
    public bool crouching;
    [SyncVar]
    public bool sprinting;
    [SyncVar]
    public Vector3 velocityInput;
    [SyncVar]
    public bool jumping;

    [Command]
    public void CmdJump(bool value)
    {
        if (isServer)
        {
            jumping = value;
        }
    }

    [Command]
    public void CmdCrouch(bool value)
    {
        if (isServer)
        {
            crouching = value;
        }
    }

    [Command]
    public void CmdSprint(bool value)
    {
        if (isServer)
        {
            sprinting = value;
        }
    }

    [Command]
    public void CmdVelocityInput(Vector3 value)
    {
        if (isServer)
        {
            velocityInput = value;
        }
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
        if (sprinting)
            GetComponent<Player>().model.armAim = false;

        if (crouching)
            sprinting = false;

        if (crouching)
            speed = crouchSpeed;
        else if (sprinting)
            speed = sprintSpeed;
        else
            speed = walkSpeed;

        if(GetComponent<RuntimeWeapon>().aiming)
            sensitivityMultiplier = 0.5f;
        else
            sensitivityMultiplier = 1f;
    }

    public virtual void FixedUpdate()
    {
        Movement();
        Face();
    }

    private void Movement()
    {
        if (isLocalPlayer)
        {
            if (targetVelocity.magnitude > 1)
                targetVelocity.Normalize();

            CmdVelocityInput(targetVelocity);

            targetVelocity = transform.TransformDirection(targetVelocity);
            // Apply a force that attempts to reach our target velocity
            Vector3 velocity = GetComponent<Rigidbody>().velocity;
            Vector3 velocityChange = (targetVelocity * speed - velocity);
            velocityChange.x = Mathf.Clamp(velocityChange.x, -maxVelocityChange, maxVelocityChange);
            velocityChange.z = Mathf.Clamp(velocityChange.z, -maxVelocityChange, maxVelocityChange);
            velocityChange.y = 0;

            if (jumping)
            {
                GetComponent<Rigidbody>().velocity = new Vector3(velocity.x, CalculateJumpVerticalSpeed(), velocity.z);
                CmdJump(false);
            }

            GetComponent<Rigidbody>().AddForce(velocityChange, ForceMode.VelocityChange);


            //
            if (!grounded)
                targetVelocity *= airSpeed;
            else
                targetVelocity *= speed;
        }
    }

    private void Face()
    {
        if (isLocalPlayer)
        {
            if (pitch > maxpitch)
                pitch = maxpitch;

            if (pitch < minpitch)
                pitch = minpitch;

            transform.GetComponent<Player>().localpitch = pitch;
            if (pitch != transform.GetComponent<Player>().pitch)
                transform.GetComponent<Player>().CmdSetPitch(pitch);
            transform.GetComponent<Player>().yaw = yaw;
        }
    }
    
    void OnCollisionStay()
    {
        grounded = true;
    }

    float CalculateJumpVerticalSpeed()
    {
        // From the jump height and gravity we deduce the upwards speed 
        // for the character to reach at the apex.
        return Mathf.Sqrt(2 * jumpHeight * gravity);
    }
}