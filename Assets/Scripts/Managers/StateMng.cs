using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameState
{
    PAUSED, 
    EXPLORATION, 
    INTERACTION1,
    TALK, 
    INVENTORY
}

public class StateMng : MonoBehaviour
{
    // **************
    // STATE MANAGER
    // **************

    public GameState stateNow;
    private GameState prevState;

    private bool isPaused = false;

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

    public void GoPauseState()
    {
        stateNow = GameState.PAUSED;
        isPaused = !isPaused;
        UImanager.Instance.ChangeUI(stateNow, isPaused);

        if (!isPaused)
        {
            stateNow = GameState.EXPLORATION;
        }
    }

    public void GoIntr1State(string name, string desc)
    {
        // Getting NPC info
        InvAndNPCmng.Instance.npcName = name;        // Name of NPC
        InvAndNPCmng.Instance.descNextDialog = desc; // Description of dialogue to be played
        //InvAndNPCmng.Instance.conversationNumber = number;

        TurnOffCurrentState();

        stateNow = GameState.INTERACTION1;
        UImanager.Instance.ChangeUI(stateNow, true);
    }

    public void GoTalkState()
    {
        TurnOffCurrentState();
        stateNow = GameState.TALK;
        UImanager.Instance.ChangeUI(stateNow, true);
    }

    public void GoInventory()
    {
        TurnOffCurrentState();
        stateNow = GameState.INVENTORY;
        UImanager.Instance.ChangeUI(stateNow, true);
    }

    private void TurnOffCurrentState()
    {
        prevState = stateNow;
        UImanager.Instance.ChangeUI(prevState, false);
    }


    /*
    private void SwitchState(GameState state)
    {
        if (stateNow != state)
        {
            prevState = stateNow;
            stateNow = state;
            UImanager.Instance.ChangeUI(prevState, false);
            UImanager.Instance.ChangeUI(state, true);
            //UImanager.Instance.ShowUI(SelectUIState(state), true);
        }
        else
        {
            stateNow = prevState;
            UImanager.Instance.ChangeUI(state, false);
            //UImanager.Instance.ShowUI(SelectUIState(state), false);
        }
    }
    */

    /*

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

    */
}
