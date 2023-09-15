using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class C1Ask2 : MonoBehaviour
{
    private bool isCollected = false;

    void Start()
    {
        enabled = false;
    }

    void Update()
    {
        //KeyItem
        if (GameManager.instance.keysCollected.TryGetValue("c1Camera", out isCollected))
        {
            if (isCollected)
            {
                ((Ink.Runtime.BoolValue)DialogueManager.instance.GetVariableState("c1Ask2_boolAnswer")).value = true;
            }
        }

        //Choices
        if (((Ink.Runtime.StringValue)DialogueManager.instance.GetVariableState("c1Ask2_reply")).value == "itemLost" &&
            ((Ink.Runtime.StringValue)DialogueManager.instance.GetVariableState("answering")).value == "c1Ask2_boolAnswer")
        {
            GameManager.instance.playerStatsSO.logic += 1;
            GameManager.instance.playerStatsSO.charisma += 1;
            DataPersistenceManager.instance.SaveGame();
            ((Ink.Runtime.StringValue)DialogueManager.instance.GetVariableState("c1Ask2_reply")).value = "itemFound";
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
