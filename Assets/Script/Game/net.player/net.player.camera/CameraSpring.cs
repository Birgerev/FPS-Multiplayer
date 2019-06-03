using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSpring : MonoBehaviour
{
    public Vector3 velocity = Vector3.zero;

    public float accelerationValue = 0.05f;
    public float frictionValue = 0.03f;

    public float velocityMultiplier = 0.1f;

    public float maxVelocity = 3f;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //round to 0 if velocity is unoticable
        if (velocity.magnitude <= 0.001f && transform.localPosition.magnitude < 0.01f)
        {
            velocity = Vector3.zero;
            return;
        }        

        if(frictionValue > 0.9f)
        {
            Debug.LogError("Camera spring friction can't be greater than 1!");
            return;
        }

        if (velocity.magnitude > maxVelocity)
            velocity = velocity.normalized * maxVelocity;


        //Apply last frames calculated velocity to camera
        transform.localPosition += velocity;

        //add inverted velocity, so that it starts going in the opposite direction
        velocity += (-(transform.localPosition * accelerationValue));

        //Apply friction, so that it eventually will be lost
        velocity += -(velocity * frictionValue);

    }
}
