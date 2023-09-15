using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class GenderMenu : Menu
{
    [Header("Menu Navigation")]
    [SerializeField] private ConfirmMenu confirmMenu;
    [SerializeField] private LoadMenu loadMenu;

    [Header("Gender Menu UI")]
    [SerializeField] private GameObject genderMenuPanel;
    [SerializeField] private GameObject playerMaleImage;
    [SerializeField] private Animator genderMaleAnimator;
    [SerializeField] private GameObject playerFemaleImage;
    [SerializeField] private Animator genderFemaleAnimator;
    [SerializeField] private Button playerMaleButton;
    [SerializeField] private Button playerFemaleButton;

    [Header("Player RuntimeAnimatorController")]
    [SerializeField] private RuntimeAnimatorController playerMaleRuntimeAnimator;
    [SerializeField] private RuntimeAnimatorController playerFemaleRuntimeAnimator;

    [SerializeField] private AudioClip clickAudioClip;

    public string gender = "Male";

    private void Start()
    {
        SetPlayerAnimator(); //Called immediately in case of the player closing the game while in gender selection
        SetFirstSelectedButton(playerMaleButton);
        Debug.Log("Start GenderMenu");
    }

    public void OnPlayerClick(string gender)
    {   
        AudioManager.instance.PlaySFX(clickAudioClip);
        this.gender = gender;

        confirmMenu.ActivateMenu //Activates confirmNewGameMenu & setting up its Listeners
            (
                "So you're a " + gender + ", are you sure about that?",
                () => //confirmButton.onClick method
                {   
                    SetPlayerAnimator();
                    loadMenu.SceneTransition(
                        GameManager.instance.scenesUnlockedSO.sceneName,
                        GameManager.instance.scenesUnlockedSO.sceneName,
                        GameManager.instance.playerStatsSO.xPosition,
                        GameManager.instance.playerStatsSO.yPosition
                    );
                },
                () => //cancelButton.onClick method
                {
                    confirmMenu.DeactivateMenu();
                }
            );
    }

    public void SetPlayerAnimator()
    {
        if (gender == "Male")
        {   
            GameManager.instance.playerStatsSO.gender = "Male";
            GameManager.instance.playerStatsSO.runtimeAnimatorController = playerMaleRuntimeAnimator;
        }
        else if (gender == "Female")
        {   
            GameManager.instance.playerStatsSO.gender = "Female";
            GameManager.instance.playerStatsSO.runtimeAnimatorController = playerFemaleRuntimeAnimator;
        }
    }

}
