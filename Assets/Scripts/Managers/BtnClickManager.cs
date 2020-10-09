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
        StateMng.Instance.GoTalkState();
    }

    public void BtnInvClick()
    {
        //Debug.Log("using inventory...");
        StateMng.Instance.GoInventory();
    }

    public void BtnLeaveClick()
    {
        //Debug.Log("leaving...");
        //StateMng.Instance.SetInteract1(" ", " "); // Dummy values
        //UImanager.Instance.ShowUI(UIState.INTERACTMENU1, false);

        StateMng.Instance.stateNow = GameState.EXPLORATION;
        UImanager.Instance.ChangeUI(GameState.INTERACTION1, false);

    }
    // *************************************************************************************

    // INVENTORY MENU **********************************************************************
    public void SelectItemClick() // Make interaction between NPC and selected object
    {
        //Debug.Log("ITEM SELECTED: ");
        UImanager.Instance.LetConfirmBtnClickable(false);
        UImanager.Instance.LetSlotsClickable(true);
        UImanager.Instance.ResetHighlightSlots();

        // MUST BE IN A IF ELSE STRUCTURE!
        InvAndNPCmng.Instance.GiveObject();




    }

    public void CancelSelectionClick()
    {
        //Debug.Log("SELECTION CANCELED!");
        UImanager.Instance.LetSlotsClickable(true);
        UImanager.Instance.ResetHighlightSlots();
        UImanager.Instance.LetConfirmBtnClickable(false);   
    }
    // *************************************************************************************

    
}
