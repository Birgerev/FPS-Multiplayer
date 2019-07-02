using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using net.bigdog.game.player;

public class InventoryManager : MonoBehaviour
{
    public const int inventorySize = 4;

    public List<Item> items = new List<Item>(inventorySize);

    private int lastFrameNumpad = -1;

    public int selected = 0;

    private float syncPriorityLoopTime = 2;
    private bool hasSyncedThisFrame = false;

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
        StartCoroutine(SyncPriorityDataLoop());

        //Default debug items
        items[0] = (Item)ItemManager.instance.items[0].Clone();
        items[1] = (Item)ItemManager.instance.items[2].Clone();
        items[2] = (Item)ItemManager.instance.items[2].Clone();
        items[3] = (Item)ItemManager.instance.items[1].Clone();

        //select slot 0
        Select(0);
    }

    IEnumerator SyncPriorityDataLoop()
    {
        while (true)
        {
            SyncInventoryPriority();
            yield return new WaitForSeconds(syncPriorityLoopTime);
        }
    }
    
    private void SyncInventoryPriority()
    {
        PlayerInstance instance = GetComponent<Player>().networkInstance;
        if (!instance.isServer)
            return;

        int slot = 0;
        foreach (Item item in items)
        {
            if (item.priorityData.reloading)
                return;
            
            instance.RpcSyncInventoryPriorityData(item.priorityData, slot);
            slot++;
        }

        if (GetComponent<RuntimeItem>() != null)
            instance.RpcSyncRuntimeItemPriorityData(GetComponent<RuntimeItem>().item.priorityData);

        //TODO SyncRuntimeItemPriorityData()
    }

    private void SyncInventory()
    {
        PlayerInstance instance = GetComponent<Player>().networkInstance;
        if (!instance.isServer)
            return;

        int slot = 0;
        foreach(Item item in items)
        {
            if (item.priorityData.reloading)
                return;
            instance.RpcSyncInventoryItem(item.id, slot);
            slot++;
        }
        SyncInventoryPriority();
        hasSyncedThisFrame = true;
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
        hasSyncedThisFrame = false;
    }

    public void Select(int index)
    {
        //return if new item index is outside our item list
        if (index < 0 || index > items.Count)
            return;

        //Save previous weapon state to inventory
        if(GetComponent<RuntimeItem>() != null)
            items[selected] = (Item)GetComponent<RuntimeItem>().item.Clone();

        print("select " + index);

        //select new item
        selected = index;
        
        //Create a RuntimeItem class
        ItemManager.instance.createInstance(gameObject, items[index]);
        if(!hasSyncedThisFrame)
            SyncInventory();
    }
}

/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using net.bigdog.game.player;

public class InventoryManager : MonoBehaviour
{
    public const int inventorySize = 4;

    public List<Item> items = new List<Item>(inventorySize);

    private int lastFrameNumpad = -1;

    public int selected = 0;

    private float syncPriorityLoopTime = 5;
    private float syncItemsLoopTime = 20;

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
        StartCoroutine(SyncPriorityDataLoop());
        StartCoroutine(SyncItemsLoop());

        //Default debug items
        items[0] = (Item)ItemManager.instance.items[0].Clone();
        items[1] = (Item)ItemManager.instance.items[2].Clone();
        items[2] = (Item)ItemManager.instance.items[2].Clone();
        items[3] = (Item)ItemManager.instance.items[1].Clone();

        //select slot 0
        Select(0);
    }

    IEnumerator SyncPriorityDataLoop()
    {
        while (true)
        {
            SyncInventoryPriority();
            yield return new WaitForSeconds(syncPriorityLoopTime);
        }
    }

    IEnumerator SyncItemsLoop()
    {
        while (true)
        {
            SyncInventory();
            yield return new WaitForSeconds(syncItemsLoopTime);
        }
    }

    private void SyncInventoryPriority()
    {
        PlayerInstance instance = GetComponent<Player>().networkInstance;
        if (!instance.isServer)
            return;

        instance.RpcSyncInventorySlot(selected);
        
        for (int slot = 0; slot < inventorySize; slot++)
        {
            if(!items[slot].priorityData.reloading)
                instance.RpcSyncInventoryPriorityData(items[slot].priorityData, slot);
            slot++;
        }
    }

    private void SyncInventory()
    {
        PlayerInstance instance = GetComponent<Player>().networkInstance;
        if (!instance.isServer)
            return;

        int slot = 0;
        foreach(Item item in items)
        {
            if (!item.priorityData.reloading)
                instance.RpcSyncInventoryItem(item.id, slot);
            slot++;
        }
        SyncInventoryPriority();
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
            items[selected] = (Item)GetComponent<RuntimeItem>().item.Clone();

        print("select " + index);

        //select new item
        selected = index;
        
        //Create a RuntimeItem class
        ItemManager.instance.createInstance(gameObject, items[index]);
    }
}
*/