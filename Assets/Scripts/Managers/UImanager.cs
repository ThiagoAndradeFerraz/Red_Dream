using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum UIState
{
    PAUSE1,           // Screen 1 of pause menu
    NOTIFICATION,     // Notification when close to NPC or object
    TALK,             // Talking with a NPC
    INTERACTMENU1,     // Interact options with a NPC
    INVENTORYINTERACT // Inventory menu to use with a NPC
}

public class UImanager : MonoBehaviour
{
    // ***********
    // UI MANAGER
    // ***********

    public UIState currentUIstate;

    // Pause 1
    private GameObject bcgImg;     // Pause background image
    private GameObject bcgImgIntr; // Interaction background image

    private GameObject pausePanel1;
    private Transform[] pause1Buttons = new Transform[3];
    private Transform[] pause1ButtonsTXT = new Transform[3];

    // Notification panel
    private GameObject notificationPanel; // Panel
    private Transform[] txtNotificationArray = new Transform[2]; // Notification texts

    // Interaction panel 1
    private GameObject intr1Parent; // Interaction Panel 1
    private Transform intr1Panel;
    [SerializeField] private Transform[] btnIntrPanel1 = new Transform[3];    // Interaction Panel 1 buttons 
    [SerializeField] private Transform[] txtBtnIntrPanel1 = new Transform[3]; // Interaction Panel 1 button texts 
    private Transform imgNpc, txtNpcName;

    // Talk 
    [SerializeField] public Transform talkPanel, imgTalkL, imgTalkR, txtNpcNameL, txtNpcNameR, txtTalk; // Conversation game objects

    // Inventory
    private GameObject inventoryPanel;
    private Transform txtInvHeader, txtInvNpcName, imgInvNpc, btnSelectItem, txtBtnSelect, panelItens, btnCancelSelection;
    [SerializeField] private Transform[] slotsInventory = new Transform[9];

    public static UImanager Instance { get; private set; }

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

    // Start is called before the first frame update
    void Start()
    {
        GetParentElements();
        GetChildElements();

        // ONLY FOR TESTING!
        InvAndNPCmng.Instance.AddItemInventory("sword");
        InvAndNPCmng.Instance.AddItemInventory("magazine");
        InvAndNPCmng.Instance.AddItemInventory("paper");
    }

    // ----------------------------------------------------

    public void ChangeUI(GameState state, bool status)
    {

        Debug.Log(state);
        switch (state)
        {
            case GameState.PAUSED:
                PauseUI(status);
                break;

            case GameState.INTERACTION1:
                Intr1UI(status);
                break;

            case GameState.TALK:
                TalkUI(status);
                break;

            case GameState.INVENTORY:
                InventoryUI(status);
                break;

            default:
                // Doing nothing
                break;
        }
    }

    private void PauseUI(bool status)
    {
        ShowBackgroundImg(status);
        ShowPanelAndBTN(ref pause1Buttons, ref pause1ButtonsTXT, status);
    }

    private void Intr1UI(bool status)
    {
        bcgImgIntr.GetComponent<Image>().enabled = status;
        intr1Panel.gameObject.SetActive(status);
        
        //intr1Parent.GetComponent<Image>().enabled = status;
        //imgNpc.GetComponent<Image>().enabled = status;

        if (status)
        {
            imgNpc.GetComponent<Image>().sprite = Resources.Load<Sprite>(GetImgPath(InvAndNPCmng.Instance.npcName));
        }

        txtNpcName.gameObject.SetActive(status);
        txtNpcName.GetComponent<Text>().text = (status) ? InvAndNPCmng.Instance.npcName : " ";
        ShowPanelAndBTN(ref btnIntrPanel1, ref txtBtnIntrPanel1, status);
        //ShowUI(UIState.NOTIFICATION, false); // Hiding notification if activated
    }

    private void TalkUI(bool status)
    {
        ShowDialogueUI(status);
        if (status) { DialogueManager.Instance.StartDialogue(); }
        //dialogueMachine.StartDialogue(); // PAY ATTENTION HERE!!!!
    }

