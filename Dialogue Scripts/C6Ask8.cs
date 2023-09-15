using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class C6Ask8 : MonoBehaviour
{   
    [SerializeField] private LoadMenu loadMenu;
    [SerializeField] private GameObject loadSceneTrigger;

    private string[] exams = { "c6exam1", "c6exam2", "c6exam3", "c6exam4", "c6exam5", "c6exam6", "c6exam7", "c6exam8" };

    void Start()
    {
        loadSceneTrigger.gameObject.SetActive(false);
        enabled = false;
    }

    void Update()
    {
        if (((Ink.Runtime.BoolValue)DialogueManager.instance.GetVariableState("c6isDefenseDone")).value == true &&
            ((Ink.Runtime.StringValue)DialogueManager.instance.GetVariableState("c6Ask8_reply")).value == "endDefense")
        {
            loadSceneTrigger.gameObject.SetActive(true);
        }

        //Player & Permit
        ((Ink.Runtime.IntValue)DialogueManager.instance.GetVariableState("playerCash")).value = GameManager.instance.playerStatsSO.cash;

        if (((Ink.Runtime.BoolValue)DialogueManager.instance.GetVariableState("c6isDefenseDone")).value == true &&
            ((Ink.Runtime.StringValue)DialogueManager.instance.GetVariableState("c6permitNumber")).value == "c6")
        {
            GameManager.instance.playerStatsSO.cash -= 4000;
            DataPersistenceManager.instance.SaveGame();
            ((Ink.Runtime.StringValue)DialogueManager.instance.GetVariableState("c6permitNumber")).value = "";
        }

        ScoreExams();

        //if exam is done
        if(((Ink.Runtime.BoolValue)DialogueManager.instance.GetVariableState("c6isDefenseDone")).value == true &&
            ((Ink.Runtime.StringValue)DialogueManager.instance.GetVariableState("examining")).value == "c6isDefenseDone")
        {   
            //Adds players stats to examScore
            ((Ink.Runtime.IntValue)DialogueManager.instance.GetVariableState("c6examScore")).value += GameManager.instance.playerStatsSO.logic; //max logic to get in c6 is 34
            ((Ink.Runtime.IntValue)DialogueManager.instance.GetVariableState("c6examScore")).value += GameManager.instance.playerStatsSO.creativity; //max create to get in c6 is 29
            DataPersistenceManager.instance.SaveGame();
            ((Ink.Runtime.StringValue)DialogueManager.instance.GetVariableState("examining")).value = ""; //Avoid recurssion
        }

        if(((Ink.Runtime.StringValue)DialogueManager.instance.GetVariableState("repeatChapter")).value == "true")
        {
            loadMenu.SceneTransition("ChapterMenuScene", GameManager.instance.scenesUnlockedSO.sceneName, 0, 0);
            ((Ink.Runtime.StringValue)DialogueManager.instance.GetVariableState("repeatChapter")).value = "false";
        }
    }

    private void ScoreExams()
    {
        foreach (string exam in exams) //Iterates every exam and assigning each exam score to c2examScore value
        {   
            if (((Ink.Runtime.StringValue)DialogueManager.instance.GetVariableState(exam)).value == "correct" &&
            ((Ink.Runtime.StringValue)DialogueManager.instance.GetVariableState("examining")).value == exam)
            {
                ((Ink.Runtime.IntValue)DialogueManager.instance.GetVariableState("c6examScore")).value += 1;
                DataPersistenceManager.instance.SaveGame();
                Debug.Log("c6examScore: "+((Ink.Runtime.IntValue)DialogueManager.instance.GetVariableState("c6examScore")).value);
                ((Ink.Runtime.StringValue)DialogueManager.instance.GetVariableState("examining")).value = ""; //Avoid recurssion
            }
            else if (((Ink.Runtime.StringValue)DialogueManager.instance.GetVariableState(exam)).value == "wrong" &&
                ((Ink.Runtime.StringValue)DialogueManager.instance.GetVariableState("examining")).value == exam)
            {
                //((Ink.Runtime.IntValue)DialogueManager.instance.GetVariableState("c6examScore")).value -= 1;
                DataPersistenceManager.instance.SaveGame();
                Debug.Log("c6examScore: "+((Ink.Runtime.IntValue)DialogueManager.instance.GetVariableState("c6examScore")).value);
                ((Ink.Runtime.StringValue)DialogueManager.instance.GetVariableState("examining")).value = "";
            }
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