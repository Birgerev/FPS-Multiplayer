using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstPersonLayer : MonoBehaviour
{
    public bool assignToChildren = false;

    private CharacterModel model;

    private int defaultLayer = 0;

    // Start is called before the first frame update
    void Start()
    {
        model = GetComponentInParent<CharacterModel>();
        defaultLayer = gameObject.layer;
    }

    // Update is called once per frame
    void Update()
    {
        //Only change layers if the character model is in first person mode
        if (model.firstPerson)
        {
            gameObject.layer = 12;

            if (assignToChildren)
            {
                setLayerRecursively(gameObject, 12);
            }
        }
        else
        {
            gameObject.layer = defaultLayer;

            if (assignToChildren)
            {
                setLayerRecursively(gameObject, defaultLayer);
            }
        }
    }


    void setLayerRecursively(GameObject obj, int newLayer)
    {
        if (null == obj)
        {
            return;
        }

        obj.layer = newLayer;

        foreach (Transform child in obj.transform)
        {
            if (null == child)
            {
                continue;
            }
            setLayerRecursively(child.gameObject, newLayer);
        }
    }

}
