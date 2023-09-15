using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDataPersistence //Scripts extended to this class is required to load and save GameData using the methods below
{
    void LoadData(GameData data); //Loads the GameData from its parameter
    void SaveData(GameData data); //Saves data to the GameData in its parameter

    /*
    
    public void LoadData(GameData data)
    {
        
    }

    public void SaveData(GameData data)
    {
        
    }

    */
}
