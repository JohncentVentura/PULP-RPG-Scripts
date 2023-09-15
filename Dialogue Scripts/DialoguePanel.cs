using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialoguePanel : Menu
{  

    // Start is called before the first frame update
    void Start()
    {
        //Extends to Menu
        SetFirstSelectedButton(firstSelected);
        Debug.Log("Start DialoguePanel");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
