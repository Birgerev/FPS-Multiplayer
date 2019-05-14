using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour{

    public float damage = 0;
    public string calibre;
    public float speed;
    public float gravityMultiplier;
    public GameObject owner;
    public float maxage = 8;
    public GameObject hitEffect;

    private Rigidbody rb;

    public GameObject casing;

    void Start() {
        //      TODO
        //GameObject obj = Instantiate(casing);
        //casing.transform.position = transform.position;
        //casing.transform.rotation = transform.rotation;

        //obj.GetComponent<Casing>();

        rb = GetComponent<Rigidbody>();
        rb.AddForce(transform.forward*speed);
        rb.mass = gravityMultiplier;
        rb.useGravity = false;

        //Destroy object if it never hit anything
        Destroy(gameObject, maxage);
    }

    void FixedUpdate()
    {
        Vector3 gravity = 9.81f * gravityMultiplier * Vector3.up;
        rb.AddForce(-gravity, ForceMode.Acceleration);
    }

    void OnCollisionEnter(Collision col)
    {
        GameObject obj = Instantiate(hitEffect);
        obj.transform.position = col.contacts[0].point;
        //TODO COLISSION ROTATION
        Destroy(obj, 1);
        
        if (col.gameObject.GetComponent<Hitbox>() != null)
            col.gameObject.GetComponent<Hitbox>().hit(damage);


        Destroy(gameObject);
    }
}
