using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class C1Ask1 : MonoBehaviour
{

    void Start()
    {
        enabled = false;
    }

    void Update()
    {
        //Player
        ((Ink.Runtime.IntValue)DialogueManager.instance.GetVariableState("playerCash")).value = GameManager.instance.playerStatsSO.cash;

        //Choices
        if (((Ink.Runtime.IntValue)DialogueManager.instance.GetVariableState("c1Ask1_intAnswer")).value == 100 &&
            ((Ink.Runtime.StringValue)DialogueManager.instance.GetVariableState("answering")).value == "c1Ask1_intAnswer")
        {
            GameManager.instance.playerStatsSO.cash -= 100;
            GameManager.instance.playerStatsSO.charisma += 1;
            DataPersistenceManager.instance.SaveGame();
            ((Ink.Runtime.StringValue)DialogueManager.instance.GetVariableState("answering")).value = ""; //answering will be saved in ApplicationOnExit or SavePoint
        }
        else if (((Ink.Runtime.IntValue)DialogueManager.instance.GetVariableState("c1Ask1_intAnswer")).value == 10 &&
            ((Ink.Runtime.StringValue)DialogueManager.instance.GetVariableState("answering")).value == "c1Ask1_intAnswer")
        {
            GameManager.instance.playerStatsSO.cash -= 10;
            GameManager.instance.playerStatsSO.charisma += 1;
            DataPersistenceManager.instance.SaveGame();
            ((Ink.Runtime.StringValue)DialogueManager.instance.GetVariableState("answering")).value = "";
        }
        else if (((Ink.Runtime.IntValue)DialogueManager.instance.GetVariableState("c1Ask1_intAnswer")).value == 1 &&
            ((Ink.Runtime.StringValue)DialogueManager.instance.GetVariableState("answering")).value == "c1Ask1_intAnswer")
        {
            DataPersistenceManager.instance.SaveGame();
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
