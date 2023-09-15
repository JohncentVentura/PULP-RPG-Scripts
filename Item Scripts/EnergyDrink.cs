using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergyDrink : Item
{
    private bool isOnUnequipActive = false;

    public EnergyDrink()
    {
        itemName = "Energy Drink";
        itemText = "A type of drink containing stimulant compounds, usually caffeine, which is marketed as providing mental and physical stimulation. Press 'K' to temporarily move swiftly, check its duration in the item fill bar.";
        itemMaxFill = 5;
    }

    public override void ActivateEffect()
    {
        if ((!GameManager.instance.inventorySO.isItemUsed) && (itemFill == itemMaxFill))
        {
            itemFill = 5;
            playerControls.moveSpeed = 8;
            playerControls.acceleration = 10;
            playerControls.deceleration = 10;
            playerControls.velPower = 1.1f;
            playerControls.frictionAmount = 0.6f;
            GameManager.instance.inventorySO.isItemUsed = true;
            isOnUnequipActive = false;
            DataPersistenceManager.instance.SaveGame();
        }
    }

    public override void PassiveEffect()
    {
        if (GameManager.instance.inventorySO.isItemUsed)
        {
            if (itemFill > 0)
            {
                itemFill -= Time.deltaTime;
            }
            else
            {
                itemFill = 0;
                isOnUnequipActive = false;
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

        if(itemFill == itemMaxFill)
        {
            OnUnequip();
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
        playerControls.acceleration = 8;
        playerControls.deceleration = 8;
        playerControls.velPower = 0.9f;
        playerControls.frictionAmount = 0.4f;
    }
}
