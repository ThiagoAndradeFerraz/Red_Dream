﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC1 : Interactive
{
    
    // Start is called before the first frame update
    /*void Start()
    {
        
    }*/

    // Update is called once per frame
    void Update()
    {
        CheckDistance();
    }

    protected override void Interact()
    {
        Debug.Log("interagiu!");
    }
}