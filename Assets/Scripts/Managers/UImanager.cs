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
    private Transform[] pause1Buttons;

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
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetPause1UI(bool command) // Panel 1
    {
        ActivateBcgImg(command);
        
        for(int i = 0; i < 3; i++)
        {
            pause1Buttons[i].gameObject.SetActive(command);
        }
    }

    private void ActivateBcgImg(bool command)
    {
        bcgImg.GetComponent<Image>().enabled = command;
    }

    private void GetButtonsP1()
    {
        pause1Buttons[0] = pausePanel1.GetComponent<Transform>().GetChild(0); // Continue
        pause1Buttons[1] = pausePanel1.GetComponent<Transform>().GetChild(1); // Settings
        pause1Buttons[2] = pausePanel1.GetComponent<Transform>().GetChild(2); // Quit
    }
}
