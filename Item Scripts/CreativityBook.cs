using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreativityBook : Item
{   
    private int bookStatPoints = 20;

    public CreativityBook()
    {
        itemName = "Creativity Book";
        itemText = "A book that makes you think that is art imitates nature, or nature imitates art? Makes your creativity +20 as long as this is equipped.";
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
        GameManager.instance.playerStatsSO.tempStatPoint = GameManager.instance.playerStatsSO.creativity;
        GameManager.instance.playerStatsSO.creativity += bookStatPoints;
    }

    public override void OnUnequip()
    {
        GameManager.instance.playerStatsSO.creativity = GameManager.instance.playerStatsSO.tempStatPoint;
        GameManager.instance.playerStatsSO.tempStatPoint = 0;
    }
}
