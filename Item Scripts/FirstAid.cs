using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstAid : Item
{
    public FirstAid()
    {
        itemName = "First Aid";
        itemText = "A set of materials used for giving emergency treatment to a sick or injured person. Press 'K' to heal 1 hit point,  can't be use if hit points is full or if the item fill bar is 0";
        itemMaxFill = 1;
    }

    public override void ActivateEffect()
    {
        Debug.Log("ActivateEffect: " + itemName);

        if ((!GameManager.instance.inventorySO.isItemUsed) && (itemFill == itemMaxFill))
        {
            if (GameManager.instance.playerStatsSO.energy < GameManager.instance.playerStatsSO.maxEnergy)
            {
                itemFill = 1;
                GameManager.instance.playerStatsSO.energy = GameManager.instance.playerStatsSO.energy + 1;
                GameManager.instance.inventorySO.isItemUsed = true;
                DataPersistenceManager.instance.SaveGame();
            }
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

    }

    public override void OnEquip()
    {

    }

    public override void OnUnequip()
    {

    }

}
