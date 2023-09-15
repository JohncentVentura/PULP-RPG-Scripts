using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ink.Runtime;

public class DialogueVariablesManager
{
    public Dictionary<string, Ink.Runtime.Object> variables { get; private set; }

    public Story globalVariablesStory;
    //private const string saveVariablesKey = "INK_VARIABLES";

    public DialogueVariablesManager(TextAsset loadGlobalsJSON)
    {   
        // create the story
        globalVariablesStory = new Story(loadGlobalsJSON.text);

        /*
        //if we have saved data, load it
        if(PlayerPrefs.HasKey(saveVariablesKey))
        {
            string jsonState = PlayerPrefs.GetString(saveVariablesKey);
            globalVariablesStory.state.LoadJson(jsonState);
        }
        */

        if(GameManager.instance.saveVariablesKey != "")
        {
            string jsonState = GameManager.instance.saveVariablesKey;
            globalVariablesStory.state.LoadJson(jsonState);
        }

        // initialize the dictionary
        variables = new Dictionary<string, Ink.Runtime.Object>();

        //We'll loop each global variable name in the globals file
        foreach (string name in globalVariablesStory.variablesState)
        {   
            //For each variable name, we'll get its current value, and add that entry to the dictionary
            Ink.Runtime.Object value = globalVariablesStory.variablesState.GetVariableWithName(name);
            variables.Add(name, value);
            //Debug.Log("Initialized global dialogue variable: " + name + " = " + value);
        }
    }

    public void StartListening(Story story)
    {
        VariableToStory(story);
        story.variablesState.variableChangedEvent += VariableChanged;
    }

    public void StopListening(Story story)
    {
        story.variablesState.variableChangedEvent -= VariableChanged;
    }

    private void VariableChanged(string name, Ink.Runtime.Object value)
    {   
        //only maintain variables that were initialized from the globals ink file
        if (variables.ContainsKey(name)) //Check if the name is contained in the dictionary, using the ContainsKey method
        {
            variables.Remove(name); //removes the old entry
            variables.Add(name, value); //add and update the entry
        }
    }

    public void VariableToStory(Story story) //This method takes a story object
    {   
        //We'll loop each entry in the dictionary to pass in the key and value 
        foreach (KeyValuePair<string, Ink.Runtime.Object> variable in variables)
        {
            story.variablesState.SetGlobal(variable.Key, variable.Value);
        }
    }

    /*
    public void SaveVariables()
    {
        if(globalVariablesStory != null)
        {   
            //Load the current state of all our variables to the globals story
            VariableToStory(globalVariablesStory);
            //NOTE: eventually you would replace this with an actual save/load, rather than using playerprefs
            PlayerPrefs.SetString(saveVariablesKey, globalVariablesStory.state.ToJson());
        }
    }

    //My scripts

    public void LoadVariables()
    {
        if(globalVariablesStory != null)
        {   
            //Load the current state of all our variables to the globals story
            VariableToStory(globalVariablesStory);
            //NOTE: eventually you would replace this with an actual save/load, rather than using playerprefs
            PlayerPrefs.GetString(saveVariablesKey, globalVariablesStory.state.ToJson());
        }
    }
    */
}