using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SaveSlotsMenu : Menu
{
    [Header("Menu Navigation")]
    [SerializeField] private StartMenu startMenu;
    [SerializeField] private ChapterMenu chapterMenu;
    [SerializeField] private ConfirmMenu confirmMenu;
    [SerializeField] private LoadMenu loadMenu;

    [Header("SaveSlotsMenu Buttons")]
    [SerializeField] private Button backButton;

    [Header("New Game First Scene")]
    [SerializeField] private string firstSceneName; //The first scene that will be called when starting a new game

    private SaveSlot[] saveSlots; //Array to contain the save slots
    private bool isLoadingGame = false; //If the game is loading a data or not, sets to true when ActivateMenu is called from StartMenu

    [SerializeField] private AudioClip clickAudioClip;

    private void Start() 
    {
        SetFirstSelectedButton(firstSelected);
        Debug.Log("Start SaveSlotsMenu");
    }

    private void Awake()
    {
        //Initialize this variable will all of the child save slot under the GameObject this script is attached to
        //Gets all SaveSlot children of the GameObject this script is attached to
        saveSlots = this.GetComponentsInChildren<SaveSlot>();
    }

    public void OnSaveSlotClick(SaveSlot saveSlot) //Use for OnClick of SaveSlot, use the same SaveSlot button as the SaveSlot on its OnClick
    {   
        AudioManager.instance.PlaySFX(clickAudioClip);
        DisableMenuButtons(); //Disables buttons since the game will now load

        if (isLoadingGame) //Loads a saved game
        {
            DataPersistenceManager.instance.ChangeSelectedProfileId(saveSlot.GetProfileId());
            ActivateChapterMenu();
        }
        else if (saveSlot.hasData) //Creates a new game, but there is a saved game
        {
            confirmMenu.ActivateMenu //Activates confirmNewGameMenu & setting up its Listeners
            (
                "Starting a new game will override the currently saved data. Are you sure?",
                () => //confirmButton.onClick method
                {
                    DataPersistenceManager.instance.ChangeSelectedProfileId(saveSlot.GetProfileId()); //Use the profileId corresponding to this SaveSlot
                    DataPersistenceManager.instance.NewGame(); //Creates a new game on the SaveSlot, overriding the saved game
                    ActivateGenderMenu();
                },
                () => //cancelButton.onClick method
                {
                    this.ActivateMenu(isLoadingGame); //Refreshes the menu that selecting loading
                }
            );
        }
        else //Creates a new game on a SaveSlot
        {   
            DataPersistenceManager.instance.ChangeSelectedProfileId(saveSlot.GetProfileId());
            DataPersistenceManager.instance.NewGame();
            ActivateGenderMenu();
        }
    }

    private void ActivateChapterMenu() //When continuing a saved game
    {   
        loadMenu.SceneTransition(
            "ChapterMenuScene",
            GameManager.instance.scenesUnlockedSO.sceneName,
            GameManager.instance.playerStatsSO.xPosition,
            GameManager.instance.playerStatsSO.yPosition
        );
    }

    private void ActivateGenderMenu() //When starting a new game
    {   
        loadMenu.SceneTransition(
            "GenderMenuScene",
            GameManager.instance.scenesArray[1],
            GameManager.instance.playerStatsSO.xPosition,
            GameManager.instance.playerStatsSO.yPosition
        );
    }

    public void ActivateMenu(bool isLoadingGame)
    {
        this.gameObject.SetActive(true);

        //If isLoadingGame is true then we're opening this menu after pressing the LoadGameButton
        //Else if isLoadingGame is false then we're opening this menu after pressing the NewGameButton
        this.isLoadingGame = isLoadingGame;

        //Load all profileId that are currently exist
        Dictionary<string, GameData> profilesGameData = DataPersistenceManager.instance.GetAllProfilesGameData();

        //Ensure to make backButton interactable again since we disable it after calling OnSaveSlotClick
        backButton.interactable = true;
        GameObject firstSelected = backButton.gameObject;

        //Iterate each SaveSlot to initialize its UI, to set active either NoDataUI Object or HasDataUI Object in the Unity Editor
        foreach (SaveSlot saveSlot in saveSlots)
        {
            GameData profileData = null; //Placeholder for each GameData of saveSlot

            //Getting a GameData from profilesGameData where the key is the same in saveSlot.GetProfileId() and store it to profileData
            profilesGameData.TryGetValue(saveSlot.GetProfileId(), out profileData);

            //The SaveSlot will set the profileData
            saveSlot.SetData(profileData);

            //If we are loading a GameData
            if (profileData == null && isLoadingGame)
            {
                saveSlot.SetInteractable(false);
            }
            else //Else If we are not loading a GameData
            {
                saveSlot.SetInteractable(true);

                //Makes backButton the first selected button if there are no save data to be loaded
                if (firstSelected.Equals(backButton.gameObject))
                {
                    firstSelected = saveSlot.gameObject;
                }
            }
        }

        //Sets the first selected button
        Button firstSelectedButton = firstSelected.GetComponent<Button>();
        this.SetFirstSelectedButton(firstSelectedButton);
    }

    public void DeactivateMenu()
    {
        this.gameObject.SetActive(false);
    }

    public void OnBackClick()
    {   
        AudioManager.instance.PlaySFX(clickAudioClip);
        startMenu.ActivateMenu();
        this.DeactivateMenu();
    }

    private void DisableMenuButtons()
    {
        foreach (SaveSlot saveSlot in saveSlots)
        {
            saveSlot.SetInteractable(false);
        }
        
        backButton.interactable = false;
    }
}
