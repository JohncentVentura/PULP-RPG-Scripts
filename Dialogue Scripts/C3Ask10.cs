using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class C3Ask10 : MonoBehaviour
{
    void Start()
    {
        enabled = false;
    }

    void Update()
    {
        //Choices
        if (((Ink.Runtime.StringValue)DialogueManager.instance.GetVariableState("c1Ask3_reply")).value == "itemFound" &&
            ((Ink.Runtime.StringValue)DialogueManager.instance.GetVariableState("answering")).value == "c3Ask10_reply")
        {
            GameManager.instance.playerStatsSO.cash += 1000;
            GameManager.instance.playerStatsSO.creativity += 1;
            GameManager.instance.playerStatsSO.charisma += 1;
            DataPersistenceManager.instance.SaveGame();
            ((Ink.Runtime.StringValue)DialogueManager.instance.GetVariableState("answering")).value = ""; //answering will be saved in ApplicationOnExit or SavePoint
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
