using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace net.bigdog.game.player.camera
{
    public class WeaponSway : MonoBehaviour {

        public float amountX;
        public float amountY;
        public float maxAmountX;
        public float maxAmountY;

        private Vector3 initialPosition;
        private CameraController cameraController;

        // Use this for initialization
        void Start() {
            cameraController = GetComponentInParent<CameraController>();

            initialPosition = transform.localPosition;
        }

        // Update is called once per frame
        void Update() {
            if (cameraController.player.networkInstance.input.aim)
            {
                transform.localPosition = initialPosition;
                return;
            }


            float movementX = (Input.GetAxis("Right Stick X")*0.01f)+Input.GetAxis("Mouse X") * amountX;
            float movementY = (Input.GetAxis("Right Stick Y")*0.01f) + Input.GetAxis("Mouse Y") * amountY;

            Vector3 totalChange = transform.localPosition - initialPosition;

            if ((totalChange.x >= maxAmountX && movementX < 0)
                || (totalChange.x <= -maxAmountX && movementX > 0))
                movementX = 0;
            if ((totalChange.y >= maxAmountY && movementY < 0)
                || (totalChange.y <= -maxAmountY && movementY > 0))
                movementY = 0;

            transform.localPosition = transform.localPosition - new Vector3(movementX, movementY);
        }
    }
}
