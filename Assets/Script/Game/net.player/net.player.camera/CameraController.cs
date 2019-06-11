using System.Collections;
using System.Collections.Generic;
using UnityEngine.PostProcessing;
using UnityEngine.Networking;
using UnityEngine;

namespace net.bigdog.game.player.camera
{
    public class CameraController : MonoBehaviour
    {

        public const float normalFoV = 60;

        public static float aimingFoV = 45;
        public static bool aiming = false;
        public float fovChangeSpeed = 0.7f;

        public bool supressed = false;

        public Camera camera;
        public Camera weaponCamera;
        private CameraSpring cameraSpring;

        public Animator weaponCameraAnimator;
        public Animator cameraAnimator;

        public Animator animator;

        public Player player;

        // Use this for initialization
        void Start()
        {
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
            applyFoV();
            applyPostProcessing();
        }

        public void Recoil(Vector3 vel, float maxRecoil)
        {
            cameraSpring.velocity += vel * cameraSpring.velocityMultiplier;
            cameraSpring.maxVelocity = maxRecoil;
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

        private void applyPostProcessing()     //Depth Of Field
        {
            DepthOfFieldModel.Settings dof = camera.GetComponent<PostProcessingBehaviour>().profile.depthOfField.settings;
            VignetteModel.Settings vignette = camera.GetComponent<PostProcessingBehaviour>().profile.vignette.settings;


            dof.aperture = 32f;

            vignette.intensity = 0.17f;
            vignette.smoothness = 0.2f;


            if (supressed)//Apply Supressed DoF
            {
                dof.aperture = 6;
                dof.focusDistance = 0.3f;

                vignette.intensity = 0.34f;
                vignette.smoothness = 0.85f;
            }

            camera.GetComponent<PostProcessingBehaviour>().profile.depthOfField.settings = dof;
            camera.GetComponent<PostProcessingBehaviour>().profile.vignette.settings = vignette;
        }

        private void applyFoV()
        {
            float resultFOV = normalFoV;
            float currentFOV = camera.fieldOfView;

            if (aiming)
            {
                resultFOV = aimingFoV;
            }

            //Smooth
            resultFOV = Mathf.Lerp(currentFOV, resultFOV, Time.deltaTime * fovChangeSpeed);

            //Apply FOV 
            camera.fieldOfView = resultFOV;
            weaponCamera.fieldOfView = resultFOV;
        }
    }
}