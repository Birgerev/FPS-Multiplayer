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

        private float mouseSensitivityX = 4.0f;
        private float mouseSesitivityY = 4.0f;

        public static float mouseSensitivity = 1;

        private int framesSinceNumpad = 0;
        private int framesSinceReload = 0;

        public InputData input = new InputData();

        //public InputData input;
        // Start is called before the first frame update
        void Start()
        {
            if (!GetComponent<PlayerInstance>().isLocalPlayer)
                Destroy(this);

            inputMaster = new InputMaster();
            inputMaster.Enable();


            inputMaster.Soldier.Shoot.performed += handleShoot;
            inputMaster.Soldier.Shoot.canceled += handleShoot;
            inputMaster.Soldier.Shoot.Enable();

            inputMaster.Soldier.Aim.performed += handleAim;
            inputMaster.Soldier.Aim.canceled += handleAim;
            inputMaster.Soldier.Aim.Enable();


            inputMaster.Soldier.Jump.performed += handleJump;
            inputMaster.Soldier.Jump.canceled += handleJump;
            inputMaster.Soldier.Jump.Enable();


            inputMaster.Soldier.Movement.performed += handleMovement;
            inputMaster.Soldier.Movement.canceled += handleMovement;
            inputMaster.Soldier.Movement.Enable();
        }

        // Update is called once per frame
        void Update()
        {
            if (!GetComponent<PlayerInstance>().isLocalPlayer)
                Destroy(this);

            CursorSettings();

            input.crouch = Input.GetKey(KeyCode.LeftControl);
            input.sprint = Input.GetKey(KeyCode.LeftShift);
            //input.shoot = Input.GetMouseButton(0);

            if (Input.GetKey(KeyCode.R))
            {
                input.reload = true;
                framesSinceReload = 0;
            }
            else framesSinceReload++;

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

            if (showMouse == false)
            {
                input.yaw += mouseSensitivity * (mouseSensitivityX * (Input.GetAxis("Mouse X")));// + (Input.GetAxis("Look X") * joystickMultiplier));
                input.pitch -= mouseSensitivity * (mouseSesitivityY * (Input.GetAxis("Mouse Y")));// + (Input.GetAxis("Look Y") * joystickMultiplier));

                if (GetComponent<PlayerInstance>().limitPitch)
                {
                    if (input.pitch > 75)
                        input.pitch = 75;
                    if (input.pitch < -90)
                        input.pitch = -90;
                }
            }
            framesSinceNumpad++;
        }

        public static bool showMouse = false;
        private void CursorSettings()
        {
            Cursor.visible = showMouse;
            Cursor.lockState = (showMouse) ? CursorLockMode.None : CursorLockMode.Locked;

            if (Input.GetMouseButtonDown(0))
                showMouse = false;
            if (Input.GetKeyDown(KeyCode.Escape))
                showMouse = true;
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

        public void handleMovement(InputAction.CallbackContext context)
        {
            Vector2 vel = context.ReadValue<Vector2>();
            
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
        



    }
}