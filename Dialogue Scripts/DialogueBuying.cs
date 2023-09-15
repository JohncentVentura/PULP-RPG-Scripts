using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueBuying : MonoBehaviour, IDataPersistence
{
    [HideInInspector] public SpriteRenderer spriteRenderer;
    public string itemName;
    [SerializeField] private GameObject visualCueInteract;

    [Header("Ink JSON")]
    [SerializeField] private TextAsset inkJSON;

    private bool isPlayerDetected;
    private string buyingItem = "";
    //private bool isItemBought;

    [SerializeField] private AudioClip audioClip;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        visualCueInteract.SetActive(false);
        //isItemBought = false;
        enabled = false;
    }

    void Update()
    {
        //isPlayerDetected
        if (isPlayerDetected && !DialogueManager.instance.isDialoguePlaying)
        {
            visualCueInteract.SetActive(true);

            if (Input.GetButtonDown("Jump"))
            {
                //TEMPORARY, USE DIALOGUE WHEN BUYING ITEMS
                //GameManager.instance.SetItemAdded(itemName, true, spriteRenderer.sprite);

                DialogueManager.instance.EnterDialogueMode(inkJSON);
            }
        }
        else
        {
            visualCueInteract.SetActive(false);
        }

        //Check player's cash if they can afford it
        ((Ink.Runtime.IntValue)DialogueManager.instance.GetVariableState("playerCash")).value = GameManager.instance.playerStatsSO.cash;
        //Check player's charisma for discount
        ((Ink.Runtime.IntValue)DialogueManager.instance.GetVariableState("playerCharisma")).value = GameManager.instance.playerStatsSO.charisma;

        //If player's cash can afford the item, buyingItem will get that value
        buyingItem = ((Ink.Runtime.StringValue)DialogueManager.instance.GetVariableState("buyingItem")).value;

        //Items
        if (buyingItem == "First Aid")
        {   
            if(GameManager.instance.playerStatsSO.cash >= 300 && GameManager.instance.playerStatsSO.charisma >= 3)
            {   
                BuyItem("First Aid", 300);
            }
            else if (GameManager.instance.playerStatsSO.cash >= 400)
            {   
                BuyItem("First Aid", 400);
            }
            ((Ink.Runtime.StringValue)DialogueManager.instance.GetVariableState("buyingItem")).value = ""; //buyingItem = "" to avoid recursion
        }

        if (buyingItem == "Charisma Book")
        {
            if(GameManager.instance.playerStatsSO.cash >= 700 && GameManager.instance.playerStatsSO.charisma >= 6)
            {   
                BuyItem("Charisma Book", 700);
            }
            else if (GameManager.instance.playerStatsSO.cash >= 800)
            {   
                BuyItem("Charisma Book", 800);
            }
            ((Ink.Runtime.StringValue)DialogueManager.instance.GetVariableState("buyingItem")).value = ""; //buyingItem = "" to avoid recursion
        }

        if (buyingItem == "ESD Shoes")
        {
            if(GameManager.instance.playerStatsSO.cash >= 500 && GameManager.instance.playerStatsSO.charisma >= 9)
            {   
                BuyItem("ESD Shoes", 500);
            }
            else if (GameManager.instance.playerStatsSO.cash >= 600)
            {   
                BuyItem("ESD Shoes", 600);
            }
            ((Ink.Runtime.StringValue)DialogueManager.instance.GetVariableState("buyingItem")).value = ""; //buyingItem = "" to avoid recursion
        }

        if (buyingItem == "Logic Book")
        {
            if(GameManager.instance.playerStatsSO.cash >= 900 && GameManager.instance.playerStatsSO.charisma >= 12)
            {   
                BuyItem("Logic Book", 900);
            }
            else if (GameManager.instance.playerStatsSO.cash >= 1000)
            {   
                BuyItem("Logic Book", 1000);
            }
            ((Ink.Runtime.StringValue)DialogueManager.instance.GetVariableState("buyingItem")).value = ""; //buyingItem = "" to avoid recursion
        }

        if (buyingItem == "Umbrella")
        {
            if(GameManager.instance.playerStatsSO.cash >= 700 && GameManager.instance.playerStatsSO.charisma >= 15)
            {   
                BuyItem("Umbrella", 700);
            }
            else if (GameManager.instance.playerStatsSO.cash >= 800)
            {   
                BuyItem("Umbrella", 800);
            }
            ((Ink.Runtime.StringValue)DialogueManager.instance.GetVariableState("buyingItem")).value = ""; //buyingItem = "" to avoid recursion
        }

        if (buyingItem == "Fitness Book")
        {
            if(GameManager.instance.playerStatsSO.cash >= 1000 && GameManager.instance.playerStatsSO.charisma >= 18)
            {   
                BuyItem("Fitness Book", 1000);
            }
            else if (GameManager.instance.playerStatsSO.cash >= 1200)
            {   
                BuyItem("Fitness Book", 1200);
            }
            ((Ink.Runtime.StringValue)DialogueManager.instance.GetVariableState("buyingItem")).value = ""; //buyingItem = "" to avoid recursion
        }

        if (buyingItem == "Energy Drink")
        {
            if(GameManager.instance.playerStatsSO.cash >= 900 && GameManager.instance.playerStatsSO.charisma >= 21)
            {   
                BuyItem("Energy Drink", 900);
            }
            else if (GameManager.instance.playerStatsSO.cash >= 1000)
            {   
                BuyItem("Energy Drink", 1000);
            }
            ((Ink.Runtime.StringValue)DialogueManager.instance.GetVariableState("buyingItem")).value = ""; //buyingItem = "" to avoid recursion
        }

        if (buyingItem == "Mental Health Book")
        {
            if(GameManager.instance.playerStatsSO.cash >= 1200 && GameManager.instance.playerStatsSO.charisma >= 24)
            {   
                BuyItem("Mental Health Book", 1200);
            }
            else if (GameManager.instance.playerStatsSO.cash >= 1400)
            {   
                BuyItem("Mental Health Book", 1400);
            }
            ((Ink.Runtime.StringValue)DialogueManager.instance.GetVariableState("buyingItem")).value = ""; //buyingItem = "" to avoid recursion
        }

        if (buyingItem == "Coffee")
        {
            if(GameManager.instance.playerStatsSO.cash >= 1000 && GameManager.instance.playerStatsSO.charisma >= 27)
            {   
                BuyItem("Coffee", 1000);
            }
            else if (GameManager.instance.playerStatsSO.cash >= 1200)
            {   
                BuyItem("Coffee", 1200);
            }
            ((Ink.Runtime.StringValue)DialogueManager.instance.GetVariableState("buyingItem")).value = ""; //buyingItem = "" to avoid recursion
        }

        if (buyingItem == "Creativity Book")
        {
            if(GameManager.instance.playerStatsSO.cash >= 1400 && GameManager.instance.playerStatsSO.charisma >= 30)
            {   
                BuyItem("Creativity Book", 1400);
            }
            else if (GameManager.instance.playerStatsSO.cash >= 1600)
            {   
                BuyItem("Creativity Book", 1600);
            }
            ((Ink.Runtime.StringValue)DialogueManager.instance.GetVariableState("buyingItem")).value = ""; //buyingItem = "" to avoid recursion
        }

        if (buyingItem == "Jumping Shoes")
        {
            if(GameManager.instance.playerStatsSO.cash >= 1600 && GameManager.instance.playerStatsSO.charisma >= 33)
            {   
                BuyItem("Jumping Shoes", 1600);
            }
            else if (GameManager.instance.playerStatsSO.cash >= 1800)
            {   
                BuyItem("Jumping Shoes", 1800);
            }
            ((Ink.Runtime.StringValue)DialogueManager.instance.GetVariableState("buyingItem")).value = ""; //buyingItem = "" to avoid recursion
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            isPlayerDetected = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            isPlayerDetected = false;
        }
    }

    void OnBecameVisible()
    {
        enabled = true;
        //isBuyingEnabled();
    }

    void OnBecameInvisible()
    {
        enabled = false;
    }

    public void LoadData(GameData gameData)
    {
        isBuyingEnabled();
    }

    public void SaveData(GameData gameData)
    {   
        

    }   

    private void isBuyingEnabled()
    {
        /*
        if (itemName == "Sports Shoes" && GameManager.instance.inventorySO.addSportsShoes == true) this.gameObject.SetActive(false);
        else if (itemName == "Vitamin Capsule" && GameManager.instance.inventorySO.addVitaminCapsule == true) this.gameObject.SetActive(false);
        */

        if (itemName == "First Aid" && GameManager.instance.inventorySO.addFirstAid == true) this.gameObject.SetActive(false);
        else if (itemName == "Charisma Book" && GameManager.instance.inventorySO.addCharismaBook == true) this.gameObject.SetActive(false);
        else if (itemName == "ESD Shoes" && GameManager.instance.inventorySO.addESDShoes == true) this.gameObject.SetActive(false);
        else if (itemName == "Logic Book" && GameManager.instance.inventorySO.addLogicBook == true) this.gameObject.SetActive(false);
        else if (itemName == "Umbrella" && GameManager.instance.inventorySO.addUmbrella == true) this.gameObject.SetActive(false);
        else if (itemName == "Fitness Book" && GameManager.instance.inventorySO.addFitnessBook == true) this.gameObject.SetActive(false);
        else if (itemName == "Energy Drink" && GameManager.instance.inventorySO.addEnergyDrink == true) this.gameObject.SetActive(false);
        else if (itemName == "Mental Health Book" && GameManager.instance.inventorySO.addMentalHealthBook == true) this.gameObject.SetActive(false);
        else if (itemName == "Coffee" && GameManager.instance.inventorySO.addCoffee == true) this.gameObject.SetActive(false);
        else if (itemName == "Creativity Book" && GameManager.instance.inventorySO.addCreativityBook == true) this.gameObject.SetActive(false);
        else if (itemName == "Jumping Shoes" && GameManager.instance.inventorySO.addJumpingShoes == true) this.gameObject.SetActive(false);
        else enabled = true;
    }

    private void BuyItem(string localItemName, int cost)
    {   
        AudioManager.instance.PlaySFX(audioClip);
        GameManager.instance.SetItemAdded(localItemName, true, spriteRenderer.sprite);
        GameManager.instance.playerStatsSO.cash -= cost;
        DataPersistenceManager.instance.SaveGame();

        /*
        if (GameManager.instance.IsItemAdded(localItemName)) 
        {   
            isBuyingEnabled();
        }
        */
        
        DeactiveWithTag(localItemName);
    }

    private void DeactiveWithTag(string tag)
    {
        GameObject[] deactivateObject;
        deactivateObject = GameObject.FindGameObjectsWithTag(tag);
        foreach (GameObject oneObject in deactivateObject)
        {
            oneObject.SetActive(false);
        }
    }

    /*
    //Items
        if (buyingItem == "First Aid")
        {   
            if(GameManager.instance.playerStatsSO.cash >= 300 && GameManager.instance.playerStatsSO.charisma >= 3)
            {   
                BuyItem("First Aid", 300);
                
                GameManager.instance.SetItemAdded("First Aid", true, spriteRenderer.sprite);
                GameManager.instance.playerStatsSO.cash -= 300;
                if (itemName == "First Aid") this.gameObject.SetActive(false);
                DataPersistenceManager.instance.SaveGame();
            }
            else if (GameManager.instance.playerStatsSO.cash >= 400)
            {   
                GameManager.instance.SetItemAdded("First Aid", true, spriteRenderer.sprite);
                GameManager.instance.playerStatsSO.cash -= 400;
                if (itemName == "First Aid") this.gameObject.SetActive(false);
                DataPersistenceManager.instance.SaveGame();
            }
            Debug.Log("DialogueBuying First Aid");
            ((Ink.Runtime.StringValue)DialogueManager.instance.GetVariableState("buyingItem")).value = ""; //buyingItem = "" to avoid recursion
        }

        if (buyingItem == "Charisma Book")
        {
            if (GameManager.instance.playerStatsSO.cash >= 800)
            {
                GameManager.instance.SetItemAdded("Charisma Book", true, spriteRenderer.sprite);
                GameManager.instance.playerStatsSO.cash -= 800;
                if (itemName == "Charisma Book") this.gameObject.SetActive(false);
                DataPersistenceManager.instance.SaveGame();
            }
            Debug.Log("DialogueBuying Charisma Book");
            ((Ink.Runtime.StringValue)DialogueManager.instance.GetVariableState("buyingItem")).value = ""; //buyingItem = "" to avoid recursion
        }

        if (buyingItem == "ESD Shoes")
        {
            if (GameManager.instance.playerStatsSO.cash >= 600)
            {
                GameManager.instance.SetItemAdded("ESD Shoes", true, spriteRenderer.sprite);
                GameManager.instance.playerStatsSO.cash -= 600;
                if (itemName == "ESD Shoes") this.gameObject.SetActive(false);
                DataPersistenceManager.instance.SaveGame();
            }
            Debug.Log("DialogueBuying ESD Shoes");
            ((Ink.Runtime.StringValue)DialogueManager.instance.GetVariableState("buyingItem")).value = ""; //buyingItem = "" to avoid recursion
        }

        if (buyingItem == "Logic Book")
        {
            if (GameManager.instance.playerStatsSO.cash >= 1000)
            {
                GameManager.instance.SetItemAdded("Logic Book", true, spriteRenderer.sprite);
                GameManager.instance.playerStatsSO.cash -= 1000;
                if (itemName == "Logic Book") this.gameObject.SetActive(false);
                DataPersistenceManager.instance.SaveGame();
            }
            Debug.Log("DialogueBuying Logic Book");
            ((Ink.Runtime.StringValue)DialogueManager.instance.GetVariableState("buyingItem")).value = ""; //buyingItem = "" to avoid recursion
        }

        if (buyingItem == "Umbrella")
        {
            if (GameManager.instance.playerStatsSO.cash >= 800)
            {
                GameManager.instance.SetItemAdded("Umbrella", true, spriteRenderer.sprite);
                GameManager.instance.playerStatsSO.cash -= 800;
                if (itemName == "Umbrella") this.gameObject.SetActive(false);
                DataPersistenceManager.instance.SaveGame();
            }
            Debug.Log("DialogueBuying Umbrella");
            ((Ink.Runtime.StringValue)DialogueManager.instance.GetVariableState("buyingItem")).value = ""; //buyingItem = "" to avoid recursion
        }

        if (buyingItem == "Fitness Book")
        {
            if (GameManager.instance.playerStatsSO.cash >= 1200)
            {
                GameManager.instance.SetItemAdded("Fitness Book", true, spriteRenderer.sprite);
                GameManager.instance.playerStatsSO.cash -= 1200;
                if (itemName == "Fitness Book") this.gameObject.SetActive(false);
                DataPersistenceManager.instance.SaveGame();
            }
            Debug.Log("DialogueBuying Fitness Book");
            ((Ink.Runtime.StringValue)DialogueManager.instance.GetVariableState("buyingItem")).value = ""; //buyingItem = "" to avoid recursion
        }

        if (buyingItem == "Energy Drink")
        {
            if (GameManager.instance.playerStatsSO.cash >= 1000)
            {
                GameManager.instance.SetItemAdded("Energy Drink", true, spriteRenderer.sprite);
                GameManager.instance.playerStatsSO.cash -= 1000;
                if (itemName == "Energy Drink") this.gameObject.SetActive(false);
                DataPersistenceManager.instance.SaveGame();
            }
            Debug.Log("DialogueBuying Energy Drink");
            ((Ink.Runtime.StringValue)DialogueManager.instance.GetVariableState("buyingItem")).value = ""; //buyingItem = "" to avoid recursion
        }

        if (buyingItem == "Mental Health Book")
        {
            if (GameManager.instance.playerStatsSO.cash >= 1400)
            {
                GameManager.instance.SetItemAdded("Mental Health Book", true, spriteRenderer.sprite);
                GameManager.instance.playerStatsSO.cash -= 1400;
                if (itemName == "Mental Health Book") this.gameObject.SetActive(false);
                DataPersistenceManager.instance.SaveGame();
            }
            Debug.Log("DialogueBuying Mental Health Book");
            ((Ink.Runtime.StringValue)DialogueManager.instance.GetVariableState("buyingItem")).value = ""; //buyingItem = "" to avoid recursion
        }

        if (buyingItem == "Coffee")
        {
            if (GameManager.instance.playerStatsSO.cash >= 1200)
            {
                GameManager.instance.SetItemAdded("Coffee", true, spriteRenderer.sprite);
                GameManager.instance.playerStatsSO.cash -= 1200;
                if (itemName == "Coffee") this.gameObject.SetActive(false);
                DataPersistenceManager.instance.SaveGame();
            }
            Debug.Log("DialogueBuying Coffee");
            ((Ink.Runtime.StringValue)DialogueManager.instance.GetVariableState("buyingItem")).value = ""; //buyingItem = "" to avoid recursion
        }

        if (buyingItem == "Creativity Book")
        {
            if (GameManager.instance.playerStatsSO.cash >= 1600)
            {
                GameManager.instance.SetItemAdded("Creativity Book", true, spriteRenderer.sprite);
                GameManager.instance.playerStatsSO.cash -= 1600;
                if (itemName == "Creativity Book") this.gameObject.SetActive(false);
                DataPersistenceManager.instance.SaveGame();
            }
            Debug.Log("DialogueBuying Creativity Book");
            ((Ink.Runtime.StringValue)DialogueManager.instance.GetVariableState("buyingItem")).value = ""; //buyingItem = "" to avoid recursion
        }

        if (buyingItem == "Jumping Shoes")
        {
            if (GameManager.instance.playerStatsSO.cash >= 1800)
            {
                GameManager.instance.SetItemAdded("Jumping Shoes", true, spriteRenderer.sprite);
                GameManager.instance.playerStatsSO.cash -= 1800;
                if (itemName == "Jumping Shoes") this.gameObject.SetActive(false);
                DataPersistenceManager.instance.SaveGame();
            }
            Debug.Log("DialogueBuying Jumping Shoes");
            ((Ink.Runtime.StringValue)DialogueManager.instance.GetVariableState("buyingItem")).value = ""; //buyingItem = "" to avoid recursion
        }
    */

}