    private void InventoryUI(bool status)
    {
        ShowBackgroundImg(status);

        // Selection button
        btnSelectItem.gameObject.SetActive(status); // Button
        btnSelectItem.GetChild(0).GetComponent<TxtObj>().ShowText(status); // Button text

        txtInvHeader.gameObject.SetActive(status);
        txtInvHeader.GetComponent<TxtObj>().ShowText(status); // Header inventory

        imgInvNpc.gameObject.SetActive(status);
        imgInvNpc.GetComponent<Image>().enabled = status; // Activate img

        txtInvNpcName.gameObject.SetActive(status);

        if (status) // Only load image if the image is gonna be shown
        {
            imgInvNpc.GetComponent<Image>().sprite = Resources.Load<Sprite>(GetImgPath(InvAndNPCmng.Instance.npcName)); // Loading the img
            txtInvNpcName.GetComponent<Text>().text = InvAndNPCmng.Instance.npcName; // npc name text

            // PAY ATTENTION HERE IN CASE OF SOME ERRORS, I'M STILL NOT SURE ABOUT THE FOLLOWING LOOP
            int listLength = InvAndNPCmng.Instance.inventoryList.Count;
            for (int i = 0; i < slotsInventory.Length; i++)
            {
                if (i < listLength)
                {
                    slotsInventory[i].GetComponent<InventorySlot>().LoadInfo(i);
                }
                else
                {
                    slotsInventory[i].GetComponent<InventorySlot>().LetItEmpty();
                }
            }
        }
        else
        {
            txtInvNpcName.GetComponent<Text>().text = " ";
        }

        panelItens.gameObject.SetActive(status);

        btnCancelSelection.gameObject.SetActive(status);
        btnCancelSelection.GetChild(0).GetComponent<TxtObj>().ShowText(status);
    }


    // OLD SWITCH UI METHOD
    /*
    public void ShowUI(UIState uiState, bool command)
    {
        if(command)
        {
            currentUIstate = uiState;
        }

        switch (uiState)
        {
            case UIState.PAUSE1: // Screen 1 of Pause Menu
                ShowBackgroundImg(command);
                ShowPanelAndBTN(ref pause1Buttons, ref pause1ButtonsTXT, command); // Showing the elements of this screen
                break;

            // **********************************************************************************************************************

            case UIState.NOTIFICATION: // Notification pop-up
                ShowPanelAndTXT(ref notificationPanel, ref txtNotificationArray, command);
                break;

            // **********************************************************************************************************************

            case UIState.INTERACTMENU1: // Interaction Menu 1
                bcgImgIntr.GetComponent<Image>().enabled = command;
                intrPanel1.GetComponent<Image>().enabled = command;
                imgNpc.GetComponent<Image>().enabled = command;
                
                if (command)
                {
                    imgNpc.GetComponent<Image>().sprite = Resources.Load<Sprite>(GetImgPath(InvAndNPCmng.Instance.npcName));
                }
                
                txtNpcName.GetComponent<Text>().text = (command) ? InvAndNPCmng.Instance.npcName : " ";
                ShowPanelAndBTN(ref btnIntrPanel1, ref txtBtnIntrPanel1, command);
                ShowUI(UIState.NOTIFICATION, false); // Hiding notification if activated
                
                break;

            // **********************************************************************************************************************

            case UIState.TALK:
                UImanager.Instance.ShowUI(UIState.INVENTORYINTERACT, false); // If is true
                ShowUI(UIState.INTERACTMENU1, false); // Hiding previous menu
                ShowDialogueUI(command);
                if (command) { DialogueManager.Instance.StartDialogue(); }
                //dialogueMachine.StartDialogue(); // PAY ATTENTION HERE!!!!
                break;

            // **********************************************************************************************************************

            case UIState.INVENTORYINTERACT:

                // ---------------------------------------------------------------
                // ATTENTION!
                // THE BACK FUNCTION IS TREATED BY THE STATE MANAGER PAUSE METHOD!
                // ---------------------------------------------------------------

                ShowUI(UIState.INTERACTMENU1, false); // Hiding previous menu
                ShowBackgroundImg(command);
                // Selection button
                btnSelectItem.gameObject.SetActive(command); // Button
                btnSelectItem.GetChild(0).GetComponent<TxtObj>().ShowText(command); // Button text

                txtInvHeader.GetComponent<TxtObj>().ShowText(command); // Header inventory
                imgInvNpc.GetComponent<Image>().enabled = command; // Activate img
                
                if (command) // Only load image if the image is gonna be shown
                {
                    imgInvNpc.GetComponent<Image>().sprite = Resources.Load<Sprite>(GetImgPath(InvAndNPCmng.Instance.npcName)); // Loading the img
                    txtInvNpcName.GetComponent<Text>().text = InvAndNPCmng.Instance.npcName; // npc name text
                }
                else
                {
                    txtInvNpcName.GetComponent<Text>().text = " ";
                }

                panelItens.gameObject.SetActive(command);


                // LEMBRERETE! ESSE TRECHO SÓ PODE ROLAR QUANDO O COMMAND FOR TRUE!!!!
                // Load itens in slots...
                int listLength = InvAndNPCmng.Instance.inventoryList.Count;
                for(int i = 0; i < slotsInventory.Length; i++)
                {
                    if(i < listLength)
                    {
                        slotsInventory[i].GetComponent<InventorySlot>().LoadInfo(i);
                    }
                    else
                    {
                        slotsInventory[i].GetComponent<InventorySlot>().LetItEmpty();
                    }
                }

                btnCancelSelection.gameObject.SetActive(command);
                btnCancelSelection.GetChild(0).GetComponent<TxtObj>().ShowText(command);

                break;
        }
    }

    */

