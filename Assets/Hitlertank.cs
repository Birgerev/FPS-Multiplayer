﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hitlertank : MonoBehaviour
{
    public int Speed;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Rigidbody lol = GetComponent<Rigidbody>();
        lol.AddRelativeForce(new Vector3(0, 0, Speed));
    }
}
