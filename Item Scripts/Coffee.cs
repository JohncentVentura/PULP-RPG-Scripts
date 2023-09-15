using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coffee : Item
{   
    private bool isOnUnequipActive = false;

    public Coffee()
    {
        itemName = "Coffee";
        itemText = "Whether it be energy, socialization, or tradition, cultivation of coffee served as a motivating force of the world. Press 'K' to dash switfly and prolonged airborne, check how much time you have to dash swiftly in the item fill bar.";
        itemMaxFill = 3;
    }

    public override void ActivateEffect()
    {
        if ((!GameManager.instance.inventorySO.isItemUsed) && (itemFill == itemMaxFill))
        {
            itemFill = 3;
            playerControls.dashSpeed = 17;
            playerControls.dashTime = 0.7f;
            playerControls.dashCooldown = 0.7f;
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
        playerControls.dashSpeed = 14;
        playerControls.dashTime = 0.3f;
        playerControls.dashCooldown = 0.3f;
    }
}
