using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;
using UnityEngine;

namespace net.bigdog.game.player.camera
{
    public class CameraController : MonoBehaviour
    {

        private const float normalFoV = 60;

        public static float aimingFoV = 45;
        public static bool aiming = false;

        private Camera camera;
        private CameraSpring cameraSpring;
        private float cameraZoomSpeed = 10f;

        public Animator weaponCameraAnimator;
        public Animator cameraAnimator;

        public Animator animator;

        public Player player;

        // Use this for initialization
        void Start()
        {
            camera = GetComponentInChildren<Camera>();
            cameraSpring = GetComponentInChildren<CameraSpring>();
            animator = GetComponent<Animator>();
        }

        // Update is called once per frame
        void Update()
        {
            if (player == null)
                player = GetComponentInParent<Player>();

            animateCameraStance();
            animateWeaponCamera();
            animateCamera();

            /*
            if (aiming)
            {
                if(camera.fieldOfView > aimingFoV)
                {
                    camera.fieldOfView -= cameraZoomSpeed;
                }
            }
            if (!aiming)
            {
                if (camera.fieldOfView < normalFoV)
                {
                    camera.fieldOfView += cameraZoomSpeed;
                }
            }*/
        }

        public void Recoil(Vector3 vel)
        {
            cameraSpring.velocity += vel * cameraSpring.velocityMultiplier;
        }

        private void animateCameraStance()
        {
            if (player.networkInstance != null)
            {
                animator.SetBool("crouching", player.networkInstance.input.crouch);
            }
        }

        private void animateWeaponCamera()
        {
            if (player.networkInstance != null)
            {
                weaponCameraAnimator.SetBool("walking", (player.networkInstance.input.vertical != 0 || player.networkInstance.input.horizontal != 0));
                weaponCameraAnimator.SetBool("sprint", (player.controller.sprinting));
                weaponCameraAnimator.SetBool("jump", (player.networkInstance.input.space));
                weaponCameraAnimator.SetBool("crouching", (player.controller.crouching));
                weaponCameraAnimator.SetBool("grounded", (player.controller.grounded));
                weaponCameraAnimator.SetBool("aiming",
                        (player.networkInstance.input.aim && player.item.item.weaponData.isLoaded
                        && (player.item) is RuntimeWeapon));
                weaponCameraAnimator.SetBool("shooting",
                        (player.networkInstance.input.shoot && player.item.item.weaponData.isLoaded
                        && (player.item) is RuntimeWeapon));

            }
        }

        private void animateCamera()
        {
            /*
            if (player.networkInstance != null)
            {
                cameraAnimator.SetBool("walking", (player.networkInstance.input.vertical != 0 || player.networkInstance.input.horizontal != 0));
                cameraAnimator.SetBool("sprint", (player.controller.sprinting));
                cameraAnimator.SetBool("jump", (player.networkInstance.input.space));
                cameraAnimator.SetBool("crouching", (player.controller.crouching));
                cameraAnimator.SetBool("grounded", (player.controller.grounded));
            }*/
        }
    }
}