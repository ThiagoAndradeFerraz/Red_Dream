using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameState
{
    PAUSED, EXPLORATION, COMBAT, INTERACTION
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
        if(stateNow != GameState.PAUSED)
        {
            prevState = stateNow;
            stateNow = GameState.PAUSED;
            UImanager.Instance.SetPause1UI(true);
        }
        else
        {
            stateNow = prevState;
            UImanager.Instance.SetPause1UI(false);
        }
    }

    public void SetInteract()
    {

    }

    public void SetCombat()
    {

    }

    public void SetExploration()
    {

    }

}
