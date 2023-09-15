using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "InventorySO", menuName = "ScriptableObjects/InventoryScriptableObject", order = 3)]

public class InventorySO : ScriptableObject
{   
    public bool isItemUsed; //To know if ItemFill can be refilled

    /*
    public bool addSportsShoes;
    public bool addVitaminCapsule;

    public Sprite spriteSportsShoes;
    public Sprite spriteVitaminCapsule;
    */

    public bool addFirstAid;
    public bool addCharismaBook;
    public bool addESDShoes;
    public bool addLogicBook;
    public bool addUmbrella;
    public bool addFitnessBook;
    public bool addEnergyDrink;
    public bool addMentalHealthBook;
    public bool addCoffee;
    public bool addCreativityBook;
    public bool addJumpingShoes;

    public Sprite spriteFirstAid;
    public Sprite spriteCharismaBook;
    public Sprite spriteESDShoes;
    public Sprite spriteLogicBook;
    public Sprite spriteUmbrella;
    public Sprite spriteFitnessBook;
    public Sprite spriteEnergyDrink;
    public Sprite spriteMentalHealthBook;
    public Sprite spriteCoffee;
    public Sprite spriteCreativityBook;
    public Sprite spriteJumpingShoes;
    
}