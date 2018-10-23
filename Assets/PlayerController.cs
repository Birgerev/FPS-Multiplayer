using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;
using UnityEngine;

public class PlayerController : NetworkBehaviour {
    
    private float sensitivityX = 4.0f;
    private float sesitivityY = 4.0f;

    private float yaw = 0.0f;
    private float pitch = 0.0f;

    public float maxpitch = 89.9f;
    public float minpitch = -89.9f;

    public float speed = 10.0f;
    public float gravity = 9.81f;
    public float maxVelocityChange = 10.0f;
    public bool canJump = true;
    public float jumpHeight = 2.0f;
    public float airSpeed = 4f;
    private bool grounded = false;

    public static bool showMouse = false;

    void Update()
    {
        Mouse();
        Interaction();
    }
    
    void Awake()
    {
        GetComponent<Rigidbody>().freezeRotation = true;
        GetComponent<Rigidbody>().useGravity = false;
    }

    void FixedUpdate()
    {
        Movement();
    }

    private void Interaction()
    {
        if (isLocalPlayer)
        {
            //GetComponent<Player>().CmdInterract(new WeaponInput(
            //    (Input.GetKeyDown(InputManager.interact1)),
            //    (Input.GetKey(InputManager.interact1)), 
            //    (Input.GetKey(InputManager.interact2)),
            //    (Input.GetKeyDown(InputManager.reload))));
        }
    }

    private void Movement()
    {
        if (isLocalPlayer)
        {
            // Calculate how fast we should be moving
            Vector3 targetVelocity = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
            targetVelocity = transform.TransformDirection(targetVelocity);

            if (!grounded)
                targetVelocity *= airSpeed;
            else
                targetVelocity *= speed;

            // Apply a force that attempts to reach our target velocity
            Vector3 velocity = GetComponent<Rigidbody>().velocity;
            Vector3 velocityChange = (targetVelocity - velocity);
            velocityChange.x = Mathf.Clamp(velocityChange.x, -maxVelocityChange, maxVelocityChange);
            velocityChange.z = Mathf.Clamp(velocityChange.z, -maxVelocityChange, maxVelocityChange);
            velocityChange.y = 0;
            GetComponent<Rigidbody>().AddForce(velocityChange, ForceMode.VelocityChange);

            // Jump
            if (grounded)
            {
                if (canJump && Input.GetButtonDown("Jump"))
                {
                    GetComponent<Rigidbody>().velocity = new Vector3(velocity.x, CalculateJumpVerticalSpeed(), velocity.z);
                }
            }

            // We apply gravity manually for more tuning control
            GetComponent<Rigidbody>().AddForce(new Vector3(0, -gravity * GetComponent<Rigidbody>().mass, 0));

            grounded = false;
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

    private void Mouse()
    {
        if (isLocalPlayer)
        {
            yaw += sensitivityX * Input.GetAxis("Mouse X");
            pitch -= sesitivityY * Input.GetAxis("Mouse Y");

            if (pitch > maxpitch)
                pitch = maxpitch;

            if (pitch < minpitch)
                pitch = minpitch;

            transform.GetComponent<Player>().localpitch = pitch;
            if (pitch != transform.GetComponent<Player>().pitch)
                transform.GetComponent<Player>().CmdSetPitch(pitch);
            transform.GetComponent<Player>().yaw = yaw;

            Cursor.visible = showMouse;
            Cursor.lockState = (showMouse) ? CursorLockMode.None : CursorLockMode.Locked;

            if (Input.GetMouseButtonDown(0))
                showMouse = false;
            if (Input.GetKeyDown(KeyCode.Escape))
                showMouse = true;
        }
    }
}
