using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class C6Ask7 : MonoBehaviour
{
    [SerializeField] private GameObject questionObject1;
    [SerializeField] private GameObject questionObject2;
    private string reply = "c6Ask7_reply";

    void Update()
    {   
        //Choices
        if (((Ink.Runtime.StringValue)DialogueManager.instance.GetVariableState(reply)).value == "correct" &&
            ((Ink.Runtime.StringValue)DialogueManager.instance.GetVariableState("answering")).value == reply)
        {   
            GameManager.instance.playerStatsSO.mentalHealth += 1;
            GameManager.instance.MentalHealthStatCheck(true);
            DataPersistenceManager.instance.SaveGame();
            ((Ink.Runtime.StringValue)DialogueManager.instance.GetVariableState("answering")).value = "";
        }
        else if (((Ink.Runtime.StringValue)DialogueManager.instance.GetVariableState(reply)).value == "" &&
            ((Ink.Runtime.StringValue)DialogueManager.instance.GetVariableState("answering")).value == reply)
        {   
            if(GameManager.instance.playerStatsSO.mentalHealth <= 0) GameManager.instance.playerStatsSO.mentalHealth = 0;
            else GameManager.instance.playerStatsSO.mentalHealth -= 1;
            GameManager.instance.MentalHealthStatCheck(true);
            DataPersistenceManager.instance.SaveGame();
            ((Ink.Runtime.StringValue)DialogueManager.instance.GetVariableState("answering")).value = "";
        }

        //QuestionObjects
        if ((((Ink.Runtime.StringValue)DialogueManager.instance.GetVariableState(reply)).value == "correct"))
        {
            if (questionObject1 != null) questionObject1.gameObject.SetActive(false);
            if (questionObject2 != null) questionObject2.gameObject.SetActive(false);
        }
    }

}