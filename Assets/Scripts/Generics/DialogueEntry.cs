using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueEntry
{
    public string name; // Name of character that is talking
    public string line; // Line of dialogue
    public string position; // Position of the name. L -> Left ; R -> Right
    public string emotionNPC; // Emotion of the NPC
    public string emotionPlayer; // Emotion of the player

    public DialogueEntry(string name, string line, string position, string emotionNPC, string emotionPlayer)
    {
        this.name = name;
        this.line = line;
        this.position = position;
        this.emotionNPC = emotionNPC;
        this.emotionPlayer = emotionPlayer;
    }
}
