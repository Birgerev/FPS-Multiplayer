using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    //0 = Magazine, 1 = Right, 2 = Left, 3> = Belt
    public List<Item> items = new List<Item>(3 + 4);

    public int selected = 0;


    public void Swap(int indexFrom, int indexTo)
    {
        Item fromItem = items[indexFrom];
        Item toItem = items[indexTo];

        items[indexTo] = fromItem;
        items[indexFrom] = toItem;
    }

}
