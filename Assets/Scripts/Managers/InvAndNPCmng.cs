using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvAndNPCmng : MonoBehaviour
{
    // *************************
    // Inventory and NPC manager
    // *************************

    // Inventory related
    public List<InventoryEntry> inventoryList = new List<InventoryEntry>();

    // NPCs related
    public List<NPCEntry> npcStatusList = new List<NPCEntry>(); // list used for operations like save, load and others
    private Transform npcParent;
    [SerializeField] private List<Transform> npcGameObjList = new List<Transform>(); // Reference to the game objects of each npc

    // Used for dialogue operations
    public string npcName;
    public string descNextDialog; // Small description about the next file the system will have to load when needed
    public int conversationNumber;

    // ******************************************************************************************
    // SINGLETON
    // ******************************************************************************************

    public static InvAndNPCmng Instance { get; private set; }

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
    // ******************************************************************************************

    private void Start()
    {
        // ONLY FOR TEST!
        npcStatusList.Add(new NPCEntry("Valquiria", 1)); // Adding Valquiria NPC to the npc status list


        // Getting the NPC gameobjects in a list
        npcParent = GameObject.FindGameObjectWithTag("npcParent").GetComponent<Transform>();
        for (int i = 0; i < npcParent.childCount; i++)
        {
            npcGameObjList.Add(npcParent.GetChild(i));
        }


        //npcGameObjList.Add(npc)
        //Debug.Log(npcParent.childCount);

    }

    // ******************************************************************************************
    // INVENTORY
    // ******************************************************************************************

    public void AddItemInventory(string itemName)
    {
        inventoryList.Add(new InventoryEntry(itemName));
    }

    public void RemoveItemInventory(string idItem) // WILL REMOVE ALL ITENS WITH THE GIVEN ID!
    {
        inventoryList.RemoveAll(x => x.getId() == idItem);
    }

    // ******************************************************************************************
    // NPC 
    // ******************************************************************************************

    private int GetNpcIndex(string name) // Get the index of specific NPC in list
    {
        int index = 0;
        for(int i = 0; i < npcStatusList.Count; i++)
        {
            if(npcStatusList[i].GetName() == name)
            {
                index = i;
                break;
            }
        }
        return index;
    }

    public void AttackNpcInv() // Used when the NPC is attacked with a inventory object, like sword, or poison
    {
        int index = GetNpcIndex(npcName);
        npcStatusList[index].SetHealth(false, 100);
    }

    public void GiveObject()
    {
        for (int i = 0; i < npcGameObjList.Count; i++)
        {
            if (npcGameObjList[i].GetComponent<INpc>().GetName() == npcName)
            {
                npcGameObjList[i].GetComponent<INpc>().ReceiveObj("OBJTEST_ID");
                break;
            }
        }
    }


    public void AffectNpcBody() // Change NPC animation, destroy, etc...
    {
        // Killing NPC...
        for(int i = 0; i < npcGameObjList.Count; i++)
        {/*
            if (npcGameObjList[i].GetComponent<NPC1>().npcName == npcName)
            {
                npcGameObjList[i].GetComponent<NPC1>().KillNpc();
                break;
            }*/
        }
    }
}


