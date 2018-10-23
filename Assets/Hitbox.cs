using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hitbox : MonoBehaviour
{

    public float damageMultiplier = 1;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void hit(float damage)
    {
        print("hitbox: " + damage * damageMultiplier);
        gameObject.GetComponentInParent<Player>().TakeDamage(damage * damageMultiplier);
    }

    void OnCollisionEnter(Collision col)
    {
        print(col.gameObject.name);
    }
}
