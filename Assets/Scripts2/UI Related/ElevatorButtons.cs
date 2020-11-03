using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ElevatorButtons : MonoBehaviour
{
    // *****************************************
    // This class stores the elevator UI methods
    // *****************************************

    // For the number buttons
    public void UpdateDisplay()
    {
        Text textObj = gameObject.GetComponent<Transform>().GetChild(0).GetComponent<Text>();
        char n = textObj.text[1];
        MngParent.Instance.uiManager.GetComponent<UIManager>().UpdateDisplay(n);
        //Debug.Log(textObj.text[1]);
    }

    // For the cancel button
    public void GetOut()
    {
        MngParent.Instance.uiManager.GetComponent<UIManager>().ShowElevatorUI(false);
    }

    public void LoadFloor()
    {
        string nbr = MngParent.Instance.uiManager.GetComponent<UIManager>().GetDisplayNbr();
        if (isNbrValid(nbr))
        {
            Debug.Log("Loading level...");
        }
        else
        {
            Debug.Log("Wrong!");
        }
    }

    private bool isNbrValid(string Nbr)
    {
        bool status;
        
        switch (Nbr)
        {
            case "10":
                status = true;
                break;

            case "04":
                status = true;
                break;

            case "00":
                status = true;
                break;

            default:
                status = false;
                break;
        }

        return status;
    }
}

