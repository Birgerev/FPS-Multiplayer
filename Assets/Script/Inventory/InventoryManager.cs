using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    //0 = Magazine, 1 = Right, 2 = Left, 3> = Belt
    public List<Item> items = new List<Item>(4);

    public int selected = 0;

    public bool reloadMode = false;


    public void Swap(int indexFrom, int indexTo)
    {
        Item fromItem = items[indexFrom];
        Item toItem = items[indexTo];

        items[indexTo] = fromItem;
        items[indexFrom] = toItem;
    }

    public void Start()
    {
        items[0] = ItemManager.instance.items[0];
        items[1] = ItemManager.instance.items[1];
        items[2] = ItemManager.instance.items[1];

        Select(0);
    }

    private void Update()
    {
        Player player = GetComponent<Player>();

        if (player.networkInstance != null)
            if (player.networkInstance.input.lastNumpad <= items.Count)
                if (player.networkInstance.input.lastNumpad-1 != selected)
                    Select(player.networkInstance.input.lastNumpad-1);
    }

    public void Select(int index)
    {
        if (index < 0 || index > items.Count)
            return;

        if (reloadMode)
        {
            GetComponent<RuntimeWeapon>().insertMagazine(items[index]);
            reloadMode = false;
            return;
        }


        selected = index;
        
        ItemManager.instance.createInstance(gameObject, items[index].id);
    }
}
