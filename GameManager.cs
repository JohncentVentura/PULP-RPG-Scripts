using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour, IDataPersistence
{
    //Scriptable Objects
    public PlayerStatsSO playerStatsSO;
    public ScenesUnlockedSO scenesUnlockedSO;
    public InventorySO inventorySO;

    //Dialogues & Collectibles
    public string saveVariablesKey; //Will only update once the DialogueManager is SetActive to true;
    public bool isReplayingChapter = false;
    public SerializableDictionary<string, bool> keysCollected;

    //Global Variables
    public string[] scenesArray = { "StartMenuScene", "Chapter1Scene", "Chapter2Scene", "Chapter3Scene", "Chapter4Scene", "Chapter5Scene", "Chapter6Scene" };
    public string[] chaptersArray = { "Chapter 0: Null", "Chapter 1: Highway", "Chapter 2: Computer Lab", "Chapter 3: Hallway", "Chapter 4: Town", "Chapter 5: Library", "Chapter 6: Capstone Defense" };
    public Item[] itemsArray;
    public static GameManager instance { get; private set; } //GameManager is a singleton

    //Items
    //private SportsShoes sportsShoes;
    //private VitaminCapsule vitaminCapsule;
    private FirstAid firstAid;
    private CharismaBook charismaBook;
    private ESDShoes esdShoes;
    private LogicBook logicBook;
    private Umbrella umbrella;
    private FitnessBook fitnessBook;
    private EnergyDrink energyDrink;
    private MentalHealthBook mentalHealthBook;
    private Coffee coffee;
    private CreativityBook creativityBook;
    private JumpingShoes jumpingShoes;

    //Item Sprites
    [Header("Item Sprites")]
    [SerializeField] private Sprite spriteFirstAid;
    [SerializeField] private Sprite spriteCharismaBook;
    [SerializeField] private Sprite spriteESDShoes;
    [SerializeField] private Sprite spriteLogicBook;
    [SerializeField] private Sprite spriteUmbrella;
    [SerializeField] private Sprite spriteFitnessBook;
    [SerializeField] private Sprite spriteEnergyDrink;
    [SerializeField] private Sprite spriteMentalHealthBook;
    [SerializeField] private Sprite spriteCoffee;
    [SerializeField] private Sprite spriteCreativityBook;
    [SerializeField] private Sprite spriteJumpingShoes;

    //Previous Player Stats
    private int prevFitness;
    private int prevLogic;
    private int prevCreativity;
    private int prevCharisma;

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(this.gameObject); //If there is another instance of this class, destroy it because this class is a singleton
            return;
        }
        instance = this; //If there are no instance of this class, this becomes the first & only instance of this class
        DontDestroyOnLoad(this.gameObject); //The gameObject must be in the root of a scene, to persist when loading to other scenes
    }

    private void Start()
    {
        //Items
        /*
        sportsShoes = new SportsShoes();
        vitaminCapsule = new VitaminCapsule();
        */
        firstAid = new FirstAid();
        charismaBook = new CharismaBook();
        esdShoes = new ESDShoes();
        logicBook = new LogicBook();
        umbrella = new Umbrella();
        fitnessBook = new FitnessBook();
        energyDrink = new EnergyDrink();
        mentalHealthBook = new MentalHealthBook();
        coffee = new Coffee();
        creativityBook = new CreativityBook();
        jumpingShoes = new JumpingShoes();

        itemsArray = new Item[] { firstAid, charismaBook, esdShoes, logicBook, umbrella, fitnessBook, energyDrink, mentalHealthBook, coffee, creativityBook, jumpingShoes };
    }

    private void Update()
    {
        if (Input.GetButtonDown("JoystickTrigger") && GameManager.instance.playerStatsSO.gamepadCount == 0)
        {
            GameManager.instance.playerStatsSO.gamepadCount = 1;
            Debug.Log("Update JoystickTrigger");
        }
    }

    public void PrepareGameScene(string sceneName, float xPosition, float yPosition)
    {
        scenesUnlockedSO.sceneName = sceneName;
        playerStatsSO.xPosition = xPosition;
        playerStatsSO.yPosition = yPosition;
        DataPersistenceManager.instance.SaveGame();
    }

    public void SetIsSceneUnlock(string sceneName, bool isUnlock)
    {
        if (sceneName == "StartMenuScene") scenesUnlockedSO.unlockStartMenuScene = isUnlock;
        if (sceneName == "Chapter1Scene") scenesUnlockedSO.unlockChapter1Scene = isUnlock;
        if (sceneName == "Chapter2Scene") scenesUnlockedSO.unlockChapter2Scene = isUnlock;
        if (sceneName == "Chapter3Scene") scenesUnlockedSO.unlockChapter3Scene = isUnlock;
        if (sceneName == "Chapter4Scene") scenesUnlockedSO.unlockChapter4Scene = isUnlock;
        if (sceneName == "Chapter5Scene") scenesUnlockedSO.unlockChapter5Scene = isUnlock;
        if (sceneName == "Chapter6Scene") scenesUnlockedSO.unlockChapter6Scene = isUnlock;
    }

    public bool IsSceneUnlock(string sceneName)
    { //Called in LoadScenePortal.OnTrigger()
        bool isSceneUnlock = false;

        if ((sceneName == "StartMenuScene" && scenesUnlockedSO.unlockStartMenuScene)
        || (sceneName == "Chapter1Scene" && scenesUnlockedSO.unlockChapter1Scene)
        || (sceneName == "Chapter2Scene" && scenesUnlockedSO.unlockChapter2Scene)
        || (sceneName == "Chapter3Scene" && scenesUnlockedSO.unlockChapter3Scene)
        || (sceneName == "Chapter4Scene" && scenesUnlockedSO.unlockChapter4Scene)
        || (sceneName == "Chapter5Scene" && scenesUnlockedSO.unlockChapter5Scene)
        || (sceneName == "Chapter6Scene" && scenesUnlockedSO.unlockChapter6Scene))
        {
            isSceneUnlock = true;
        }
        else isSceneUnlock = false;
        return isSceneUnlock;
    }

    public void SetItemAdded(string itemName, bool isUnlock, Sprite sprite)
    {
        /*
        if (itemName == "Sports Shoes") 
        {
            inventorySO.addSportsShoes = isUnlock;
            inventorySO.spriteSportsShoes = sprite;
        }
        if (itemName == "Vitamin Capsule") 
        {
            inventorySO.addVitaminCapsule = isUnlock;
            inventorySO.spriteVitaminCapsule = sprite;
        }
        */

        if (itemName == "First Aid")
        {
            inventorySO.addFirstAid = isUnlock;
            inventorySO.spriteFirstAid = spriteFirstAid;
        }
        if (itemName == "Charisma Book")
        {
            inventorySO.addCharismaBook = isUnlock;
            inventorySO.spriteCharismaBook = spriteCharismaBook;
        }
        if (itemName == "ESD Shoes")
        {
            inventorySO.addESDShoes = isUnlock;
            inventorySO.spriteESDShoes = spriteESDShoes;
        }
        if (itemName == "Logic Book")
        {
            inventorySO.addLogicBook = isUnlock;
            inventorySO.spriteLogicBook = spriteLogicBook;
        }
        if (itemName == "Umbrella")
        {
            inventorySO.addUmbrella = isUnlock;
            inventorySO.spriteUmbrella = spriteUmbrella;
        }
        if (itemName == "Fitness Book")
        {
            inventorySO.addFitnessBook = isUnlock;
            inventorySO.spriteFitnessBook = spriteFitnessBook;
        }
        if (itemName == "Energy Drink")
        {
            inventorySO.addEnergyDrink = isUnlock;
            inventorySO.spriteEnergyDrink = spriteEnergyDrink;
        }
        if (itemName == "Mental Health Book")
        {
            inventorySO.addMentalHealthBook = isUnlock;
            inventorySO.spriteMentalHealthBook = spriteMentalHealthBook;
        }
        if (itemName == "Coffee")
        {
            inventorySO.addCoffee = isUnlock;
            inventorySO.spriteCoffee = spriteCoffee;
        }
        if (itemName == "Creativity Book")
        {
            inventorySO.addCreativityBook = isUnlock;
            inventorySO.spriteCreativityBook = spriteCreativityBook;
        }
        if (itemName == "Jumping Shoes")
        {
            inventorySO.addJumpingShoes = isUnlock;
            inventorySO.spriteJumpingShoes = spriteJumpingShoes;
        }
    }

    public bool IsItemAdded(string itemName)
    {
        bool isItemAdded = false;
        /*
        if ((itemName == "Sports Shoes" && inventorySO.addSportsShoes)
        || (itemName == "Vitamin Capsule" && inventorySO.addVitaminCapsule)
        */

        if (itemName == "First Aid" && inventorySO.addFirstAid) isItemAdded = true;
        else if (itemName == "Charisma Book" && inventorySO.addCharismaBook) isItemAdded = true;
        else if (itemName == "ESD Shoes" && inventorySO.addESDShoes) isItemAdded = true;
        else if (itemName == "Logic Book" && inventorySO.addLogicBook) isItemAdded = true;
        else if (itemName == "Umbrella" && inventorySO.addUmbrella) isItemAdded = true;
        else if (itemName == "Fitness Book" && inventorySO.addFitnessBook) isItemAdded = true;
        else if (itemName == "Energy Drink" && inventorySO.addEnergyDrink) isItemAdded = true;
        else if (itemName == "Mental Health Book" && inventorySO.addMentalHealthBook) isItemAdded = true;
        else if (itemName == "Coffee" && inventorySO.addCoffee) isItemAdded = true;
        else if (itemName == "Creativity Book" && inventorySO.addCreativityBook) isItemAdded = true;
        else if (itemName == "Jumping Shoes" && inventorySO.addJumpingShoes) isItemAdded = true;
        else isItemAdded = false;
        return isItemAdded;
    }

    public void ResetItemsArray()
    {
        /*
        sportsShoes.itemSprite = inventorySO.spriteSportsShoes;
        vitaminCapsule.itemSprite = inventorySO.spriteVitaminCapsule;
        */

        /*
        firstAid.itemSprite = inventorySO.spriteFirstAid;
        charismaBook.itemSprite = inventorySO.spriteCharismaBook;
        esdShoes.itemSprite = inventorySO.spriteESDShoes;
        logicBook.itemSprite = inventorySO.spriteLogicBook;
        umbrella.itemSprite = inventorySO.spriteUmbrella;
        fitnessBook.itemSprite = inventorySO.spriteFitnessBook;
        energyDrink.itemSprite = inventorySO.spriteEnergyDrink;
        mentalHealthBook.itemSprite = inventorySO.spriteMentalHealthBook;
        coffee.itemSprite = inventorySO.spriteCoffee;
        creativityBook.itemSprite = inventorySO.spriteCreativityBook;
        jumpingShoes.itemSprite = inventorySO.spriteJumpingShoes;
        */

        firstAid.itemSprite = spriteFirstAid;
        charismaBook.itemSprite = spriteCharismaBook;
        esdShoes.itemSprite = spriteESDShoes;
        logicBook.itemSprite = spriteLogicBook;
        umbrella.itemSprite = spriteUmbrella;
        fitnessBook.itemSprite = spriteFitnessBook;
        energyDrink.itemSprite = spriteEnergyDrink;
        mentalHealthBook.itemSprite = spriteMentalHealthBook;
        coffee.itemSprite = spriteCoffee;
        creativityBook.itemSprite = spriteCreativityBook;
        jumpingShoes.itemSprite = spriteJumpingShoes;
        itemsArray = new Item[] { firstAid, charismaBook, esdShoes, logicBook, umbrella, fitnessBook, energyDrink, mentalHealthBook, coffee, creativityBook, jumpingShoes };
    }

    public void LoadData(GameData gameData)
    {
        scenesUnlockedSO.sceneName = gameData.scenesUnlockedData.sceneName;
        playerStatsSO.xPosition = gameData.playerStatsData.xPosition;
        playerStatsSO.yPosition = gameData.playerStatsData.yPosition;

        //playerStatsSO
        playerStatsSO.gender = gameData.playerStatsData.gender;
        playerStatsSO.runtimeAnimatorController = gameData.playerStatsData.runtimeAnimatorController;
        playerStatsSO.maxEnergy = gameData.playerStatsData.maxEnergy;
        playerStatsSO.energy = gameData.playerStatsData.energy;
        playerStatsSO.cash = gameData.playerStatsData.cash;
        playerStatsSO.fitness = gameData.playerStatsData.fitness;
        playerStatsSO.logic = gameData.playerStatsData.logic;
        playerStatsSO.creativity = gameData.playerStatsData.creativity;
        playerStatsSO.charisma = gameData.playerStatsData.charisma;
        playerStatsSO.mentalHealth = gameData.playerStatsData.mentalHealth;
        playerStatsSO.equippedItemName = gameData.playerStatsData.equippedItemName;
        playerStatsSO.tempStatPoint = gameData.playerStatsData.tempStatPoint;
        playerStatsSO.gamepadCount = gameData.playerStatsData.gamepadCount;

        //scenesUnlockedSO
        scenesUnlockedSO.unlockStartMenuScene = gameData.scenesUnlockedData.unlockStartMenuScene;
        scenesUnlockedSO.unlockChapter1Scene = gameData.scenesUnlockedData.unlockChapter1Scene;
        scenesUnlockedSO.unlockChapter2Scene = gameData.scenesUnlockedData.unlockChapter2Scene;
        scenesUnlockedSO.unlockChapter3Scene = gameData.scenesUnlockedData.unlockChapter3Scene;
        scenesUnlockedSO.unlockChapter4Scene = gameData.scenesUnlockedData.unlockChapter4Scene;
        scenesUnlockedSO.unlockChapter5Scene = gameData.scenesUnlockedData.unlockChapter5Scene;
        scenesUnlockedSO.unlockChapter6Scene = gameData.scenesUnlockedData.unlockChapter6Scene;
        scenesUnlockedSO.unlockChapterMenuScene = gameData.scenesUnlockedData.unlockChapterMenuScene;
        scenesUnlockedSO.unlockPlayerMenuScene = gameData.scenesUnlockedData.unlockPlayerMenuScene;

        //inventorySO bool
        inventorySO.isItemUsed = gameData.inventoryData.isItemUsed;
        /*
        inventorySO.addSportsShoes = gameData.inventoryData.addSportsShoes;
        inventorySO.addVitaminCapsule = gameData.inventoryData.addVitaminCapsule;
        */
        inventorySO.addFirstAid = gameData.inventoryData.addFirstAid;
        inventorySO.addCharismaBook = gameData.inventoryData.addCharismaBook;
        inventorySO.addESDShoes = gameData.inventoryData.addESDShoesBook;
        inventorySO.addLogicBook = gameData.inventoryData.addLogicBook;
        inventorySO.addUmbrella = gameData.inventoryData.addUmbrella;
        inventorySO.addFitnessBook = gameData.inventoryData.addFitnessBook;
        inventorySO.addEnergyDrink = gameData.inventoryData.addEnergyDrink;
        inventorySO.addMentalHealthBook = gameData.inventoryData.addMentalHealthBook;
        inventorySO.addCoffee = gameData.inventoryData.addCoffee;
        inventorySO.addCreativityBook = gameData.inventoryData.addCreativityBook;
        inventorySO.addJumpingShoes = gameData.inventoryData.addJumpingShoes;

        //inventorySO sprite
        /*
        inventorySO.spriteSportsShoes = gameData.inventoryData.spriteSportsShoes;
        inventorySO.spriteVitaminCapsule = gameData.inventoryData.spriteVitaminCapsule;
        */
        inventorySO.spriteFirstAid = gameData.inventoryData.spriteFirstAid;
        inventorySO.spriteCharismaBook = gameData.inventoryData.spriteCharismaBook;
        inventorySO.spriteESDShoes = gameData.inventoryData.spriteESDShoesBook;
        inventorySO.spriteLogicBook = gameData.inventoryData.spriteLogicBook;
        inventorySO.spriteUmbrella = gameData.inventoryData.spriteUmbrella;
        inventorySO.spriteFitnessBook = gameData.inventoryData.spriteFitnessBook;
        inventorySO.spriteEnergyDrink = gameData.inventoryData.spriteEnergyDrink;
        inventorySO.spriteMentalHealthBook = gameData.inventoryData.spriteMentalHealthBook;
        inventorySO.spriteCoffee = gameData.inventoryData.spriteCoffee;
        inventorySO.spriteCreativityBook = gameData.inventoryData.spriteCreativityBook;
        inventorySO.spriteJumpingShoes = gameData.inventoryData.spriteJumpingShoes;

        //Dialogues & Collectibles
        saveVariablesKey = gameData.saveVariablesKey;
        keysCollected = gameData.keysCollected;
    }

    public void SaveData(GameData gameData)
    {
        gameData.scenesUnlockedData.sceneName = scenesUnlockedSO.sceneName;
        gameData.playerStatsData.xPosition = playerStatsSO.xPosition;
        gameData.playerStatsData.yPosition = playerStatsSO.yPosition;

        //playerStatsSO
        gameData.playerStatsData.gender = playerStatsSO.gender;
        gameData.playerStatsData.runtimeAnimatorController = playerStatsSO.runtimeAnimatorController;
        gameData.playerStatsData.maxEnergy = playerStatsSO.maxEnergy;
        gameData.playerStatsData.energy = playerStatsSO.energy;
        gameData.playerStatsData.cash = playerStatsSO.cash;
        gameData.playerStatsData.fitness = playerStatsSO.fitness;
        gameData.playerStatsData.logic = playerStatsSO.logic;
        gameData.playerStatsData.creativity = playerStatsSO.creativity;
        gameData.playerStatsData.charisma = playerStatsSO.charisma;
        gameData.playerStatsData.mentalHealth = playerStatsSO.mentalHealth;
        gameData.playerStatsData.equippedItemName = playerStatsSO.equippedItemName;
        gameData.playerStatsData.tempStatPoint = playerStatsSO.tempStatPoint;
        gameData.playerStatsData.gamepadCount = playerStatsSO.gamepadCount;

        //scenesUnlockedSO
        gameData.scenesUnlockedData.unlockStartMenuScene = scenesUnlockedSO.unlockStartMenuScene;
        gameData.scenesUnlockedData.unlockChapter1Scene = scenesUnlockedSO.unlockChapter1Scene;
        gameData.scenesUnlockedData.unlockChapter2Scene = scenesUnlockedSO.unlockChapter2Scene;
        gameData.scenesUnlockedData.unlockChapter3Scene = scenesUnlockedSO.unlockChapter3Scene;
        gameData.scenesUnlockedData.unlockChapter4Scene = scenesUnlockedSO.unlockChapter4Scene;
        gameData.scenesUnlockedData.unlockChapter5Scene = scenesUnlockedSO.unlockChapter5Scene;
        gameData.scenesUnlockedData.unlockChapter6Scene = scenesUnlockedSO.unlockChapter6Scene;
        gameData.scenesUnlockedData.unlockChapterMenuScene = scenesUnlockedSO.unlockChapterMenuScene;
        gameData.scenesUnlockedData.unlockPlayerMenuScene = scenesUnlockedSO.unlockPlayerMenuScene;

        //inventorySO bool
        gameData.inventoryData.isItemUsed = inventorySO.isItemUsed;
        /*
        gameData.inventoryData.addSportsShoes = inventorySO.addSportsShoes;
        gameData.inventoryData.addVitaminCapsule = inventorySO.addVitaminCapsule;
        */
        gameData.inventoryData.addFirstAid = inventorySO.addFirstAid;
        gameData.inventoryData.addCharismaBook = inventorySO.addCharismaBook;
        gameData.inventoryData.addESDShoesBook = inventorySO.addESDShoes;
        gameData.inventoryData.addLogicBook = inventorySO.addLogicBook;
        gameData.inventoryData.addUmbrella = inventorySO.addUmbrella;
        gameData.inventoryData.addFitnessBook = inventorySO.addFitnessBook;
        gameData.inventoryData.addEnergyDrink = inventorySO.addEnergyDrink;
        gameData.inventoryData.addMentalHealthBook = inventorySO.addMentalHealthBook;
        gameData.inventoryData.addCoffee = inventorySO.addCoffee;
        gameData.inventoryData.addCreativityBook = inventorySO.addCreativityBook;
        gameData.inventoryData.addJumpingShoes = inventorySO.addJumpingShoes;

        //inventorySO sprite
        /*
        gameData.inventoryData.spriteSportsShoes = inventorySO.spriteSportsShoes;
        gameData.inventoryData.spriteVitaminCapsule = inventorySO.spriteVitaminCapsule;
        */
        gameData.inventoryData.spriteFirstAid = inventorySO.spriteFirstAid;
        gameData.inventoryData.spriteCharismaBook = inventorySO.spriteCharismaBook;
        gameData.inventoryData.spriteESDShoesBook = inventorySO.spriteESDShoes;
        gameData.inventoryData.spriteLogicBook = inventorySO.spriteLogicBook;
        gameData.inventoryData.spriteUmbrella = inventorySO.spriteUmbrella;
        gameData.inventoryData.spriteFitnessBook = inventorySO.spriteFitnessBook;
        gameData.inventoryData.spriteEnergyDrink = inventorySO.spriteEnergyDrink;
        gameData.inventoryData.spriteMentalHealthBook = inventorySO.spriteMentalHealthBook;
        gameData.inventoryData.spriteCoffee = inventorySO.spriteCoffee;
        gameData.inventoryData.spriteCreativityBook = inventorySO.spriteCreativityBook;
        gameData.inventoryData.spriteJumpingShoes = inventorySO.spriteJumpingShoes;

        //Dialogues & Collectibles
        if (DialogueManager.instance != null)
        {   //globalVariablesStory.state.ToJson() + "_" + DataPersistenceManager.instance.selectedProfileId means saving it to a Json file along with what profileId is used
            saveVariablesKey = DialogueManager.instance.dialogueVariablesManager.globalVariablesStory.state.ToJson() + "_" + DataPersistenceManager.instance.selectedProfileId;
            gameData.saveVariablesKey = saveVariablesKey;
        }

        if (isReplayingChapter)
        {
            gameData.cashCollected = new SerializableDictionary<string, bool>();
            gameData.keysCollected = new SerializableDictionary<string, bool>();
            isReplayingChapter = false;
        }
        else
        {
            gameData.keysCollected = keysCollected;
        }

        //Stats
    }

    public void MentalHealthStatCheck(bool isAdding)
    {
        if (playerStatsSO.mentalHealth <= 12 && isAdding) //If mentalHealth
        {
            prevFitness = GameManager.instance.playerStatsSO.fitness;
            GameManager.instance.playerStatsSO.fitness = GameManager.instance.playerStatsSO.mentalHealth;
            GameManager.instance.playerStatsSO.fitness += prevFitness;

            prevLogic = GameManager.instance.playerStatsSO.logic;
            GameManager.instance.playerStatsSO.logic = GameManager.instance.playerStatsSO.mentalHealth;
            GameManager.instance.playerStatsSO.logic += prevLogic;

            prevCreativity = GameManager.instance.playerStatsSO.creativity;
            GameManager.instance.playerStatsSO.creativity = GameManager.instance.playerStatsSO.mentalHealth;
            GameManager.instance.playerStatsSO.creativity += prevCreativity;

            prevCharisma = GameManager.instance.playerStatsSO.charisma;
            GameManager.instance.playerStatsSO.charisma = GameManager.instance.playerStatsSO.mentalHealth;
            GameManager.instance.playerStatsSO.charisma += prevCharisma;
        }
        else if(!isAdding)
        {
            GameManager.instance.playerStatsSO.fitness -= 12;
            GameManager.instance.playerStatsSO.logic -= 12;
            GameManager.instance.playerStatsSO.creativity -= 12;
            GameManager.instance.playerStatsSO.charisma -= 12;
            //GameManager.instance.playerStatsSO.mentalHealth -= 10;
        }
        else if (GameManager.instance.playerStatsSO.mentalHealth <= 0)
        {
            Debug.Log("Mental Health is too low it can't be reduced further, also the player will continuously die in Fitness");
        }

    }

}
