using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharismaBook : Item
{
    private int bookStatPoints = 15;

    public CharismaBook()
    {
        itemName = "Charisma Book";
        itemText = "A book that is good for enhancing communication skills and also boost one confidence, makes your charisma +15 as long as this is equipped.";
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
        GameManager.instance.playerStatsSO.tempStatPoint = GameManager.instance.playerStatsSO.charisma;
        GameManager.instance.playerStatsSO.charisma += bookStatPoints;
    }

    public override void OnUnequip()
    {
        GameManager.instance.playerStatsSO.charisma = GameManager.instance.playerStatsSO.tempStatPoint;
        GameManager.instance.playerStatsSO.tempStatPoint = 0;
    }
}
