using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DataPersistenceManager : MonoBehaviour
{
    [Header("DataFileHandler Settings")]
    public bool useEncryption; //Enable encrypting & decrypting the game data when saving it

    public static DataPersistenceManager instance { get; private set; } //DataPersistenceManager is a singleton
    public GameData gameData; //Contains all scripts that are Serializable (data that can be saved)
    private DataFileHandler dataFileHandler;
    private List<IDataPersistence> dataPersistenceObjects; //Contains all scripts extended to MonoBehaviour & IDataPersistence,

    public string selectedProfileId = ""; //The profile or folder containing a saved data file & its backup saved data file
    private string dataFileName = "data.game"; //The name of the file containing a saved data

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(this.gameObject); //If there is another instance of this class, destroy it because this class is a singleton
            return;
        }
        instance = this; //If there are no instance of this class, this becomes the first & only instance of this class
        DontDestroyOnLoad(this.gameObject); //The gameObject must be in the root of a scene, to persist when loading to other scenes

        //Application.persistentDataPath gives the OS directory for persisting data in a Unity project, for Windows OS directory is down below
        this.dataFileHandler = new DataFileHandler(Application.persistentDataPath, dataFileName, useEncryption);

        //The selectedProfileId will be the last profileId that was loaded, this keeps a single profileId to be persisted every Scene Transition
        this.selectedProfileId = dataFileHandler.GetMostRecentlyUpdatedProfileId();
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded; //Subscribes here on OnEnable
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded; //Unsubscribes here on OnDisable 
    }

    public void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        //Find all objects extended to MonoBehaviour & IDataPersistence, using System.Linq will grant access for using .OfType<>();
        IEnumerable<IDataPersistence> dataPersistenceObjects = FindObjectsOfType<MonoBehaviour>(true).OfType<IDataPersistence>(); //true means we include even inactive objects

        //dataPersistenceObjects will contain a new List everytime we load a scene, the List contains all objects extended to MonoBehaviour & IDataPersistence
        this.dataPersistenceObjects = new List<IDataPersistence>(dataPersistenceObjects);

        //Loads the game at start up, creates a NewGame if no game was loaded
        LoadGame();
    }

    public void NewGame() //Can be use in UI Buttons
    {
        gameData = new GameData();
        gameData.NewPlayerStatsData();
        gameData.NewScenesUnlockedData();
        gameData.NewInventoryData();
        gameData.NewSaveVariablesKey();
    }

    public void LoadGame() //Happens after selecting a profileID, and when a new scene is OnEnabled
    {
        this.gameData = dataFileHandler.LoadDataFile(selectedProfileId); //Loads the GameData from the selectedProfileId

        if (this.gameData == null) //If gameData is null, return immediately
        {   
            Debug.Log("No data was found, a new game needs to be started before data can be loaded");
            return;
        }

        //Iterate dataPersistenceObject to call its LoadData & also passing in the GameData
        foreach (IDataPersistence dataPersistenceObject in dataPersistenceObjects)
        {
            dataPersistenceObject.LoadData(gameData);
        }
    }

    public void SaveGame() //Happens before quitting the game, and before scene transitioning 
    {
        if (this.gameData == null) //If gameData is null, return immediately
        {
            Debug.Log("No data was found, a new game needs to be started before data can be saved");
            return;
        }

        //Iterate dataPersistenceObject to call its SaveData & also getting the GameData
        foreach (IDataPersistence dataPersistenceObject in dataPersistenceObjects)
        {
            dataPersistenceObject.SaveData(gameData);
        }

        dataFileHandler.SaveDataFile(gameData, selectedProfileId); //Saves the GameData from the selectedProfileId
    }

    private void OnApplicationQuit()
    {   
        GameManager.instance.playerStatsSO.gamepadCount = 0;
        DataPersistenceManager.instance.useEncryption = true;
        SaveGame();
    }

    // Called in other Start Menu Scripts
    public void ChangeSelectedProfileId(string newProfileId) //Called in SaveSlotsMenu
    {
        this.selectedProfileId = newProfileId; //Sets the selectedProfileId into the profileId selected from a SaveSlot
        LoadGame(); //Loads the game using the selectedProfileId which updates the GameData
    }

    public Dictionary<string, GameData> GetAllProfilesGameData()
    {
        return dataFileHandler.LoadAllProfiles();
    }

    public bool HasGameData()
    {
        return gameData != null;
    }
}