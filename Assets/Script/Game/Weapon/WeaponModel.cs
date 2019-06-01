using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponModel : MonoBehaviour {

    public bool cocked = false;

    Animator anim;

    public GameObject muzzle;
    public GameObject muzzleEffectPrefab;
    public GameObject muzzleFlashPrefab;


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

        //Muzzle Particles
        GameObject muzzleEffect = Instantiate(muzzleEffectPrefab);

        muzzleEffect.transform.parent = transform;
        muzzleEffect.transform.position = muzzle.transform.position;
        muzzleEffect.transform.localRotation = Quaternion.identity;
        muzzleEffect.transform.localScale = new Vector3(1, 1, 1);
        Destroy(muzzleEffect, 4f);

        //Muzzle flash
        GameObject muzzleFlash = Instantiate(muzzleFlashPrefab);

        muzzleFlash.transform.parent = transform;
        muzzleFlash.transform.position = muzzle.transform.position;
        muzzleFlash.transform.localScale = new Vector3(1, 1, 1);
        Destroy(muzzleFlash, 0.05f);
    }
}
