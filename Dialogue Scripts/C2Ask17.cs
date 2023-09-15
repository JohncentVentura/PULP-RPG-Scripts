using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class C2Ask17 : MonoBehaviour
{   
    [SerializeField] private LoadMenu loadMenu;
    [SerializeField] private GameObject loadSceneTrigger;
    [SerializeField] private GameObject unbuildUnit;
    [SerializeField] private GameObject buildedUnit;

    private bool[] isCollected;

    private string[] exams = { "c2exam1", "c2exam2", "c2exam3", "c2exam4", "c2exam5", "c2exam6", "c2exam7", "c2exam8"};

    void Start()
    {   
        loadSceneTrigger.gameObject.SetActive(false);
        isCollected = new bool[5];
        enabled = false;
        buildedUnit.SetActive(false);
    }

    void Update()
    {
        //KeyItems
        //Motherboard
        if (GameManager.instance.keysCollected.TryGetValue("c2Mobo", out isCollected[0]))
        {
            if (isCollected[0]) ((Ink.Runtime.BoolValue)DialogueManager.instance.GetVariableState("c2MoboFound")).value = true;
        }

        //CPU
        if (GameManager.instance.keysCollected.TryGetValue("c2CPU", out isCollected[1]))
        {
            if (isCollected[1]) ((Ink.Runtime.BoolValue)DialogueManager.instance.GetVariableState("c2CPUFound")).value = true;
        }

        //RAM
        if (GameManager.instance.keysCollected.TryGetValue("c2RAM", out isCollected[2]))
        {
            if (isCollected[2]) ((Ink.Runtime.BoolValue)DialogueManager.instance.GetVariableState("c2RAMFound")).value = true;
        }

        //HDD
        if (GameManager.instance.keysCollected.TryGetValue("c2HDD", out isCollected[3]))
        {
            if (isCollected[3]) ((Ink.Runtime.BoolValue)DialogueManager.instance.GetVariableState("c2HDDFound")).value = true;
        }

        //PSU
        if (GameManager.instance.keysCollected.TryGetValue("c2PSU", out isCollected[4]))
        {
            if (isCollected[4]) ((Ink.Runtime.BoolValue)DialogueManager.instance.GetVariableState("c2PSUFound")).value = true;
        }

        //isItemsFounded
        if (((Ink.Runtime.BoolValue)DialogueManager.instance.GetVariableState("c2MoboFound")).value == true &&
            ((Ink.Runtime.BoolValue)DialogueManager.instance.GetVariableState("c2CPUFound")).value == true &&
            ((Ink.Runtime.BoolValue)DialogueManager.instance.GetVariableState("c2RAMFound")).value == true &&
            ((Ink.Runtime.BoolValue)DialogueManager.instance.GetVariableState("c2HDDFound")).value == true &&
            ((Ink.Runtime.BoolValue)DialogueManager.instance.GetVariableState("c2PSUFound")).value == true)
        {
            ((Ink.Runtime.BoolValue)DialogueManager.instance.GetVariableState("c2isItemsFounded")).value = true;
            buildedUnit.SetActive(true);
            unbuildUnit.SetActive(false);
        }

        if (((Ink.Runtime.BoolValue)DialogueManager.instance.GetVariableState("c2isItemsFounded")).value == true &&
            ((Ink.Runtime.StringValue)DialogueManager.instance.GetVariableState("c2Ask17_reply")).value == "proceed")
        {
            loadSceneTrigger.gameObject.SetActive(true);
        }

        //Player & Permit
        ((Ink.Runtime.IntValue)DialogueManager.instance.GetVariableState("playerCash")).value = GameManager.instance.playerStatsSO.cash;

        if (((Ink.Runtime.BoolValue)DialogueManager.instance.GetVariableState("c2isExamDone")).value == true &&
            ((Ink.Runtime.StringValue)DialogueManager.instance.GetVariableState("c2permitNumber")).value == "c2")
        {
            GameManager.instance.playerStatsSO.cash -= 4000;
            GameManager.instance.playerStatsSO.fitness += 1;
            GameManager.instance.playerStatsSO.creativity += 1;
            GameManager.instance.playerStatsSO.logic += 1;
            GameManager.instance.playerStatsSO.charisma += 1;
            GameManager.instance.playerStatsSO.mentalHealth += 1;
            GameManager.instance.MentalHealthStatCheck(true);
            DataPersistenceManager.instance.SaveGame();
            ((Ink.Runtime.StringValue)DialogueManager.instance.GetVariableState("c2permitNumber")).value = "";
        }

        ScoreExams();

        //if exam is done
        if(((Ink.Runtime.BoolValue)DialogueManager.instance.GetVariableState("c2isExamDone")).value == true &&
            ((Ink.Runtime.StringValue)DialogueManager.instance.GetVariableState("examining")).value == "c2isExamDone")
        {   
            //Adds players stats to examScore
            ((Ink.Runtime.IntValue)DialogueManager.instance.GetVariableState("c2examScore")).value += GameManager.instance.playerStatsSO.logic; //max logic to get in c2 is 18
            Debug.Log("c2examScore + logic: "+((Ink.Runtime.IntValue)DialogueManager.instance.GetVariableState("c2examScore")).value);
            DataPersistenceManager.instance.SaveGame();
            ((Ink.Runtime.StringValue)DialogueManager.instance.GetVariableState("examining")).value = ""; //Avoid recurssion
        }

        if(((Ink.Runtime.StringValue)DialogueManager.instance.GetVariableState("repeatChapter")).value == "true")
        {   
            GameManager.instance.playerStatsSO.energy = 0;
            //loadMenu.SceneTransition("ChapterMenuScene", GameManager.instance.scenesUnlockedSO.sceneName, 0, 0);
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
                ((Ink.Runtime.IntValue)DialogueManager.instance.GetVariableState("c2examScore")).value += 1;
                DataPersistenceManager.instance.SaveGame();
                ((Ink.Runtime.StringValue)DialogueManager.instance.GetVariableState("examining")).value = ""; //Avoid recurssion  
                Debug.Log("c2examScore: "+((Ink.Runtime.IntValue)DialogueManager.instance.GetVariableState("c2examScore")).value);
            }
            else if (((Ink.Runtime.StringValue)DialogueManager.instance.GetVariableState(exam)).value == "wrong" &&
                ((Ink.Runtime.StringValue)DialogueManager.instance.GetVariableState("examining")).value == exam)
            {
                //((Ink.Runtime.IntValue)DialogueManager.instance.GetVariableState("c2examScore")).value -= 1;
                DataPersistenceManager.instance.SaveGame();
                ((Ink.Runtime.StringValue)DialogueManager.instance.GetVariableState("examining")).value = "";
                Debug.Log("c2examScore: "+((Ink.Runtime.IntValue)DialogueManager.instance.GetVariableState("c2examScore")).value);
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
