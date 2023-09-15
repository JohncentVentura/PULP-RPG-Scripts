using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public abstract class Item
{   
    //For PlayerControls
    public PlayerControls playerControls;
    public string itemName; 
    public Sprite itemSprite;
    public string itemText;

    //For PlayerMenu
    public int itemMaxFill;
    public float itemFill;

    public abstract void ActivateEffect(); //Called in PlayerControls
    public abstract void PassiveEffect(); //Called in PlayerControls
    public abstract void OnEquip(); //Called in PlayerMenu
    public abstract void OnUnequip(); //Called in PlayerMenu
    
    /*
    public override void ActivateEffect()
    {
        Debug.Log("ActivateEffect: "+itemName);
    }

    public override void PassiveEffect()
    {
        Debug.Log("PassiveEffect: "+itemName);
    }
    
    public override void OnEquip()
    {
        Debug.Log("OnEquip: "+itemName);
    }

    public override void OnUnequip()
    {
        Debug.Log("OnUnequip: "+itemName);
    }
    */
    
}
