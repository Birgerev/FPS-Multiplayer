using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterModel : MonoBehaviour {

    public Animator armAnimator;
    public SpineRotation spineRotator;
    public GameObject mainCamera;
    public GameObject weaponModelHolder;

    public bool armAim = false;

    // Use this for initialization
    void Start () {
        gameObject.name = "character_model";
	}
	
	// Update is called once per frame
	void Update () {
        armAnimator.SetBool("aiming", armAim);

    }

    public void DisableCamera()
    {
        print("disable");
        mainCamera.SetActive(false);
    }

    public void Quickdraw()
    {
        armAnimator.SetTrigger("quickdraw");
    }
}
