using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MentalHealthBook : Item
{   
    private int bookStatPoints = 10;

    public MentalHealthBook()
    {
        itemName = "Mental Health Book";
        itemText = "A book that encompasses emotional, psychological, and social well-being, influencing cognition, perception, and behavior. Makes your mental health +10 as long as this is equipped.";
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
        GameManager.instance.playerStatsSO.tempStatPoint = GameManager.instance.playerStatsSO.mentalHealth;
        GameManager.instance.playerStatsSO.mentalHealth += bookStatPoints;
        GameManager.instance.MentalHealthStatCheck(true);
    }

    public override void OnUnequip()
    {
        GameManager.instance.playerStatsSO.mentalHealth = GameManager.instance.playerStatsSO.tempStatPoint;
        GameManager.instance.playerStatsSO.tempStatPoint = 0;
        GameManager.instance.MentalHealthStatCheck(false);
    }
}
