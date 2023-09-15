using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class C3Ask2 : MonoBehaviour
{
    private bool isCollected = false;
    [SerializeField] private GameObject crowd1;
    [SerializeField] private GameObject crowd2;

    void Start()
    {
        enabled = false;
    }

    void Update()
    {   
        //KeyItem
        if (GameManager.instance.keysCollected.TryGetValue("c1Roomkey", out isCollected))
        {
            if (isCollected)
            {
                ((Ink.Runtime.BoolValue)DialogueManager.instance.GetVariableState("c3Ask2_boolAnswer")).value = true;
                DataPersistenceManager.instance.SaveGame();
            }
        }

        //Choices
        if (((Ink.Runtime.StringValue)DialogueManager.instance.GetVariableState("c3Ask2_reply")).value == "itemLost" &&
            ((Ink.Runtime.StringValue)DialogueManager.instance.GetVariableState("answering")).value == "c3Ask2_boolAnswer")
        {
            GameManager.instance.playerStatsSO.charisma += 1;
            DataPersistenceManager.instance.SaveGame();
            ((Ink.Runtime.StringValue)DialogueManager.instance.GetVariableState("c3Ask2_reply")).value = "itemFound";
            ((Ink.Runtime.StringValue)DialogueManager.instance.GetVariableState("answering")).value = "";
        }

        //Crowd
        if ((((Ink.Runtime.StringValue)DialogueManager.instance.GetVariableState("c3Ask2_reply")).value == "itemFound"))
        {
            if (crowd1 != null) crowd1.gameObject.SetActive(false);
            if (crowd2 != null) crowd2.gameObject.SetActive(false);
            DataPersistenceManager.instance.SaveGame();
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
