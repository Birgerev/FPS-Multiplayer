using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;
using UnityEngine;

public class ItemManager : MonoBehaviour
{
    public static ItemManager instance;

    public List<Item> items = new List<Item>();

    void Start()
    {
        instance = this;    //So that refering to class is made easy

        //Assign items in the list their id's based on index
        for (int i = 0; i < items.Count; i++)
        {
            items[i].id = i;
        }
    }

    public RuntimeItem createInstance(GameObject obj, int id)
    {
        //If id isn't in list, return null
        if (id < 0 || id > items.Count)
            return null;

        return createInstance(obj, items[id]);
    }

    public RuntimeItem createInstance(GameObject obj, Item item)
    {
        //Create and initialize a RuntimeWeapon
        RuntimeItem itemInstance;

        //Destroy previous RuntimeItems attached to the gameObject
        if (obj.GetComponent<RuntimeItem>() != null)
            Destroy(obj.GetComponent<RuntimeItem>());

        //Get the runtime script type
        System.Type type = System.Type.GetType(item.runtimeScript);

        //Attach it to the object
        itemInstance = (RuntimeItem)obj.AddComponent(type);

        //create a clone of the item, so that we're not just referencing the default state 
        //(will cause problems when the player changes the weapon (ie shoots, reloads))
        itemInstance.item = (Item)item.Clone();

        //return a reference to the newly created RuntimeItem
        return itemInstance;
    }
}
