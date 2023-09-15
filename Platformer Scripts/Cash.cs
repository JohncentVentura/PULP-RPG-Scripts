using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cash : MonoBehaviour, IDataPersistence
{   
    [SerializeField] private AudioClip cashCollected;

    [SerializeField] private int cashValue;

    [SerializeField] private string id; //Every cash has a unique ID that determines if it is pickep or not while loading and saving it

    [ContextMenu("Generate Guid of ID")] //ContextMenu can show an additional menu in the Unity Inspector with the name base on the parameter
    private void GenerateGuid()
    {   
        //Guid are strings of 32 char that have a high probability to be unique, NewGuid().ToString() generates a string for the cash id
        //CAUTION: Drag the GameObjects attached to this script to the parent GameObject to force the scene to save, or else the GUID will become null
        id = System.Guid.NewGuid().ToString();
    }

    private bool isCollected = false;

    private void OnTriggerEnter2D(Collider2D other) 
    {
        if(other.gameObject.tag == "Player")
        {   
            AudioManager.instance.PlaySFX(cashCollected);
            GameManager.instance.playerStatsSO.cash += cashValue;
            isCollected = true;
            gameObject.SetActive(false);
        }
    }

    public void LoadData(GameData gameData)
    {   
        ///*
        //Getting all cash from cashCollected to check if there is a cash with same id with this cash, and store that value to isCollected
        gameData.cashCollected.TryGetValue(id, out isCollected);

        if(isCollected)
        {
            gameObject.SetActive(false); //Disable this GameObject if the loaded isCollected is true
        }
        //*/
    }

    public void SaveData(GameData gameData)
    {   
        ///*
        //Removes this cash ID since we will update this value when saving
        if(gameData.cashCollected.ContainsKey(id)) 
        {
            gameData.cashCollected.Remove(id); 
        }

        //Add this cash ID with an updated value
        gameData.cashCollected.Add(id, isCollected);
        //*/
    }
}
