using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class C5Ask5 : MonoBehaviour
{   
    [SerializeField] private LoadMenu loadMenu;
    [SerializeField] private GameObject loadSceneTrigger;

    private bool[] isCollected;

    private string[] exams = { "c5exam1", "c5exam2", "c5exam3", "c5exam4", "c5exam5", "c5exam6", "c5exam7", "c5exam8" };

    void Start()
    {
        loadSceneTrigger.gameObject.SetActive(false);
        isCollected = new bool[5];
        enabled = false;
    }

    void Update()
    {
        //KeyItems
        //Doc1
        if (GameManager.instance.keysCollected.TryGetValue("c5Doc1", out isCollected[0]))
        {
            if (isCollected[0]) ((Ink.Runtime.BoolValue)DialogueManager.instance.GetVariableState("c5Doc1Found")).value = true;
        }

        //Doc2
        if (GameManager.instance.keysCollected.TryGetValue("c5Doc2", out isCollected[1]))
        {
            if (isCollected[1]) ((Ink.Runtime.BoolValue)DialogueManager.instance.GetVariableState("c5Doc2Found")).value = true;
        }

        //Doc3
        if (GameManager.instance.keysCollected.TryGetValue("c5Doc3", out isCollected[2]))
        {
            if (isCollected[2]) ((Ink.Runtime.BoolValue)DialogueManager.instance.GetVariableState("c5Doc3Found")).value = true;
        }

        //Doc4
        if (GameManager.instance.keysCollected.TryGetValue("c5Doc4", out isCollected[3]))
        {
            if (isCollected[3]) ((Ink.Runtime.BoolValue)DialogueManager.instance.GetVariableState("c5Doc4Found")).value = true;
        }

        //isItemsFounded
        if (((Ink.Runtime.BoolValue)DialogueManager.instance.GetVariableState("c5Doc1Found")).value == true &&
            ((Ink.Runtime.BoolValue)DialogueManager.instance.GetVariableState("c5Doc2Found")).value == true &&
            ((Ink.Runtime.BoolValue)DialogueManager.instance.GetVariableState("c5Doc3Found")).value == true &&
            ((Ink.Runtime.BoolValue)DialogueManager.instance.GetVariableState("c5Doc4Found")).value == true)
        {
            ((Ink.Runtime.BoolValue)DialogueManager.instance.GetVariableState("c5isItemsFounded")).value = true;
        }

        if (((Ink.Runtime.BoolValue)DialogueManager.instance.GetVariableState("c5isItemsFounded")).value == true &&
            ((Ink.Runtime.StringValue)DialogueManager.instance.GetVariableState("c5Ask5_reply")).value == "proceed")
        {
            loadSceneTrigger.gameObject.SetActive(true);
        }

        if (((Ink.Runtime.StringValue)DialogueManager.instance.GetVariableState("c5Ask5_reply")).value == "proceed" &&
            ((Ink.Runtime.StringValue)DialogueManager.instance.GetVariableState("c5docNumber")).value == "c5")
        {
            GameManager.instance.playerStatsSO.fitness += 1;
            GameManager.instance.playerStatsSO.creativity += 1;
            GameManager.instance.playerStatsSO.logic += 1;
            GameManager.instance.playerStatsSO.charisma += 1;
            GameManager.instance.playerStatsSO.mentalHealth += 1;
            GameManager.instance.MentalHealthStatCheck(true);
            ((Ink.Runtime.StringValue)DialogueManager.instance.GetVariableState("c5docNumber")).value = "";
        }

        ScoreExams();

        //if exam is done
        if(((Ink.Runtime.BoolValue)DialogueManager.instance.GetVariableState("c5IsDocDone")).value == true &&
            ((Ink.Runtime.StringValue)DialogueManager.instance.GetVariableState("examining")).value == "c5IsDocDone")
        {   
            //Adds players stats to examScore
            ((Ink.Runtime.IntValue)DialogueManager.instance.GetVariableState("c5examScore")).value += GameManager.instance.playerStatsSO.creativity; //max create to get in c52 is 13
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
                ((Ink.Runtime.IntValue)DialogueManager.instance.GetVariableState("c5examScore")).value += 1;
                DataPersistenceManager.instance.SaveGame();
                Debug.Log("c5examScore: "+((Ink.Runtime.IntValue)DialogueManager.instance.GetVariableState("c5examScore")).value);
                ((Ink.Runtime.StringValue)DialogueManager.instance.GetVariableState("examining")).value = ""; //Avoid recurssion
            }
            else if (((Ink.Runtime.StringValue)DialogueManager.instance.GetVariableState(exam)).value == "wrong" &&
                ((Ink.Runtime.StringValue)DialogueManager.instance.GetVariableState("examining")).value == exam)
            {   
                //((Ink.Runtime.IntValue)DialogueManager.instance.GetVariableState("c5examScore")).value -= 1;
                DataPersistenceManager.instance.SaveGame();
                Debug.Log("c5examScore: "+((Ink.Runtime.IntValue)DialogueManager.instance.GetVariableState("c5examScore")).value);
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
