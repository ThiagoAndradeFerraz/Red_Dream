using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalMng : MonoBehaviour
{
    // ***************
    // GLOBAL MANAGER
    // ***************

    // Inventory
    public List<InventoryEntry> inventoryList = new List<InventoryEntry>();

    public static GlobalMng Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void AddItemInventory(string itemName)
    {
        inventoryList.Add(new InventoryEntry(itemName));
    }

    public void RemoveItemInventory(string idItem) // WILL REMOVE ALL ITENS WITH THE GIVEN ID!
    {
        inventoryList.RemoveAll(x => x.getId() == idItem);
    }

}


