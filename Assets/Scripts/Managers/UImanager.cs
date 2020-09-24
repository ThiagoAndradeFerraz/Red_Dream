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
    private GameObject intrPanel1; // Interaction Panel 1
    [SerializeField] private Transform[] btnIntrPanel1 = new Transform[3];    // Interaction Panel 1 buttons 
    [SerializeField] private Transform[] txtBtnIntrPanel1 = new Transform[3]; // Interaction Panel 1 button texts 

    [SerializeField] private Transform talkPanel, imgTalkL, imgTalkR, txtNpcNameL, txtNpcNameR, txtTalk; // Conversation game objects




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
    }

    // ----------------------------------------------------

    public void ShowUI(UIState uiState, bool command)
    {
        switch (uiState)
        {
            case UIState.PAUSE1: // Screen 1 of Pause Menu
                ShowBackgroundImg(command);
                ShowPanelAndBTN(ref pause1Buttons, ref pause1ButtonsTXT, command); // Showing the elements of this screen
                break;

            case UIState.NOTIFICATION: // Notification pop-up
                ShowPanelAndTXT(ref notificationPanel, ref txtNotificationArray, command);
                break;

            case UIState.INTERACTMENU1: // Interaction Menu 1
                bcgImgIntr.GetComponent<Image>().enabled = command;
                intrPanel1.GetComponent<Image>().enabled = command;
                ShowPanelAndBTN(ref btnIntrPanel1, ref txtBtnIntrPanel1, command);
                ShowUI(UIState.NOTIFICATION, false); // Hiding notification if activated
                break;

            case UIState.TALK:
                ShowUI(UIState.INTERACTMENU1, false); // Hiding previous menu
                ShowDialogueUI(command);
                break;

        }
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
        intrPanel1 = GameObject.FindGameObjectWithTag("optionsPanel");
        bcgImgIntr = GameObject.FindGameObjectWithTag("bcgImgIntr");
        talkPanel = GameObject.FindGameObjectWithTag("talkPanel").GetComponent<Transform>();
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
        btnIntrPanel1[0] = intrPanel1.GetComponent<Transform>().GetChild(0);          // Talk Btn
        txtBtnIntrPanel1[0] = btnIntrPanel1[0].GetComponent<Transform>().GetChild(0); // Talk Text

        btnIntrPanel1[1] = intrPanel1.GetComponent<Transform>().GetChild(1);          // Inventory Btn
        txtBtnIntrPanel1[1] = btnIntrPanel1[1].GetComponent<Transform>().GetChild(0); // Inventory Text
        
        btnIntrPanel1[2] = intrPanel1.GetComponent<Transform>().GetChild(2);          // Leave Btn
        txtBtnIntrPanel1[2] = btnIntrPanel1[2].GetComponent<Transform>().GetChild(0); // Leave Text
        // ***********************

        // Dialogue elements *****
        imgTalkL = talkPanel.GetChild(0);
        imgTalkR = talkPanel.GetChild(1);
        txtNpcNameL = talkPanel.GetChild(2);
        txtNpcNameR = talkPanel.GetChild(3);
        txtTalk = talkPanel.GetChild(4);
        // ***********************




    }

    // ----------------------------------------------------
}
