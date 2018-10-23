using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AimController : MonoBehaviour {

    public static bool aiming = false;

    public Animator anim;

    void Update () {
        anim.SetBool("aiming", aiming);
	}
}
