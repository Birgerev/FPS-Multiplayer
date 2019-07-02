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

    public float radgrollPhysicsTime = 20;

    // Use this for initialization
    void Start()
    {
        gameObject.name = "character_model";

        ChangeRagdollMode(gameObject, false);
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

    public void ChangeRagdollMode(bool mode)
    {
        ChangeRagdollMode(gameObject, mode);
        
        GetComponentInChildren<SkinnedMeshRenderer>().updateWhenOffscreen = mode;

        if (mode)
        {
            firstPerson = false;

            Invoke("RagdollKinematic", radgrollPhysicsTime);
        }

    }

    public void ChangeRagdollMode(GameObject obj, bool mode)
    {
        if (obj.GetComponent<Rigidbody>() != null)
            obj.GetComponent<Rigidbody>().isKinematic = !mode;
        if (obj.GetComponent<Animator>() != null)
            obj.GetComponent<Animator>().enabled = !mode;
        if(mode)
            if (obj.name == "WeaponSlot" || obj.name == "MagazineSlot")
                Destroy(obj);


        //Pass by this function to children
        for (int i = 0; i < obj.transform.childCount; i++)
        {
            ChangeRagdollMode(obj.transform.GetChild(i).gameObject, mode);
        }
    }



    public void RagdollKinematic()
    {
        RagdollKinematic(gameObject);

        //GetComponentInChildren<SkinnedMeshRenderer>().updateWhenOffscreen = false;
    }

    public void RagdollKinematic(GameObject obj)
    {
        if (obj.GetComponent<Joint>() != null)
            Destroy(obj.GetComponent<Joint>());
        if (obj.GetComponent<Rigidbody>() != null)
            Destroy(obj.GetComponent<Rigidbody>());
        if (obj.GetComponent<Animator>() != null)
            Destroy(obj.GetComponent<Animator>());
        if (obj.GetComponent<Collider>() != null)
            Destroy(obj.GetComponent<Collider>());
        if (obj.GetComponent<net.bigdog.game.player.ItemArms>() != null)
            Destroy(obj.GetComponent<net.bigdog.game.player.ItemArms>());
        if (obj.GetComponent<CharacterModel>() != null)
            Destroy(obj.GetComponent<CharacterModel>());
        if (obj.GetComponent<WeaponModel>() != null)
            Destroy(obj.GetComponent<WeaponModel>());


        //Pass by this function to children
        for (int i = 0; i < obj.transform.childCount; i++)
        {
            RagdollKinematic(obj.transform.GetChild(i).gameObject);
        }
    }
}
