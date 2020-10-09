using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Valquiria : NPC1, INpc
{
    public int GetConvNbr()
    {
        return convNumb;
    }

    public string GetName()
    {
        return npcName;
    }

    public void ReceiveObj(string idObj)
    {

        //UImanager.Instance.ShowUI(UIState.TALK, true);
        Debug.Log("recebeu: " + idObj);
    }

    protected override void Interact()
    {
        StateMng.Instance.GoIntr1State(npcName, descNextDialog);
    }

    
}
