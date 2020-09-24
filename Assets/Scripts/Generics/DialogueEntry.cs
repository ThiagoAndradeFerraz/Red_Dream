using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueEntry
{
    public string name;
    public string line;
    public string emotionNPC;
    public string emotionPlayer;

    public DialogueEntry(string name, string line, string emotionNPC, string emotionPlayer)
    {
        this.name = name;
        this.line = line;
        this.emotionNPC = emotionNPC;
        this.emotionPlayer = emotionPlayer;
    }
}
