using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[System.Serializable] //Can be written and read in a file

public class GameData //Storage of saved data
{
    public long lastUpdated; //Use to store serialized date time object

    //Scriptable Objects, by order base on its ScriptableObject
    public PlayerStatsData playerStatsData;
    public ScenesUnlockedData scenesUnlockedData;
    public InventoryData inventoryData;

    //Platformer
    public SerializableDictionary<string, bool> cashCollected;
    public SerializableDictionary<string, bool> keysCollected;

    //Dialogues
    public string saveVariablesKey;

    public GameData()
    {   
        //PlayerStats
        playerStatsData = new PlayerStatsData();
        scenesUnlockedData = new ScenesUnlockedData();
        inventoryData = new InventoryData();

        //Platformer
        cashCollected = new SerializableDictionary<string, bool>();
        keysCollected = new SerializableDictionary<string, bool>();

        //Dialogues
        saveVariablesKey = "";
    }

    public int GetPercentageComplete()
    {   
        /*
        int totalCollected = 0; //Placeholder for the coins we collected

        //Iterate coins from coinsCollected and check each values, if the value is true then it is collected and increments the number of totalCollected coins
        foreach (bool collected in cashCollected.Values) 
        {
            if (collected) 
            {
                totalCollected++;
            }
        }

        int percentageCompleted = -1; //Ensure we don't divide by 0 when calculating the percentage

        if(cashCollected.Count != 0) //coinsCollected.Count is how many coins are in the game
        {
            percentageCompleted = (totalCollected * 100 / cashCollected.Count); //percentage calculation
        }

        return percentageCompleted;
        */

        return 100;
    }

    //Called when New Game
    public void NewPlayerStatsData()
    {   
        GameManager.instance.playerStatsSO.xPosition = 0;
        GameManager.instance.playerStatsSO.yPosition = 0;
        GameManager.instance.playerStatsSO.runtimeAnimatorController = null;
        GameManager.instance.playerStatsSO.maxEnergy = 1;
        GameManager.instance.playerStatsSO.energy = GameManager.instance.playerStatsSO.maxEnergy;
        GameManager.instance.playerStatsSO.cash = 0;
        GameManager.instance.playerStatsSO.fitness = 2;
        GameManager.instance.playerStatsSO.logic = 0;
        GameManager.instance.playerStatsSO.creativity = 0;
        GameManager.instance.playerStatsSO.charisma = 0;
        GameManager.instance.playerStatsSO.mentalHealth = 2;
        GameManager.instance.playerStatsSO.equippedItemName = null;
        GameManager.instance.playerStatsSO.tempStatPoint = 1;
        GameManager.instance.MentalHealthStatCheck(true);
        GameManager.instance.playerStatsSO.maxEnergy = GameManager.instance.playerStatsSO.fitness;
        GameManager.instance.playerStatsSO.energy = GameManager.instance.playerStatsSO.maxEnergy;
        GameManager.instance.playerStatsSO.gamepadCount = 0; //0 is using keyboard, 1 is using gamepad
    }

    public void NewScenesUnlockedData()
    {
        GameManager.instance.scenesUnlockedSO.sceneName = "";
        GameManager.instance.scenesUnlockedSO.unlockStartMenuScene = true;
        GameManager.instance.scenesUnlockedSO.unlockChapter1Scene = true;
        GameManager.instance.scenesUnlockedSO.unlockChapter2Scene = false;
        GameManager.instance.scenesUnlockedSO.unlockChapter3Scene = false;
        GameManager.instance.scenesUnlockedSO.unlockChapter4Scene = false;
        GameManager.instance.scenesUnlockedSO.unlockChapter5Scene = false;
        GameManager.instance.scenesUnlockedSO.unlockChapter6Scene = false;
        GameManager.instance.scenesUnlockedSO.unlockChapterMenuScene = true;
        GameManager.instance.scenesUnlockedSO.unlockPlayerMenuScene = true;
    }

    public void NewInventoryData()
    {   
        //bool
        GameManager.instance.inventorySO.isItemUsed = false;
        //GameManager.instance.inventorySO.addSportsShoes = false;
        //GameManager.instance.inventorySO.addVitaminCapsule = false;
        GameManager.instance.inventorySO.addFirstAid = false;
        GameManager.instance.inventorySO.addCharismaBook = false;
        GameManager.instance.inventorySO.addESDShoes = false;
        GameManager.instance.inventorySO.addLogicBook = false;
        GameManager.instance.inventorySO.addUmbrella = false;
        GameManager.instance.inventorySO.addFitnessBook = false;
        GameManager.instance.inventorySO.addEnergyDrink = false;
        GameManager.instance.inventorySO.addMentalHealthBook = false;
        GameManager.instance.inventorySO.addCoffee = false;
        GameManager.instance.inventorySO.addCreativityBook = false;
        GameManager.instance.inventorySO.addJumpingShoes = false;

        //sprite
        //GameManager.instance.inventorySO.spriteSportsShoes = null;
        //GameManager.instance.inventorySO.spriteVitaminCapsule = null;
        GameManager.instance.inventorySO.spriteFirstAid = null;
        GameManager.instance.inventorySO.spriteCharismaBook = null;
        GameManager.instance.inventorySO.spriteESDShoes = null;
        GameManager.instance.inventorySO.spriteLogicBook = null;
        GameManager.instance.inventorySO.spriteUmbrella = null;
        GameManager.instance.inventorySO.spriteFitnessBook = null;
        GameManager.instance.inventorySO.spriteEnergyDrink = null;
        GameManager.instance.inventorySO.spriteMentalHealthBook = null;
        GameManager.instance.inventorySO.spriteCoffee = null;
        GameManager.instance.inventorySO.spriteCreativityBook = null;
        GameManager.instance.inventorySO.spriteJumpingShoes = null;
        
    }

    public void NewSaveVariablesKey()
    {
        GameManager.instance.saveVariablesKey = "";
        GameManager.instance.keysCollected = new SerializableDictionary<string, bool>();
    }

}