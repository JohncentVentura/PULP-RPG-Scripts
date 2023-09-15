using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SportsShoes : Item
{
    public SportsShoes()
    {
        itemName = "Sports Shoes";
        itemText = "A shoe designed for sports, exercising, or recreational activity. This allows the player to double jump while in the air";

        itemMaxFill = 1;
    }

    public override void ActivateEffect()
    {
        Debug.Log("ActivateEffect: " + itemName);
    }

    public override void PassiveEffect()
    {
        if (playerControls.isCollidingGround || playerControls.isCollidingWall)
        {
            playerControls.jumpCounter = 2;
            itemFill = 1;

            if(itemFill < itemMaxFill)
            {
                itemFill += Time.deltaTime;
            }
            else
            {
                itemFill = itemMaxFill;
            }
        }

        if (playerControls.jumpCounter == 0)
        {   
            if(itemFill > 0)
            {
                itemFill -= Time.deltaTime;
            }
            else
            {
                itemFill = 0;
            }
        }

        Debug.Log("PassiveEffect: " + itemName);
    }

    public override void OnEquip()
    {
        Debug.Log("OnEquip: "+itemName);
    }
    
    public override void OnUnequip()
    {
        Debug.Log("OnUnequip: "+itemName);
    }
}
