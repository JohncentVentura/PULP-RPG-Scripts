using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using TMPro;

public class ConfirmMenu : Menu
{
    [Header("ConfirmNewGameMenu UI")]
    [SerializeField] private TextMeshProUGUI confirmText;
    [SerializeField] private Button confirmButton;
    [SerializeField] private Button cancelButton;

    [SerializeField] private AudioClip clickAudioClip;

    private void Start()
    {
        SetFirstSelectedButton(cancelButton);
        Debug.Log("Start ConfirmMenu");
    }

    //UnityActions are block of codes for when the confirm or cancel button is clicked
    public void ActivateMenu(string displayText, UnityAction confirmAction, UnityAction cancelAction)
    {
        this.gameObject.SetActive(true);

        this.confirmText.text = displayText;

        //Remove all Listeners because we will add our own OnClick Listener
        confirmButton.onClick.RemoveAllListeners();
        cancelButton.onClick.RemoveAllListeners();

        //Add the OnClick Listeners
        confirmButton.onClick.AddListener(() => 
        {   
            AudioManager.instance.PlaySFX(clickAudioClip);
            DeactivateMenu(); //Deactivates this menu after pressing this OnClick()
            confirmAction(); //Calls confirmAction
        });
        cancelButton.onClick.AddListener(() => 
        {   
            AudioManager.instance.PlaySFX(clickAudioClip);
            DeactivateMenu(); //Deactivates this menu after pressing this OnClick()
            cancelAction(); //Calls cancelAction
        });
    }

    public void DeactivateMenu()
    {
        this.gameObject.SetActive(false);
    }
}
