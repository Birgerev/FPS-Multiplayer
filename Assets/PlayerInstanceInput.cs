using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInstanceInput : MonoBehaviour
{
    [System.Serializable]
    public struct InputData
    {
        //movement
        public bool space;

        public float vertical;
        public float horizontal;

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

    private float mouseSensitivityX = 4.0f;
    private float mouseSesitivityY = 4.0f;

    public static float mouseSensitivity = 1;

    public InputData input = new InputData();

    //public InputData input;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!GetComponent<PlayerInstance>().isLocalPlayer)
            Destroy(this);

        CursorSettings();

        input.space = Input.GetKey(KeyCode.Space);

        input.horizontal = Input.GetAxis("Horizontal");
        input.vertical = Input.GetAxis("Vertical");

        input.crouch = Input.GetKey(KeyCode.LeftControl);
        input.sprint = Input.GetKey(KeyCode.LeftShift);

        input.aim = Input.GetMouseButton(1);
        input.shoot = Input.GetMouseButton(0);
        input.reload = Input.GetKey(KeyCode.R);

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
            }
        }

        if (!numpadChanged)
            input.lastNumpad = -1;

        if (showMouse == false)
        {
            input.yaw += mouseSensitivity * (mouseSensitivityX * (Input.GetAxis("Mouse X")));// + (Input.GetAxis("Look X") * joystickMultiplier));
            input.pitch -= mouseSensitivity * (mouseSesitivityY * (Input.GetAxis("Mouse Y")));// + (Input.GetAxis("Look Y") * joystickMultiplier));

        }
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
}
