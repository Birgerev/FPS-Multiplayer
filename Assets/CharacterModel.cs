using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterModel : MonoBehaviour
{
    public Animator characterAnimator;
    public Animator headAnimator;
    public Animator armAnimator;
    public SpineRotation spineRotator;
    public GameObject mainCamera;
    public GameObject weaponModelHolder;

    public bool armAim = false;
    public bool headTilt = false;

    public bool firstPerson = false;

    // Use this for initialization
    void Start()
    {
        gameObject.name = "character_model";
    }

    // Update is called once per frame
    void Update()
    {
        armAnimator.SetBool("aiming", armAim);
        headAnimator.SetBool("tilting", headTilt);

    }

    public void DisableCamera()
    {
        mainCamera.SetActive(false);
    }

    public void Quickdraw()
    {
        //TODO armAnimator.SetTrigger("quickdraw");
    }
}
