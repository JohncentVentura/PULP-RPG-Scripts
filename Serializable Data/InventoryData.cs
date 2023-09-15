using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]

public class InventoryData
{   
    public bool isItemUsed;

    /*
    public bool addSportsShoes;
    public bool addVitaminCapsule;

    public Sprite spriteSportsShoes;
    public Sprite spriteVitaminCapsule;
    */

    public bool addFirstAid;
    public bool addCharismaBook;
    public bool addESDShoesBook;
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
    public Sprite spriteESDShoesBook;
    public Sprite spriteLogicBook;
    public Sprite spriteUmbrella;
    public Sprite spriteFitnessBook;
    public Sprite spriteEnergyDrink;
    public Sprite spriteMentalHealthBook;
    public Sprite spriteCoffee;
    public Sprite spriteCreativityBook;
    public Sprite spriteJumpingShoes;


    public InventoryData() //Constructor, initialize the starting value of attributes
    {   
        isItemUsed = false;

        /*
        addSportsShoes = false;
        addVitaminCapsule = false;

        spriteSportsShoes = null;
        spriteVitaminCapsule = null;
        */
    }
}
