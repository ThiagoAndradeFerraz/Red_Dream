using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum NpcStatus
{
    HEALTHY, HURT, VERYHURT, DEAD
}

public class NPCEntry
{
    private int conversationNbr; // Indicates what dialogue file to use in a talk session
    private string npcName;
    private int health;
    private NpcStatus status;

    public NPCEntry(string npcName, int conversationNbr)
    {
        this.npcName = npcName;
        this.conversationNbr = conversationNbr;
        health = 100;
        status = NpcStatus.HEALTHY;
    }

    public string GetName()
    {
        return npcName;
    }

    public void SetName(string name)
    {
        npcName = name;
    }

    public int GetConvNbr()
    {
        return conversationNbr;
    }

    public void SetConvNbr(int nbr)
    {
        conversationNbr = nbr;
    }

    public int GetHealth()
    {
        return health;
    }

    public void SetHealth(bool isPositive, int value)
    {
        if (isPositive)
        {
            health += value;

            if (health > 100)
            {
                health = 100;
            }
        }
        else
        {
            health -= value;
            
            if(health <= 0)
            {
                KillNpc();
            }
        }
    }

    private void KillNpc()
    {
        status = NpcStatus.DEAD;
        InvAndNPCmng.Instance.AffectNpcBody();
        Debug.Log(npcName + " is dead");
    }

}