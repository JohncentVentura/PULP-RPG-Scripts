using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LogicBook : Item
{   
    private int bookStatPoints = 20;

    public LogicBook()
    {
        itemName = "Logic Book";
        itemText = "A book about Data Structures and Algorithm that makes a thousands of IT students cry, makes your logic +20 as long as this is equipped.";
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
        GameManager.instance.playerStatsSO.tempStatPoint = GameManager.instance.playerStatsSO.logic;
        GameManager.instance.playerStatsSO.logic += bookStatPoints;
    }

    public override void OnUnequip()
    {
        GameManager.instance.playerStatsSO.logic = GameManager.instance.playerStatsSO.tempStatPoint;
        GameManager.instance.playerStatsSO.tempStatPoint = 0;
    }
}
