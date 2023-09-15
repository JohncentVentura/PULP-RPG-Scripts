using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class C3Ask9 : MonoBehaviour
{
    void Start()
    {
        enabled = false;
    }

    void Update()
    {
        //Player
        ((Ink.Runtime.IntValue)DialogueManager.instance.GetVariableState("playerEnergy")).value = GameManager.instance.playerStatsSO.energy;

        //Choices
        if (((Ink.Runtime.StringValue)DialogueManager.instance.GetVariableState("c3Ask9_reply")).value == "Yes" &&
            ((Ink.Runtime.StringValue)DialogueManager.instance.GetVariableState("answering")).value == "c3Ask9_reply")
        {   
            GameManager.instance.playerStatsSO.energy = GameManager.instance.playerStatsSO.maxEnergy;
            DataPersistenceManager.instance.SaveGame();
            ((Ink.Runtime.StringValue)DialogueManager.instance.GetVariableState("c3Ask9_reply")).value = "No";
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
