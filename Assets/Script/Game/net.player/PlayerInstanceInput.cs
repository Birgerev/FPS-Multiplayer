using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine;

namespace net.bigdog.game.player
{
    public class PlayerInstanceInput : MonoBehaviour
    {
        [System.Serializable]
        public struct InputData
        {
            //movement
            public bool space;

            public int vertical;
            public int horizontal;

            public bool crouch;
            public bool sprint;

            public bool interact;

            //Mouse
            public float pitch;
            public float yaw;

            //Weapons
            public bool aim;
            public bool shoot;
            public bool reload;

            public int lastNumpad;
        }
        
        public InputMaster inputMaster;

        private float mouseSensitivityX = 8.0f;
        private float mouseSensitivityY = 8.0f;
        public static float controllerSensitivity = 8;
        public static float aimingSensitivity = 1f;

        public static float normalSensitivity = 1f;
        public static float sensitivity = 1;

        private int framesSinceNumpad = 0;
        private int framesSinceReload = 0;

        [Header("Local Input")]
        public bool input_Paused = false;
        public bool input_Scoreboard = false;


        [Header("Synced Input")]
        public InputData input = new InputData();

        // Start is called before the first frame update
        void Start()
        {
            if (!GetComponent<PlayerInstance>().isLocalPlayer)
                Destroy(this);

            inputMaster = new InputMaster();
            inputMaster.Enable();

            inputMaster.Soldier.Interact.performed += handleInteract;
            inputMaster.Soldier.Interact.canceled += handleInteract;
            inputMaster.Soldier.Interact.Enable();

            inputMaster.Soldier.Scoreboard.performed += handleScoreboard;
            inputMaster.Soldier.Scoreboard.canceled += handleScoreboard;
            inputMaster.Soldier.Scoreboard.Enable();

            inputMaster.Soldier.Pause.performed += handlePause;
            inputMaster.Soldier.Pause.canceled += handlePause;
            inputMaster.Soldier.Pause.Enable();

            inputMaster.Soldier.Shoot.performed += handleShoot;
            inputMaster.Soldier.Shoot.canceled += handleShoot;
            inputMaster.Soldier.Shoot.Enable();

            inputMaster.Soldier.Aim.performed += handleAim;
            inputMaster.Soldier.Aim.canceled += handleAim;
            inputMaster.Soldier.Aim.Enable();


            inputMaster.Soldier.Jump.performed += handleJump;
            inputMaster.Soldier.Jump.canceled += handleJump;
            inputMaster.Soldier.Jump.Enable();

            inputMaster.Soldier.Crouch.performed += handleCrouch;
            inputMaster.Soldier.Crouch.canceled += handleCrouch;
            inputMaster.Soldier.Crouch.Enable();

            inputMaster.Soldier.ToggleCrouch.performed += handleToggleCrouch;
            inputMaster.Soldier.ToggleCrouch.Enable();


            inputMaster.Soldier.Sprint.performed += handleSprint;
            inputMaster.Soldier.Sprint.canceled += handleSprint;
            inputMaster.Soldier.Sprint.Enable();
            
            inputMaster.Soldier.SwapWeaponPositive.performed += handleSwapWeaponPositive;
            inputMaster.Soldier.SwapWeaponPositive.Enable();

            inputMaster.Soldier.SwapWeaponNegative.performed += handleSwapWeaponNegative;
            inputMaster.Soldier.SwapWeaponNegative.Enable();

            inputMaster.Soldier.Reload.performed += handleReload;
            inputMaster.Soldier.Reload.Enable();
        }

