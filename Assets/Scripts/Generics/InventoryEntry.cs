using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryEntry 
{
    private string id; // Ex: SWORD_ID, CUP_ID...
    private string imgName; // Name of the image to be loaded

    public InventoryEntry(string itemName) 
    {
        id = itemName.ToUpper() + "_ID";
        imgName = itemName.ToUpper();
    }

    public string getId()
    {
        return id;
    }
    
    public string getImgName()
    {
        return imgName;
    }
}