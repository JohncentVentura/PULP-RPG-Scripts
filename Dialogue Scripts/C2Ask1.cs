using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class C2Ask1 : MonoBehaviour
{
    [SerializeField] private GameObject questionObject1;
    [SerializeField] private GameObject questionObject2;
    private string reply = "c2Ask1_reply";

    void Update()
    {   
        //Choices
        if (((Ink.Runtime.StringValue)DialogueManager.instance.GetVariableState(reply)).value == "correct" &&
            ((Ink.Runtime.StringValue)DialogueManager.instance.GetVariableState("answering")).value == reply)
        {   
            GameManager.instance.playerStatsSO.logic += 1;
            DataPersistenceManager.instance.SaveGame();
            ((Ink.Runtime.StringValue)DialogueManager.instance.GetVariableState("answering")).value = ""; //answering will be saved in ApplicationOnExit or SavePoint
        }
        else if (((Ink.Runtime.StringValue)DialogueManager.instance.GetVariableState(reply)).value == "" &&
            ((Ink.Runtime.StringValue)DialogueManager.instance.GetVariableState("answering")).value == reply)
        {   
            if(GameManager.instance.playerStatsSO.logic <= 0) GameManager.instance.playerStatsSO.logic = 0;
            else GameManager.instance.playerStatsSO.logic -= 1;
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
