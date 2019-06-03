using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using net.bigdog.game.player;

public class InventoryManager : MonoBehaviour
{
    public List<Item> items = new List<Item>(4);

    private int lastFrameNumpad = -1;

    public int selected = 0;

    public void Swap(int indexFrom, int indexTo)
    {
        //swap places in array
        Item fromItem = items[indexFrom];
        Item toItem = items[indexTo];

        items[indexTo] = fromItem;
        items[indexFrom] = toItem;
    }

    public void Start()
    {
        //Default debug items
        items[0] = ItemManager.instance.items[0];
        items[1] = ItemManager.instance.items[1];
        items[2] = ItemManager.instance.items[1];

        //select slot 0
        Select(0);
    }

    private void SyncInventory()
    {

    }

    private void Update()
    {
        Player player = GetComponent<Player>();

        if (player.networkInstance == null) //If network instance isn't null
            return;

        if (player.networkInstance.input.lastNumpad-1 != selected)  //If the new input isn't the same as what is selected
            if(lastFrameNumpad != player.networkInstance.input.lastNumpad)  //Check wheter the press is new
                Select(player.networkInstance.input.lastNumpad-1);

        lastFrameNumpad = player.networkInstance.input.lastNumpad;
    }

    public void Select(int index)
    {
        //return if new item index is outside our item list
        if (index < 0 || index > items.Count)
            return;

        //Save previous weapon state to inventory
        if(GetComponent<RuntimeItem>() != null)
            items[selected] = GetComponent<RuntimeItem>().item;
        
        //select new item
        selected = index;
        
        //Create a RuntimeItem class
        ItemManager.instance.createInstance(gameObject, items[index]);
    }
}