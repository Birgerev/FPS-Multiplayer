using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Casing : MonoBehaviour {

    static int maxCasings = 50;
    static float rigidbodyMaxAge = 3;
    
    static int totalAmount = 0;

    // Use this for initialization
    void Start () {
        Rigidbody rb = GetComponent<Rigidbody>();
        float torque = 20;

        rb.AddRelativeForce(new Vector3(Random.Range(-20f, 20f), Random.Range(50f, 75f), Random.Range(-100f, -60f)));
        rb.AddRelativeTorque(new Vector3(Random.Range(-torque, torque), Random.Range(-torque, torque), Random.Range(-torque, torque)));

        totalAmount++;
        Destroy(GetComponent<Rigidbody>(), rigidbodyMaxAge);

        StartCoroutine(loop());
    }

    IEnumerator loop()
    {
        while (true)
        {
            if (totalAmount > maxCasings)
                if (Random.Range(0, maxCasings) < maxCasings/5)
                    destroy();

            yield return new WaitForSeconds(2);
        }
    }

    public void destroy()
    {
        totalAmount--;
        Destroy(gameObject);
    }
}
