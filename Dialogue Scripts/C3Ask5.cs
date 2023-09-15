using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class C3Ask5 : MonoBehaviour
{
    [SerializeField] private GameObject crowd1;
    [SerializeField] private GameObject crowd2;
    private string reply = "c3Ask5_reply";

    // Update is called once per frame
    void Update()
    {   
        //Choices
        if (((Ink.Runtime.StringValue)DialogueManager.instance.GetVariableState(reply)).value == "correct" &&
            ((Ink.Runtime.StringValue)DialogueManager.instance.GetVariableState("answering")).value == reply)
        {   
            GameManager.instance.playerStatsSO.fitness += 1;
            GameManager.instance.playerStatsSO.creativity += 1;
            GameManager.instance.playerStatsSO.charisma += 1;
            DataPersistenceManager.instance.SaveGame();
            ((Ink.Runtime.StringValue)DialogueManager.instance.GetVariableState("answering")).value = "";
        }
        else if (((Ink.Runtime.StringValue)DialogueManager.instance.GetVariableState(reply)).value == "wrong" &&
            ((Ink.Runtime.StringValue)DialogueManager.instance.GetVariableState("answering")).value == reply)
        {   
            if(GameManager.instance.playerStatsSO.fitness <= 0) GameManager.instance.playerStatsSO.fitness = 0;
            else GameManager.instance.playerStatsSO.fitness -= 1;
            if(GameManager.instance.playerStatsSO.creativity <= 0) GameManager.instance.playerStatsSO.creativity = 0;
            else GameManager.instance.playerStatsSO.creativity -= 1;
            if(GameManager.instance.playerStatsSO.charisma <= 0) GameManager.instance.playerStatsSO.charisma = 0;
            else GameManager.instance.playerStatsSO.charisma -= 1;
            DataPersistenceManager.instance.SaveGame();
            ((Ink.Runtime.StringValue)DialogueManager.instance.GetVariableState("answering")).value = "";
        }

        //Crowd
        if ((((Ink.Runtime.StringValue)DialogueManager.instance.GetVariableState(reply)).value == "correct") ||
            (((Ink.Runtime.StringValue)DialogueManager.instance.GetVariableState(reply)).value == "wrong") )
        {
            if (crowd1 != null) crowd1.gameObject.SetActive(false);
            if (crowd2 != null) crowd2.gameObject.SetActive(false);
        }
    }
}
