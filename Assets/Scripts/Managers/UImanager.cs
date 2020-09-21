using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UImanager : MonoBehaviour
{
    // ***********
    // UI MANAGER
    // ***********

    // Pause 1
    [SerializeField]
    private GameObject bcgImg; // Background image
    [SerializeField]
    private GameObject pausePanel1;
    [SerializeField]
    private Transform[] pause1Buttons = new Transform[2];
    [SerializeField]
    private Transform[] pause1ButtonsTXT = new Transform[3];

    // Notification panel
    private GameObject notificationPanel; // Panel
    private Transform[] txtNotificationArray = new Transform[2]; // Notification texts



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
        bcgImg = GameObject.FindGameObjectWithTag("bcgImg");
        pausePanel1 = GameObject.FindGameObjectWithTag("pausePanel1");
        notificationPanel = GameObject.FindGameObjectWithTag("notificationPanel");
        
        GetElements();
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // ----------------------------------------------------

    public void ShowNotificationUI(bool command)
    {
        notificationPanel.GetComponent<Image>().enabled = command;

        for(int i = 0; i < txtNotificationArray.Length; i++)
        {
            txtNotificationArray[i].gameObject.SetActive(command);
            txtNotificationArray[i].gameObject.GetComponent<TxtObj>().ShowText(command);
        }
    }


    public void ShowPauseUI1(bool command) // Panel 1
    {
        
        ActivateBcgImg(command);

        for (int i = 0; i < pause1Buttons.Length; i++)
        {
            pause1Buttons[i].gameObject.SetActive(command);
            pause1ButtonsTXT[i].gameObject.GetComponent<TxtObj>().ShowText(command);
        }
    }    

    // Used in SetPause1UI
    private void ActivateBcgImg(bool command)
    {
        bcgImg.GetComponent<Image>().enabled = command;
    }

    // Used in Start method
    private void GetElements()
    {
        // Pause buttons 1 ***

        pause1Buttons[0] = pausePanel1.GetComponent<Transform>().GetChild(0);         // Continue Btn
        pause1ButtonsTXT[0] = pause1Buttons[0].GetComponent<Transform>().GetChild(0); // Continue Text

        pause1Buttons[1] = pausePanel1.GetComponent<Transform>().GetChild(1);         // Settings Btn
        pause1ButtonsTXT[1] = pause1Buttons[1].GetComponent<Transform>().GetChild(0); // Continue Text

        pause1Buttons[2] = pausePanel1.GetComponent<Transform>().GetChild(2);         // Quit Btn
        pause1ButtonsTXT[2] = pause1Buttons[2].GetComponent<Transform>().GetChild(0); // Continue Text

        // ***

        // Notification texts ***
        txtNotificationArray[0] = notificationPanel.GetComponent<Transform>().GetChild(0); // Name Text
        txtNotificationArray[1] = notificationPanel.GetComponent<Transform>().GetChild(1); // Interaction Text

        // ***

    }

    // ----------------------------------------------------
    


}
