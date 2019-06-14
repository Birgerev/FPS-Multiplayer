using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterModel : MonoBehaviour
{
    public Animator characterAnimator;
    public Animator armAnimator;
    public SpineRotation spineRotator;
    public GameObject mainCamera;
    public GameObject weaponModelHolder;

    public bool ready = false;
    public bool aim = false;

    public bool firstPerson = false;
    public bool rotateSpine = false;

    public float pitch;

    public Item item;

    // Use this for initialization
    void Start()
    {
        gameObject.name = "character_model";
    }

    // Update is called once per frame
    void Update()
    {
        if (aim)
            ready = true;

        armAnimator.SetBool("aiming", aim);

        rotateSpine = !firstPerson;
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
