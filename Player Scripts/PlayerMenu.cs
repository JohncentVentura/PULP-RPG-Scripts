using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class PlayerMenu : Menu
{
    [Header("Main UI")]
    [SerializeField] PlayerControls playerControls;
    [SerializeField] private GameObject playerMenuPanel;
    [SerializeField] private Button continueButton;

    [Header("Stats")]
    [SerializeField] private TextMeshProUGUI fitnesssText;
    [SerializeField] private TextMeshProUGUI logicText;
    [SerializeField] private TextMeshProUGUI creativityText;
    [SerializeField] private TextMeshProUGUI charismaText;
    [SerializeField] private TextMeshProUGUI mentalHealthText;

    [Header("Player UI")]
    [SerializeField] private Image playerImage;
    [SerializeField] private Animator playerImageAnimator;
    [SerializeField] private RuntimeAnimatorController playerMale;
    [SerializeField] private RuntimeAnimatorController playerFemale;
    //[SerializeField] private Button nextSkinButton;
    //[SerializeField] private Button prevSkinButton;

    [Header("Inventory UI")]
    [SerializeField] private Button invUpButton;
    [SerializeField] private Button invDownButton;
    [SerializeField] private TextMeshProUGUI firstItemText;
    [SerializeField] private TextMeshProUGUI secondItemtext;
    [SerializeField] private TextMeshProUGUI thirdItemText;
    [SerializeField] private TextMeshProUGUI fourthItemText;
    [SerializeField] private TextMeshProUGUI fifthItemText;

    [Header("Item UI")]
    [SerializeField] private Image itemImage;
    [SerializeField] private TextMeshProUGUI itemText;
    [SerializeField] private Button equipButton;
    [SerializeField] private TextMeshProUGUI equipButtonText;

    //Global Variables
    [HideInInspector] public List<Item> inventory;
    private int pointedID = 0;

    [Header("Audio")]
    public AudioClip openMenuClip;
    public AudioClip clickClip;
    public AudioClip equipClip;
    public AudioClip unequipClip;

    private void Start()
    {
        //DeactivateMenu();
        SetFirstSelectedButton(equipButton);
        Debug.Log("Start PlayerMenu");
    }

    public void ActivateMenu()
    {
        //TEMPORARY, CHANGE IF THERE IS A SAVEPOINT
        //DataPersistenceManager.instance.SaveGame();

        AudioManager.instance.PlaySFX(openMenuClip);

        //Default Activate Menu Settings
        playerControls.gameObject.SetActive(false);
        playerMenuPanel.SetActive(true);

        inventory = new List<Item>();

        for (int i = 0; i < GameManager.instance.itemsArray.Length; i++)
        {
            if (GameManager.instance.playerStatsSO.equippedItemName == GameManager.instance.itemsArray[i].itemName)
            {
                pointedID = i;
            }
            else
            {
                pointedID = 0;
            }
        }
        Debug.Log("pointedID: "+pointedID);

        fitnesssText.text = GameManager.instance.playerStatsSO.fitness.ToString();
        logicText.text = GameManager.instance.playerStatsSO.logic.ToString();
        creativityText.text = GameManager.instance.playerStatsSO.creativity.ToString();
        charismaText.text = GameManager.instance.playerStatsSO.charisma.ToString();
        mentalHealthText.text = GameManager.instance.playerStatsSO.mentalHealth.ToString();

        if (GameManager.instance.playerStatsSO.gender == "Male") playerImageAnimator.runtimeAnimatorController = playerMale;
        else if (GameManager.instance.playerStatsSO.gender == "Female") playerImageAnimator.runtimeAnimatorController = playerFemale;

        firstItemText.text = "";
        secondItemtext.text = "";
        thirdItemText.text = "";
        fourthItemText.text = "";
        fifthItemText.text = "";

        itemImage.sprite = null;
        itemText.text = "";

        //Updated Activate Menu Settings
        SetStatsText();

        GameManager.instance.ResetItemsArray(); //So item images will work
        SetInvList();
        SetInvItemsText();

        //invUpButton.interactable = false if we're at the first item
        if (pointedID == 0) invUpButton.interactable = false;
        else invUpButton.interactable = true;

        //Debug.Log("inventory.Count: " + inventory.Count);

        //invDownButton.interactable = false if we're at the last item
        if ((pointedID == (inventory.Count - 1)) || (inventory.Count <= 0))
        {
            invDownButton.interactable = false;
        }
        else
        {
            invDownButton.interactable = true;
        }
    }

    public void DeactivateMenu()
    {
        playerControls.gameObject.SetActive(true);
        playerMenuPanel.SetActive(false);
    }

    public void OnContinueButtonClicked()
    {   
        DataPersistenceManager.instance.SaveGame();
        this.DeactivateMenu();
    }

    private void Update()
    {
        if(Input.GetButtonDown("Fire3") || Input.GetButtonDown("Cancel")) //For Controller
        {   
            OnContinueButtonClicked();
        }
    }

    public void SetStatsText()
    {
        fitnesssText.text = GameManager.instance.playerStatsSO.fitness.ToString();
        logicText.text = GameManager.instance.playerStatsSO.logic.ToString();
        creativityText.text = GameManager.instance.playerStatsSO.creativity.ToString();
        charismaText.text = GameManager.instance.playerStatsSO.charisma.ToString();
        mentalHealthText.text = GameManager.instance.playerStatsSO.mentalHealth.ToString();
    }

    public void SetInvList()
    {
        foreach (Item item in GameManager.instance.itemsArray)
        {
            if (GameManager.instance.IsItemAdded(item.itemName))
            {
                if (inventory.Contains(item)) //To avoid duplicating the item
                {
                    inventory.Remove(item);
                    inventory.Add(item);
                }
                else //To add the item since it doesn't have a duplicate
                {
                    inventory.Add(item);
                }
            }
        }
    }

    public void OnInvUpButtonClicked()
    {   
        AudioManager.instance.PlaySFX(clickClip);

        pointedID--;

        if (pointedID == 0) //if we're at the first item
        {
            invUpButton.interactable = false;
            if(GameManager.instance.playerStatsSO.gamepadCount == 1) invDownButton.Select();
        }

        invDownButton.interactable = true;
        //ActivateMenu();
        SetInvItemsText();
    }

    public void OnInvDownButtonClicked()
    {   
        AudioManager.instance.PlaySFX(clickClip);

        pointedID++;

        if (pointedID == (inventory.Count - 1)) //if we're at the last item
        {
            invDownButton.interactable = false;
            if(GameManager.instance.playerStatsSO.gamepadCount == 1) invUpButton.Select();
        }

        invUpButton.interactable = true;
        //ActivateMenu();
        SetInvItemsText();
    }

    private void SetInvItemsText()
    {
        try
        {
            if (inventory[pointedID - 2] != null) //2
            {
                firstItemText.text = inventory[pointedID - 2].itemName;
                //Debug.Log("try firstItemText");
            }
        }
        catch (Exception e)
        {
            firstItemText.text = "";
            //Debug.LogException(e);
        }

        try
        {
            if (inventory[pointedID - 1] != null) //1
            {
                secondItemtext.text = inventory[pointedID - 1].itemName;
                //Debug.Log("try secondItemtext");
            }
        }
        catch (Exception e)
        {
            secondItemtext.text = "";
            //Debug.LogException(e);
        }

        try
        {
            if (inventory[pointedID] != null) //inventory[pointedID - 1] != null
            {
                thirdItemText.text = inventory[pointedID].itemName;
                itemImage.sprite = inventory[pointedID].itemSprite;
                itemText.text = inventory[pointedID].itemText;

                if (GameManager.instance.playerStatsSO.equippedItemName == inventory[pointedID].itemName)
                {
                    equipButtonText.text = "Unequip";
                }
                else
                {
                    equipButtonText.text = "Equip";
                }

                //Debug.Log("try thirdItemText");
            }
        }
        catch (Exception e)
        {
            thirdItemText.text = "";
            //Debug.LogException(e);
        }

        try
        {
            if (inventory[pointedID + 1] != null)
            {
                fourthItemText.text = inventory[pointedID + 1].itemName;
                //Debug.Log("try fourthItemText");
            }
        }
        catch (Exception e)
        {
            fourthItemText.text = "";
            //Debug.LogException(e);
        }

        try
        {
            if (inventory[pointedID + 2] != null)
            {
                fifthItemText.text = inventory[pointedID + 2].itemName;
                //Debug.Log("try fifthItemText");
            }
        }
        catch (Exception e)
        {
            fifthItemText.text = "";
            //Debug.LogException(e);
        }
    }

    public void OnEquipButtonClick()
    {   
        AudioManager.instance.PlaySFX(equipClip);
        if (equipButtonText.text == "Equip")
        {   
            if(playerControls.equippedItem != null)
            {
                playerControls.equippedItem.OnUnequip();
            }
            GameManager.instance.playerStatsSO.equippedItemName = inventory[pointedID].itemName;
            DataPersistenceManager.instance.SaveGame();
            DataPersistenceManager.instance.LoadGame();
            playerControls.equippedItem.OnEquip(); //Commented sometimes since it is called again when playing the game again
            //ActivateMenu();
            SetStatsText();
            SetInvItemsText();
        }
        else if (equipButtonText.text == "Unequip")
        {   
            AudioManager.instance.PlaySFX(unequipClip);
            GameManager.instance.playerStatsSO.equippedItemName = null;
            playerControls.equippedItem.OnUnequip();
            DataPersistenceManager.instance.SaveGame();
            DataPersistenceManager.instance.LoadGame();
            //ActivateMenu();
            SetStatsText();
            SetInvItemsText();
        }

    }

}
