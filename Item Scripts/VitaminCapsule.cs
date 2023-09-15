using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VitaminCapsule : Item
{
    public VitaminCapsule()
    {
        itemName = "Vitamin Capsule";
        itemText = "Vitamins and minerals are considered essential nutrients. Press the Space Bar to activate a shield";

        itemMaxFill = 1;
    }

    public override void ActivateEffect()
    {   
        Debug.Log("ActivateEffect: " + itemName);

        if(!GameManager.instance.inventorySO.isItemUsed)
        {   
            itemFill = 1;
            GameManager.instance.playerStatsSO.energy = GameManager.instance.playerStatsSO.energy + 1;
            GameManager.instance.inventorySO.isItemUsed = true;
            DataPersistenceManager.instance.SaveGame();
        }
    }

    public override void PassiveEffect()
    {   
        if(GameManager.instance.inventorySO.isItemUsed)
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
        
        if(!GameManager.instance.inventorySO.isItemUsed)
        {
            if(itemFill < itemMaxFill)
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
        Debug.Log("OnEquip: "+itemName);
    }
    
    public override void OnUnequip()
    {
        Debug.Log("OnUnequip: "+itemName);
    }
}
