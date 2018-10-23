using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;
using UnityEngine;

public class CameraController : NetworkBehaviour {

    private const float normalFoV = 60;

    public static float aimingFoV = 45;
    public static bool aiming = false;

    private Camera camera;
    private float cameraZoomSpeed = 10f;

    public Animator animator;

    // Use this for initialization
    void Start () {
        camera = GetComponent<Camera>();
	}
	
	// Update is called once per frame
	void Update () {
        animator.SetBool("aiming", aiming);

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