    // Inventory related methods -----------------------------------------------

    public void LetConfirmBtnClickable(bool command)
    {
        btnSelectItem.GetComponent<Button>().interactable = command;
        btnCancelSelection.GetComponent<Button>().interactable = command;
    }


    public void LetSlotsClickable(bool command)
    {
        for (int i = 0; i < slotsInventory.Length; i++)
        {
            if (command)
            {
                if(slotsInventory[i].GetComponent<InventorySlot>().GetId() != " ")
                {
                    slotsInventory[i].GetComponent<Button>().interactable = command;

                }
            }
            else
            {
                slotsInventory[i].GetComponent<Button>().interactable = command;
            }

            //slotsInventory[i].GetComponent<Button>().interactable = command;
        }
    }

    public void ResetHighlightSlots()
    {
        for (int i = 0; i < slotsInventory.Length; i++)
        {
            slotsInventory[i].GetComponent<Image>().color = Color.white;
        }
    }
    // -------------------------------------------------------------------------------





    private string GetImgPath(string charName)
    {
        // EXAMPLE: Arts/Characters/Cassandra/Cassandra_NEUTRAL
        string path = "Arts/Characters/" + charName + "/" + charName + "_NEUTRAL";
        return path;
    }

    private void ShowPanelAndBTN(ref Transform[] btnArray, ref Transform[] txtArray, bool command)
    {
        for(int i = 0; i < btnArray.Length; i++)
        {
            btnArray[i].gameObject.SetActive(command);
            txtArray[i].gameObject.GetComponent<TxtObj>().ShowText(command);
        }
    }

    private void ShowPanelAndTXT(ref GameObject panel, ref Transform[] txtArray, bool command)
    {
        panel.GetComponent<Image>().enabled = command;

        for (int i = 0; i < txtArray.Length; i++)
        {
            txtArray[i].gameObject.SetActive(command);
            txtArray[i].gameObject.GetComponent<TxtObj>().ShowText(command);
        }
    }

    private void ShowDialogueUI(bool command)
    {
        bcgImgIntr.GetComponent<Image>().enabled = command;

        imgTalkL.gameObject.SetActive(command);
        imgTalkR.gameObject.SetActive(command);
        txtNpcNameL.gameObject.SetActive(command);
        txtNpcNameR.gameObject.SetActive(command);
        txtTalk.gameObject.SetActive(command);
        /*
        imgTalkL.GetComponent<Image>().enabled = command;
        imgTalkR.GetComponent<Image>().enabled = command;
        txtNpcNameL.GetComponent<Text>().enabled = command;
        txtNpcNameR.GetComponent<Text>().enabled = command;
        txtTalk.GetComponent<Text>().enabled = command;
        */
        //  talkPanel, imgTalkL, imgTalkR, txtNpcNameL, txtNpcNameR, txtTalk;

    }



