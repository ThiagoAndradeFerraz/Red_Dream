using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameState
{
    PAUSED, EXPLORATION, COMBAT, INTERACTION1
}

public class StateMng : MonoBehaviour
{
    // **************
    // STATE MANAGER
    // **************

    [SerializeField]
    private GameState stateNow;
    [SerializeField]
    private GameState prevState;

    /*
    // used only in dialogue system ****
    public string npcName;
    public int conversationNumber; */
    // *********************************


    public static StateMng Instance { get; private set; }

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
        stateNow = GameState.EXPLORATION; // PAY ATTENTION TO THIS!!!! MAYBE IT NEEDS TO CHANGE LATER!!!!
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public GameState GetState()
    {
        return stateNow;
    }

    public void SetPause()
    {
        /*
        switch (UImanager.Instance.currentUIstate)
        {
            case UIState.INVENTORYINTERACT:
                UImanager.Instance.ShowUI(UIState.INVENTORYINTERACT, false);
                UImanager.Instance.ShowUI(UIState.INTERACTMENU1, true);
                break;

            case UIState.INVENTORYINTERACT
        }
        */


        if(UImanager.Instance.currentUIstate == UIState.INVENTORYINTERACT)
        {
            // ESC IS THE BACK BUTTON IN SOME SITUATIONS
            UImanager.Instance.ShowUI(UIState.INVENTORYINTERACT, false);
            UImanager.Instance.ShowUI(UIState.INTERACTMENU1, true);
        }
        else
        {
            SwitchState(GameState.PAUSED);
        }


    }

    public void SetCombat()
    {

    }

    public void SetExploration()
    {

    }

    public void SetInteract1(string name, string desc)
    {
        // Name and Number -> Used to load the right dialogue file acording to the time and NPC

        InvAndNPCmng.Instance.npcName = name;
        //InvAndNPCmng.Instance.conversationNumber = number;
        InvAndNPCmng.Instance.descNextDialog = desc;
        SwitchState(GameState.INTERACTION1);
    }

    private void SwitchState(GameState state)
    {
        if (stateNow != state)
        {
            prevState = stateNow;
            stateNow = state;
            UImanager.Instance.ShowUI(SelectUIState(state), true);
        }
        else
        {
            stateNow = prevState;
            UImanager.Instance.ShowUI(SelectUIState(state), false);
        }
    }

    private UIState SelectUIState(GameState state)
    {
        UIState uiState = UIState.PAUSE1; // DEFAULT VALUE, PAY ATTENTION TO THIS!!!!

        switch (state)
        {
            case GameState.INTERACTION1:
                uiState = UIState.INTERACTMENU1;
                break;

            case GameState.PAUSED:
                uiState = UIState.PAUSE1;
                break;
        }

        return uiState;
    }
}
