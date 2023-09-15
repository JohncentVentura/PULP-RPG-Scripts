using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]

public class PlayerStatsData
{   
    public float xPosition;
    public float yPosition;
    public string gender;
    public RuntimeAnimatorController runtimeAnimatorController;

    public int maxEnergy;
    public int energy;
    public int cash;
    public int fitness;
    public int logic;
    public int creativity;
    public int charisma;
    public int mentalHealth;
    public int tempStatPoint;

    public string equippedItemName;

    public int gamepadCount;

    public PlayerStatsData() //Constructor, initialize the starting value of attributes
    {   
        /*
        xPosition = 0;
        yPosition = 0;
        gender = "";
        runtimeAnimatorController = null;

        maxEnergy = 3;
        energy = maxEnergy;
        cash = 0;
        fitness = 1;
        logic = 1;
        creativity = 1;
        charisma = 1;
        mentalHealth = 1;

        equippedItemName = "";
        */
    }
}
