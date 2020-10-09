using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface INpc
{
    void ReceiveObj(string idObj);
    string GetName();
    int GetConvNbr();
}

public abstract class NPC1 : Interactive
{
    [SerializeField] protected string npcName;   // NPC Name...
    [SerializeField] protected string descNextDialog;
    [SerializeField] protected int convNumb = 1; // Conversation number...


    // Start is called before the first frame update
    /*void Start()
    {
        
    }*/

    // Update is called once per frame
    void Update()
    {
        CheckDistance();
    }

    /*

    protected override void Interact()
    {
        //Debug.Log("interagiu!");
        StateMng.Instance.SetInteract1(npcName, convNumb);
    }

    */

    //public abstract void ReceiveObj(string idObj);

    public void KillNpc()
    {
        //UImanager.Instance.ShowUI(UIState.TALK, true);
        Destroy(gameObject);
    }
}
