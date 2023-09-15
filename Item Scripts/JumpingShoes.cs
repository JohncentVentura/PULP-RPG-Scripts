using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpingShoes : Item
{
    public JumpingShoes()
    {
        itemName = "Jumping Shoes";
        itemText = "A shoe designed for sports, exercising, or recreational activity. This allows the player to double jump while in the air.";
        itemMaxFill = 1;
    }

    public override void ActivateEffect()
    {

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
    }

    public override void OnEquip()
    {

    }
    
    public override void OnUnequip()
    {

    }

}
