using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class C1Ask4 : MonoBehaviour
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
        if (((Ink.Runtime.StringValue)DialogueManager.instance.GetVariableState("c1Ask4_reply")).value == "Yes" &&
            ((Ink.Runtime.StringValue)DialogueManager.instance.GetVariableState("answering")).value == "c1Ask4_reply")
        {   
            GameManager.instance.playerStatsSO.energy = GameManager.instance.playerStatsSO.maxEnergy;
            DataPersistenceManager.instance.SaveGame();
            ((Ink.Runtime.StringValue)DialogueManager.instance.GetVariableState("c1Ask4_reply")).value = "No";
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
