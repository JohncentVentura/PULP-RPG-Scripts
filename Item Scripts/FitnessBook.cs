using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FitnessBook : Item
{   
    private int bookStatPoints = 15;

    public FitnessBook()
    {
        itemName = "Fitness Book";
        itemText = "A book that teaches regular physical activity as it is one of the most important things you can do for your health, makes your fitness +15 as long as this is equipped. ";
        itemMaxFill = 1;
    }

    public override void ActivateEffect()
    {

    }

    public override void PassiveEffect()
    {

    }

    public override void OnEquip()
    {   
        GameManager.instance.playerStatsSO.tempStatPoint = GameManager.instance.playerStatsSO.fitness;
        GameManager.instance.playerStatsSO.fitness += bookStatPoints;
    }

    public override void OnUnequip()
    {
        GameManager.instance.playerStatsSO.fitness = GameManager.instance.playerStatsSO.tempStatPoint;
        GameManager.instance.playerStatsSO.tempStatPoint = 0;
    }
}
