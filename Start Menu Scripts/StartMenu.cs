using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartMenu : Menu
{
    [Header("Menu Navigation")]
    [SerializeField] private SaveSlotsMenu saveSlotsMenu;

    [Header("StartMenu Buttons")]
    [SerializeField] private Button newGameButton;
    [SerializeField] private Button loadGameButton;

    [Header("Audio Sources")]
    public AudioClip bgm;
    [SerializeField] private AudioClip clickAudioClip;

    void Awake()
    {   
        AudioManager.instance.PlayBGM(bgm);
    }

    void Start()
    {   
        //If the game doesn't have any save data 
        if (!DataPersistenceManager.instance.HasGameData())
        {
            loadGameButton.interactable = false;
        }

        if(GameManager.instance.playerStatsSO.gamepadCount == 0) //Player is using Keyboard & Mouse
        {  
            SetFirstSelectedButton(null);
        }
        else if(GameManager.instance.playerStatsSO.gamepadCount == 1) //Player is using Gamepad
        {   
            SetFirstSelectedButton(newGameButton);
            Debug.Log("Start StartMenu");
        }
    }

    public void OnNewGameClicked()
    {   
        AudioManager.instance.PlaySFX(clickAudioClip);
        saveSlotsMenu.ActivateMenu(false); //False means the game will not load a saved data
        this.DeactivateMenu();
    }

    public void OnLoadGameClicked()
    {   
        AudioManager.instance.PlaySFX(clickAudioClip);
        saveSlotsMenu.ActivateMenu(true); //True means the game will load a saved data
        this.DeactivateMenu();
    }

    public void OnOptionsClicked()
    {
        
    }

    public void OnQuitGameClicked()
    {   
        DataPersistenceManager.instance.useEncryption = true;
        DataPersistenceManager.instance.SaveGame();
        Application.Quit();
    }

    public void ActivateMenu()
    {   
        this.gameObject.SetActive(true);
    }

    public void DeactivateMenu()
    {
        this.gameObject.SetActive(false);
    }

}