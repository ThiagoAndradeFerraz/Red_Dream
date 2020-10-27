using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MngParent : MonoBehaviour
{
    // ---------------------------------------------------------------
    // Parent object that holds the other managers in children objects
    // ---------------------------------------------------------------

    public Transform uiManager, languageManager, missionManager;

    public static MngParent Instance { get; private set; }

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        GetManagers();
    }

    void GetManagers()
    {
        uiManager = gameObject.transform.GetChild(0).GetComponent<Transform>();
        missionManager = gameObject.transform.GetChild(1).GetComponent<Transform>();
    }

    




}
