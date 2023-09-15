using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class C3Crowd : MonoBehaviour
{   
    [SerializeField] private GameObject crowd1;
    [SerializeField] private GameObject crowd2;
    [SerializeField] private string reply; //Sample is "c3Ask2_reply"

    void Update()
    {
        //NPC Blockers
        if ((((Ink.Runtime.StringValue)DialogueManager.instance.GetVariableState(reply)).value == "correct"))
        {
            if (crowd1 != null) crowd1.gameObject.SetActive(false);
            if (crowd2 != null) crowd2.gameObject.SetActive(false);
        }
    }
}
