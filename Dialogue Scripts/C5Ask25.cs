using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class C5Ask25 : MonoBehaviour
{
    private CapsuleCollider2D capsuleCollider2D;
    private string reply = "c5Ask25_reply";

    void Start()
    {   
        capsuleCollider2D = GetComponent<CapsuleCollider2D>();
        enabled = false;
    }

    void Update()
    {
        //Choices
        if (((Ink.Runtime.StringValue)DialogueManager.instance.GetVariableState(reply)).value == "correct" &&
            ((Ink.Runtime.StringValue)DialogueManager.instance.GetVariableState("answering")).value == reply)
        {
            GameManager.instance.playerStatsSO.creativity += 2;
            DataPersistenceManager.instance.SaveGame();
            ((Ink.Runtime.StringValue)DialogueManager.instance.GetVariableState("answering")).value = "";
        }
        else if (((Ink.Runtime.StringValue)DialogueManager.instance.GetVariableState(reply)).value == "wrong" &&
            ((Ink.Runtime.StringValue)DialogueManager.instance.GetVariableState("answering")).value == reply)
        {
            if(GameManager.instance.playerStatsSO.creativity <= 0) GameManager.instance.playerStatsSO.creativity = 0;
            else GameManager.instance.playerStatsSO.creativity -= 1;
            DataPersistenceManager.instance.SaveGame();
            ((Ink.Runtime.StringValue)DialogueManager.instance.GetVariableState("answering")).value = "";
        }

        if(((Ink.Runtime.StringValue)DialogueManager.instance.GetVariableState(reply)).value == "correct" ||
            ((Ink.Runtime.StringValue)DialogueManager.instance.GetVariableState(reply)).value == "wrong")
        {
            capsuleCollider2D.isTrigger = true;
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