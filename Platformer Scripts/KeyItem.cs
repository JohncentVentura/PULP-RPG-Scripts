using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyItem : MonoBehaviour, IDataPersistence
{      
    [SerializeField] private string id;
    [SerializeField] GameObject playerItemHolder;
    [SerializeField] string tagName;
    private bool isCollected = false;
   
    private void Start() 
    {
        //Debug.Log("Start isCollected: "+isCollected);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            this.transform.SetParent(playerItemHolder.transform);
            this.transform.localPosition = Vector3.zero;
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.tag == tagName)
        {   
            isCollected = true;
            DataPersistenceManager.instance.SaveGame();
            //Debug.Log("Collided with "+tagName);
            gameObject.SetActive(false);
        }
    }

    public void LoadData(GameData gameData)
    {
        gameData.keysCollected.TryGetValue(id, out isCollected);

        if (isCollected)
        {
            gameObject.SetActive(false);
        }
    }

    public void SaveData(GameData gameData)
    {
        if (gameData.keysCollected.ContainsKey(id))
        {
            gameData.keysCollected.Remove(id);
        }

        gameData.keysCollected.Add(id, isCollected);
    }
}
