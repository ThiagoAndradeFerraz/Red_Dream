using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BtnClickManager : MonoBehaviour
{
    // *************************************************************************************
    // BTN CLICK MANAGER: Take care of the click methods of some buttons
    // *************************************************************************************








    // SINGLETON ***************************************************************************
    public static BtnClickManager Instance { get; private set; }

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
    // *************************************************************************************

    // FIRST INTERACTION MENU WITH NPC *****************************************************
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
    // *************************************************************************************

    // INVENTORY MENU **********************************************************************
    public void SelectItemClick()
    {
        Debug.Log("ITEM SELECTED: ");
        UImanager.Instance.LetConfirmBtnClickable(false);
        UImanager.Instance.LetSlotsClickable(true);
        UImanager.Instance.ResetHighlightSlots();
    }

    public void CancelSelectionClick()
    {
        Debug.Log("SELECTION CANCELED!");
        UImanager.Instance.LetSlotsClickable(true);
        UImanager.Instance.ResetHighlightSlots();
        UImanager.Instance.LetConfirmBtnClickable(false);
    }
    // *************************************************************************************

}
