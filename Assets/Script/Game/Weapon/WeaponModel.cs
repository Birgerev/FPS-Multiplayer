using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponModel : MonoBehaviour {

    public bool cocked = false;

    Animator anim;



    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        anim.SetBool("cocked", cocked);
    }
    
    public void Shoot()
    {
        anim.SetTrigger("shoot");
    }
}
