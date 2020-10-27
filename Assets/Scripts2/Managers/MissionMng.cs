using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissionMng : MonoBehaviour
{
    public int currentMainMission = 0;
    public int currentSideMission = 0;

    private void Start()
    {
        CheckWhatToDo();
    }


    private void CheckWhatToDo()
    {
        switch (currentMainMission)
        {
            case 0:

                MngParent.Instance.uiManager.GetComponent<UIManager>().StartDialogue("Intro", "1");


                break;
        }
    }


}
