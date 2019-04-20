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

        for (int i = 0; i < items.Count; i++)
        {
            items[i].id = i;
        }
    }

    public RuntimeItem createInstance(GameObject obj, int id)
    {
        if (id < 0 || id > items.Count)
            return null;

        print("weapon: " + id);

        //Create and initialize a RuntimeWeapon
        RuntimeItem item;

        if (obj.GetComponent<RuntimeItem>() != null)
            Destroy(obj.GetComponent<RuntimeItem>());

        System.Type type = System.Type.GetType(items[id].runtimeScript);
        
        item = (RuntimeItem)obj.AddComponent(type);

        item.item = items[id];
        print("item: "+ item.item.Name);


        print("item: " + item.gameObject.name);

        return item;
    }
}
