using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using net.bigdog.game.player;

public class Bullet : MonoBehaviour
{
    
    public string calibre;
    public float speed;
    public int ownerId = 0;
    private float maxage = 4;
    public GameObject hitEffect;

    private Vector3 startPosition;

    public AnimationCurve damageCurve;

    private Rigidbody rb;

    public GameObject casing;

    void Start() {
        //      TODO
        //GameObject obj = Instantiate(casing);
        //casing.transform.position = transform.position;
        //casing.transform.rotation = transform.rotation;

        //obj.GetComponent<Casing>();
        startPosition = transform.position;

        rb = GetComponent<Rigidbody>();
        rb.AddForce(transform.forward*speed);
        rb.useGravity = false;

        //Destroy object if it never hit anything
        Destroy(gameObject, maxage);
    }

    void FixedUpdate()
    {
        Vector3 gravity = 9.81f * Vector3.down;
        rb.AddForce(gravity, ForceMode.Force);
    }

    void OnCollisionEnter(Collision col)
    {
        GameObject obj = Instantiate(hitEffect);
        obj.transform.position = col.contacts[0].point;
        //TODO COLISSION ROTATION
        Destroy(obj, 1);

        float damage = damageCurve.Evaluate(Vector3.Distance(startPosition, transform.position));
        
        if (col.gameObject.GetComponent<Hitbox>() != null && damage > 0)
            col.gameObject.GetComponent<Hitbox>().hit(damage, ownerId);


        Destroy(gameObject);
    }
}
