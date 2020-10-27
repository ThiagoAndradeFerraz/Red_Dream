using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IInteractible
{
    void ShowInfo();
    void Interact();
    string Name { get; set; } // Name of object / NPC
    string StateDesc { get; set; } // Description of current state of object / NPC
}
