using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Elevator : IntrGO
{
    protected override void Interact()
    {
        MngParent.Instance.uiManager.GetComponent<UIManager>().ShowElevatorUI(true);
    }
}
