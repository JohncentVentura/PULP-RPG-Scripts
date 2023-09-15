using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Umbrella : Item
{   
    private bool isOnUnequipActive = false; //So dashing movement is not interrupted since it also overiddes the rb.gravityScale

    public Umbrella()
    {
        itemName = "Umbrella";
        itemText = "A folding canopy that is designed to protect a person against rain or sunlight. Press 'K' while in mid air to glide slowly, check its duration in the item fill bar.";
        itemMaxFill = 5;
    }

    public override void ActivateEffect()
    {
        if ((!GameManager.instance.inventorySO.isItemUsed) && (itemFill == itemMaxFill) && (!playerControls.isCollidingGround || !playerControls.isCollidingWall))
        {
            itemFill = 5;
            playerControls.moveSpeed = 3;
            playerControls.rb.gravityScale = 0.2f;
            GameManager.instance.inventorySO.isItemUsed = true;
            isOnUnequipActive = false;
            DataPersistenceManager.instance.SaveGame();
        }
    }

    public override void PassiveEffect()
    {
        if (GameManager.instance.inventorySO.isItemUsed)
        {
            if (playerControls.isCollidingGround || playerControls.isCollidingWall || playerControls.isCollidingObject)
            {
                itemFill = 0;
            }

            if (itemFill > 0)
            {
                itemFill -= Time.deltaTime;
            }
            else
            {
                itemFill = 0;
            }
        }

        if (!GameManager.instance.inventorySO.isItemUsed)
        {
            if (itemFill < itemMaxFill)
            {
                itemFill += Time.deltaTime;
            }
            else
            {
                itemFill = itemMaxFill;
            }
        }

        if (itemFill <= 0 && isOnUnequipActive == false)
        {
            OnUnequip();
            isOnUnequipActive = true;
        }
    }

    public override void OnEquip()
    {

    }

    public override void OnUnequip()
    {
        playerControls.moveSpeed = 6;
        playerControls.rb.gravityScale = 2.2f;
    }
}