    // Show the background image
    private void ShowBackgroundImg(bool command)
    {
        bcgImg.GetComponent<Image>().enabled = command;
    }

    // Used in Start method
    private void GetParentElements()
    {
        bcgImg = GameObject.FindGameObjectWithTag("bcgImg");
        pausePanel1 = GameObject.FindGameObjectWithTag("pausePanel1");
        notificationPanel = GameObject.FindGameObjectWithTag("notificationPanel");
        intr1Parent = GameObject.FindGameObjectWithTag("optionsPanel");
        bcgImgIntr = GameObject.FindGameObjectWithTag("bcgImgIntr");
        talkPanel = GameObject.FindGameObjectWithTag("talkPanel").GetComponent<Transform>();
        inventoryPanel = GameObject.FindGameObjectWithTag("inventoryPanel");
    }

    // Used in Start method
    private void GetChildElements()
    {
        // Pause buttons 1 *******
        pause1Buttons[0] = pausePanel1.GetComponent<Transform>().GetChild(0);         // Continue Btn
        pause1ButtonsTXT[0] = pause1Buttons[0].GetComponent<Transform>().GetChild(0); // Continue Text

        pause1Buttons[1] = pausePanel1.GetComponent<Transform>().GetChild(1);         // Settings Btn
        pause1ButtonsTXT[1] = pause1Buttons[1].GetComponent<Transform>().GetChild(0); // Continue Text

        pause1Buttons[2] = pausePanel1.GetComponent<Transform>().GetChild(2);         // Quit Btn
        pause1ButtonsTXT[2] = pause1Buttons[2].GetComponent<Transform>().GetChild(0); // Continue Text
        // ***********************

        // Notification texts ****
        txtNotificationArray[0] = notificationPanel.GetComponent<Transform>().GetChild(0); // Name Text
        txtNotificationArray[1] = notificationPanel.GetComponent<Transform>().GetChild(1); // Interaction Text
        // ***********************

        // Interaction PANEL 1 ***
        intr1Panel = intr1Parent.GetComponent<Transform>().GetChild(0);

        btnIntrPanel1[0] = intr1Panel.GetComponent<Transform>().GetChild(0);          // Talk Btn
        txtBtnIntrPanel1[0] = btnIntrPanel1[0].GetComponent<Transform>().GetChild(0); // Talk Text

        btnIntrPanel1[1] = intr1Panel.GetComponent<Transform>().GetChild(1);          // Inventory Btn
        txtBtnIntrPanel1[1] = btnIntrPanel1[1].GetComponent<Transform>().GetChild(0); // Inventory Text
        
        btnIntrPanel1[2] = intr1Panel.GetComponent<Transform>().GetChild(2);          // Leave Btn
        txtBtnIntrPanel1[2] = btnIntrPanel1[2].GetComponent<Transform>().GetChild(0); // Leave Text

        imgNpc = intr1Panel.GetComponent<Transform>().GetChild(3);     // NPC img portrait
        txtNpcName = intr1Panel.GetComponent<Transform>().GetChild(4); // NPC text name
        // ***********************

        // Dialogue elements *****
        imgTalkL = talkPanel.GetChild(0);
        imgTalkR = talkPanel.GetChild(1);
        txtNpcNameL = talkPanel.GetChild(2);
        txtNpcNameR = talkPanel.GetChild(3);
        txtTalk = talkPanel.GetChild(4);
        // ***********************

        // Inventory elements ****
        txtInvHeader = inventoryPanel.GetComponent<Transform>().GetChild(0);
        txtInvNpcName = inventoryPanel.GetComponent<Transform>().GetChild(1);
        imgInvNpc = inventoryPanel.GetComponent<Transform>().GetChild(2);
        btnSelectItem = inventoryPanel.GetComponent<Transform>().GetChild(3);
        //txtBtnSelect = btnSelectItem.GetChild(0);
        panelItens = inventoryPanel.GetComponent<Transform>().GetChild(4);
        
        for(int i = 0; i < 9; i++)
        {
            slotsInventory[i] = panelItens.GetChild(i);
        }

        btnCancelSelection = inventoryPanel.GetComponent<Transform>().GetChild(5);
        // ***********************

    }

    // ----------------------------------------------------
}
