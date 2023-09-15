using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ESDShoes : Item
{   
    private int currentEnergy;

    public ESDShoes()
    {
        itemName = "ESD Shoes";
        itemText = "Also known as electrostatic dissipative shoes. The purpose of this type of footwear is to reduce static discharge. Press 'K' to temporary be immune to damage, check its duration in the item fill bar.";
        itemMaxFill = 6;
    }

    public override void ActivateEffect()
    {
        if ((!GameManager.instance.inventorySO.isItemUsed) && (itemFill == itemMaxFill))
        {
            itemFill = 6;
            GameManager.instance.inventorySO.isItemUsed = true;
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
            currentEnergy = GameManager.instance.playerStatsSO.energy;

            if (itemFill < itemMaxFill)
            {
                itemFill += Time.deltaTime;
            }
            else
            {
                itemFill = itemMaxFill;
            }
        }

        //Invincbile Timer
        if (GameManager.instance.inventorySO.isItemUsed && itemFill > 0)
        {
            GameManager.instance.playerStatsSO.energy = currentEnergy;
        }
    }

    public override void OnEquip()
    {   
        currentEnergy = GameManager.instance.playerStatsSO.energy;
    }

    public override void OnUnequip()
    {

    }
}
