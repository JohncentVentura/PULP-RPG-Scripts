using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class C4Ask1 : MonoBehaviour
{
    private bool isCollected = false;

    void Start()
    {
        enabled = false;
    }

    void Update()
    {
        //KeyItem
        if (GameManager.instance.keysCollected.TryGetValue("c4PowerDrill", out isCollected))
        {
            if (isCollected)
            {
                ((Ink.Runtime.BoolValue)DialogueManager.instance.GetVariableState("c4Ask1_boolAnswer")).value = true;
            }
        }

        //Choices
        if (((Ink.Runtime.StringValue)DialogueManager.instance.GetVariableState("c4Ask1_reply")).value == "itemLost" &&
            ((Ink.Runtime.StringValue)DialogueManager.instance.GetVariableState("answering")).value == "c4Ask1_boolAnswer")
        {   
            GameManager.instance.playerStatsSO.fitness += 1;
            GameManager.instance.playerStatsSO.charisma += 1;
            GameManager.instance.playerStatsSO.cash += 1000;
            DataPersistenceManager.instance.SaveGame();
            ((Ink.Runtime.StringValue)DialogueManager.instance.GetVariableState("c4Ask1_reply")).value = "itemFound";
            ((Ink.Runtime.StringValue)DialogueManager.instance.GetVariableState("answering")).value = "";
        }

    }

    void OnBecameVisible()
    {
        enabled = true;
    }

    void OnBecameInvisible()
    {
        enabled = false;
    }
}