        // Update is called once per frame
        void Update()
        {
            if (!GetComponent<PlayerInstance>().isLocalPlayer)
                Destroy(this);
            
            CursorSettings();

            //Disable input if paused
            if (input_Paused)
            {
                disableInput();
                return;
            }

            sensitivity = normalSensitivity;
            if (input.sprint)
                sensitivity = 0.4f;

            if (input.aim)
                sensitivity = aimingSensitivity;

            handleMouse();
            handleMovement();

            framesSinceReload++;

            if (framesSinceReload >= 0.5f / Time.deltaTime)
            {
                input.reload = false;
            }

            KeyCode[] keyCodes = {
         KeyCode.Alpha0,
         KeyCode.Alpha1,
         KeyCode.Alpha2,
         KeyCode.Alpha3,
         KeyCode.Alpha4,
         KeyCode.Alpha5,
         KeyCode.Alpha6,
         KeyCode.Alpha7,
         KeyCode.Alpha8,
         KeyCode.Alpha9,
     };

            bool numpadChanged = false;

            for (int i = 0; i < keyCodes.Length; i++)
            {
                if (Input.GetKey(keyCodes[i]))
                {
                    input.lastNumpad = i;
                    numpadChanged = true;
                    framesSinceNumpad = 0;
                }
            }

            if (!numpadChanged && framesSinceNumpad >= 1 / Time.deltaTime)
                input.lastNumpad = -1;
            
            framesSinceNumpad++;
        }

        private void disableInput()
        {
            float saved_pitch = input.pitch;
            float saved_yaw = input.yaw;

            input = new InputData();

            input.pitch = saved_pitch;
            input.yaw = saved_yaw;
        }


        public static bool showMouse = false;
        private void CursorSettings()
        {
            Cursor.visible = showMouse;
            Cursor.lockState = (showMouse) ? CursorLockMode.None : CursorLockMode.Locked;
        }

        public void handleInteract(InputAction.CallbackContext context)
        {
            input.interact = context.performed;
        }

        public void handleScoreboard(InputAction.CallbackContext context)
        {
            input_Scoreboard = context.performed;
        }

        public void handlePause(InputAction.CallbackContext context)
        {
            if(context.performed)
                input_Paused = !input_Paused;
        }

        public void handleShoot(InputAction.CallbackContext context)
        {
            input.shoot = context.performed;
        }

        public void handleAim(InputAction.CallbackContext context)
        {
            input.aim = context.performed;
        }

        public void handleJump(InputAction.CallbackContext context)
        {
            input.space = context.performed;
        }

        public void handleCrouch(InputAction.CallbackContext context)
        {
            input.crouch = context.performed;
        }

        public void handleToggleCrouch(InputAction.CallbackContext context)
        {
            input.crouch = !input.crouch;
        }

        public void handleMovement()
        {
            Vector2 vel = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
            
            input.horizontal = 0;
            input.vertical = 0;

            if (vel.x > 0)
                input.horizontal = 1;
            if (vel.x < 0)
                input.horizontal = -1;

            if (vel.y > 0)
                input.vertical = 1;
            if (vel.y < 0)
                input.vertical = -1;
        }

        public void handleMouse()
        {
            if (showMouse == false)
            {
                input.yaw += (sensitivity * (Input.GetAxis("Mouse X"))) + 
                    ((Input.GetAxis("Right Stick X")) * controllerSensitivity);// + (Input.GetAxis("Look X") * joystickMultiplier));
                input.pitch -= (sensitivity * (Input.GetAxis("Mouse Y"))) +
                    ((Input.GetAxis("Right Stick Y")) * controllerSensitivity);// + (Input.GetAxis("Look Y") * joystickMultiplier));

                if (GetComponent<PlayerInstance>().limitPitch)
                {
                    if (input.pitch > 75)
                        input.pitch = 75;
                    if (input.pitch < -90)
                        input.pitch = -90;
                }
            }
        }

        public void handleSprint(InputAction.CallbackContext context)
        {
            input.sprint = context.performed;
        }

        public void handleReload(InputAction.CallbackContext context)
        {
            input.reload = true;
            framesSinceReload = 0;
        }

        public void handleSwapWeaponPositive(InputAction.CallbackContext context)
        {
            input.lastNumpad += 1;
            if (input.lastNumpad > InventoryManager.inventorySize)
                input.lastNumpad = 1;
            if (input.lastNumpad <= 0)
                input.lastNumpad = InventoryManager.inventorySize;
            
            framesSinceNumpad = 0;
        }

        public void handleSwapWeaponNegative(InputAction.CallbackContext context)
        {
            input.lastNumpad -= 1;
            if (input.lastNumpad > InventoryManager.inventorySize)
                input.lastNumpad = 1;
            if (input.lastNumpad <= 0)
                input.lastNumpad = InventoryManager.inventorySize;
            
            framesSinceNumpad = 0;
        }



    }
}