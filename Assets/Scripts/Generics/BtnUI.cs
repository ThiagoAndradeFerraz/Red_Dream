using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BtnUI : MonoBehaviour
{
    
    public void BtnTalkClick()
    {
        UImanager.Instance.ShowUI(UIState.TALK, true);
        //Debug.Log("talking...");
    }

    public void BtnInvClick()
    {
        //Debug.Log("using inventory...");
        UImanager.Instance.ShowUI(UIState.INVENTORYINTERACT, true);
    }

    public void BtnLeaveClick()
    {
        //Debug.Log("leaving...");
        StateMng.Instance.SetInteract1(" ", 0); // Dummy values
        //UImanager.Instance.ShowUI(UIState.INTERACTMENU1, false);
    }

}
