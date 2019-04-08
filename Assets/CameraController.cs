using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;
using UnityEngine;

public class CameraController : MonoBehaviour {

    private const float normalFoV = 60;

    public static float aimingFoV = 45;
    public static bool aiming = false;

    private Camera camera;
    private float cameraZoomSpeed = 10f;

    public Animator animator;

    public Player player;

    // Use this for initialization
    void Start () {
        camera = GetComponent<Camera>();
        animator = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
        if (player == null)
            player = GetComponentInParent<Player>();

        if (player.networkInstance != null)
        {
            animator.SetBool("crouching", player.networkInstance.input.crouch);
        }

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
        }
    }
}
