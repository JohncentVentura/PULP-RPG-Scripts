using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class C4Ask3 : MonoBehaviour
{
    private string reply = "c4Ask3_reply";

    void Update()
    {   
        //Choices
        if (((Ink.Runtime.StringValue)DialogueManager.instance.GetVariableState(reply)).value == "Based on your answer, there is a possibility you have a high IQ or Intelligence Quotient" &&
            ((Ink.Runtime.StringValue)DialogueManager.instance.GetVariableState("answering")).value == reply)
        {   
            GameManager.instance.playerStatsSO.fitness += 1;
            GameManager.instance.playerStatsSO.logic += 1;
            DataPersistenceManager.instance.SaveGame();
            ((Ink.Runtime.StringValue)DialogueManager.instance.GetVariableState("answering")).value = ""; 
        }

        else if (((Ink.Runtime.StringValue)DialogueManager.instance.GetVariableState(reply)).value == "Based on your answer, there is a possibility you have a high EQ or Emotional Quotient" &&
            ((Ink.Runtime.StringValue)DialogueManager.instance.GetVariableState("answering")).value == reply)
        {   
            GameManager.instance.playerStatsSO.fitness += 1;
            GameManager.instance.playerStatsSO.mentalHealth += 1;
            GameManager.instance.MentalHealthStatCheck(true);
            DataPersistenceManager.instance.SaveGame();
            ((Ink.Runtime.StringValue)DialogueManager.instance.GetVariableState("answering")).value = "";
        }

        else if (((Ink.Runtime.StringValue)DialogueManager.instance.GetVariableState(reply)).value == "Based on your answer, there is a possibility you have a high SQ or Social Quotient" &&
            ((Ink.Runtime.StringValue)DialogueManager.instance.GetVariableState("answering")).value == reply)
        {   
            GameManager.instance.playerStatsSO.fitness += 1;
            GameManager.instance.playerStatsSO.charisma += 1;
            DataPersistenceManager.instance.SaveGame();
            ((Ink.Runtime.StringValue)DialogueManager.instance.GetVariableState("answering")).value = "";
        }
    }
}
