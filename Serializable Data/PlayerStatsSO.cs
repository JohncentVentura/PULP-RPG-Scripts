using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerStatsSO", menuName = "ScriptableObjects/PlayerStatsScriptableObject", order = 1)]

public class PlayerStatsSO : ScriptableObject
{   
    public float xPosition;
    public float yPosition;
    public string gender;
    public RuntimeAnimatorController runtimeAnimatorController; //If the animation is boy or girl

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
}