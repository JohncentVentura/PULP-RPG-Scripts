using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class C5Ask2 : MonoBehaviour
{
    private CapsuleCollider2D capsuleCollider2D;
    private string reply = "c5Ask2_reply";
    private bool isCollected = false;
    
    void Start()
    {   
        capsuleCollider2D = GetComponent<CapsuleCollider2D>();
        enabled = false;
    }

    void Update()
    {
        //KeyItem
        if (GameManager.instance.keysCollected.TryGetValue("c5Book2", out isCollected))
        {
            if (isCollected)
            {
                ((Ink.Runtime.BoolValue)DialogueManager.instance.GetVariableState("c5Ask2_boolAnswer")).value = true;
            }
        }

        //Choices
        if (((Ink.Runtime.StringValue)DialogueManager.instance.GetVariableState(reply)).value == "correct" &&
            ((Ink.Runtime.StringValue)DialogueManager.instance.GetVariableState("answering")).value == reply)
        {
            GameManager.instance.playerStatsSO.fitness += 1;
            GameManager.instance.playerStatsSO.creativity += 1;
            GameManager.instance.playerStatsSO.mentalHealth += 1;
            GameManager.instance.MentalHealthStatCheck(true);
            DataPersistenceManager.instance.SaveGame();
            ((Ink.Runtime.StringValue)DialogueManager.instance.GetVariableState(reply)).value = "answered";
            ((Ink.Runtime.StringValue)DialogueManager.instance.GetVariableState("answering")).value = "";
        }
        else if (((Ink.Runtime.StringValue)DialogueManager.instance.GetVariableState(reply)).value == "wrong" &&
            ((Ink.Runtime.StringValue)DialogueManager.instance.GetVariableState("answering")).value == reply)
        {
            if(GameManager.instance.playerStatsSO.fitness <= 0) GameManager.instance.playerStatsSO.fitness = 0;
            else GameManager.instance.playerStatsSO.fitness -= 1;
            if(GameManager.instance.playerStatsSO.creativity <= 0) GameManager.instance.playerStatsSO.creativity = 0;
            else GameManager.instance.playerStatsSO.creativity -= 1;
            if(GameManager.instance.playerStatsSO.mentalHealth <= 0) GameManager.instance.playerStatsSO.mentalHealth = 0;
            else GameManager.instance.playerStatsSO.mentalHealth -= 1;
            GameManager.instance.MentalHealthStatCheck(true);
            DataPersistenceManager.instance.SaveGame();
            ((Ink.Runtime.StringValue)DialogueManager.instance.GetVariableState(reply)).value = "answered";
            ((Ink.Runtime.StringValue)DialogueManager.instance.GetVariableState("answering")).value = "";
        }

        if(((Ink.Runtime.StringValue)DialogueManager.instance.GetVariableState(reply)).value == "answered")
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
