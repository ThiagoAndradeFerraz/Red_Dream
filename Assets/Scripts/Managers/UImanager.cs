using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UImanager : MonoBehaviour
{
    // ***********
    // UI MANAGER
    // ***********

    [SerializeField]
    private GameObject bcgImg; // Background image
    [SerializeField]
    private GameObject pausePanel1;
    [SerializeField]
    private Transform[] pause1Buttons = new Transform[2];
    [SerializeField]
    private Transform[] pause1ButtonsTXT = new Transform[3];

    // Pause 1
    [SerializeField]
    private TextAsset textFileP1;
    private string strFileP1;

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
        GetButtonsP1();
        //LoadBtnTexts(); // Loading texts
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // ----------------------------------------------------

    public void SetPause1UI(bool command) // Panel 1
    {
        ActivateBcgImg(command);

        for (int i = 0; i < pause1Buttons.Length; i++)
        {
            pause1Buttons[i].gameObject.SetActive(command);
            pause1ButtonsTXT[i].gameObject.GetComponent<TxtObj>().ShowText(command);
        }




        /*
        pause1Buttons[0].gameObject.SetActive(command);
        pause1ButtonsTXT[0].gameObject.GetComponent<Text>().text = GetLocalizedValue("CONTInNUE_ID");

        pause1Buttons[1].gameObject.SetActive(command);
        pause1ButtonsTXT[1].gameObject.GetComponent<Text>().text = GetLocalizedValue("SETTINGS_ID");

        pause1Buttons[2].gameObject.SetActive(command);
        pause1ButtonsTXT[2].gameObject.GetComponent<Text>().text = GetLocalizedValue("QUIT_ID");
        */
    }

    private string GetLocalizedValue(string key)
    {
        return LangMng.Instance.GetValueMenu(key);
    }

    

    // Used in SetPause1UI
    private void ActivateBcgImg(bool command)
    {
        bcgImg.GetComponent<Image>().enabled = command;
    }

    // Used in Start method
    private void GetButtonsP1()
    {
        pause1Buttons[0] = pausePanel1.GetComponent<Transform>().GetChild(0);         // Continue Btn
        pause1ButtonsTXT[0] = pause1Buttons[0].GetComponent<Transform>().GetChild(0); // Continue Text

        pause1Buttons[1] = pausePanel1.GetComponent<Transform>().GetChild(1);         // Settings Btn
        pause1ButtonsTXT[1] = pause1Buttons[1].GetComponent<Transform>().GetChild(0); // Continue Text

        pause1Buttons[2] = pausePanel1.GetComponent<Transform>().GetChild(2);         // Quit     Btn
        pause1ButtonsTXT[2] = pause1Buttons[2].GetComponent<Transform>().GetChild(0); // Continue Text

    }

    // ----------------------------------------------------






}
