using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlatformerHUD : MonoBehaviour, IDataPersistence
{
    [SerializeField] private PlayerControls playerControls;
    [SerializeField] private LoadMenu loadMenu;

    [Header("UI")]
    [SerializeField] private GameObject platformerHUDPanel;
    /*
    [SerializeField] private TextMeshProUGUI coinText;
    [SerializeField] private TextMeshProUGUI energyText;
    [SerializeField] private TextMeshProUGUI dashCDText;
    */

    [SerializeField] private Slider cashSlider;
    [SerializeField] private TextMeshProUGUI cashValueText;
    [SerializeField] private Slider energySlider;
    [SerializeField] private TextMeshProUGUI energyValueText;
    [SerializeField] private Slider dashSlider;
    [SerializeField] private TextMeshProUGUI dashValueText;
    [SerializeField] private Slider itemSlider;
    [SerializeField] private TextMeshProUGUI itemValueText;
    [SerializeField] private TextMeshProUGUI cashText;
    [SerializeField] private TextMeshProUGUI keyItemText;
    [SerializeField] private ConfirmMenu confirmMenu;

    //private List<Item> keyItems;

    [SerializeField] private AudioClip audioClip;

    private void Start()
    {   
        confirmMenu.DeactivateMenu();
        //keyItems = new List<Item>();
        //DeactivateMenu();
    }

    void Update()
    {
        if (platformerHUDPanel.activeSelf == true)
        {
            cashSlider.value = GameManager.instance.playerStatsSO.cash;
            cashSlider.maxValue = 9999;
            cashValueText.text = GameManager.instance.playerStatsSO.cash.ToString();

            energySlider.value = GameManager.instance.playerStatsSO.energy;
            energySlider.maxValue = GameManager.instance.playerStatsSO.maxEnergy;
            energyValueText.text = "" + GameManager.instance.playerStatsSO.energy + "/" + GameManager.instance.playerStatsSO.maxEnergy;

            dashSlider.value = playerControls.dashFill;
            dashValueText.text = playerControls.dashCooldown.ToString("F1");
            //dashValueText.text = ""+(int)playerControls.dashCooldown;

            if (playerControls.equippedItem != null)
            {
                itemSlider.gameObject.SetActive(true);
                itemSlider.maxValue = playerControls.equippedItem.itemMaxFill;
                itemSlider.value = playerControls.equippedItem.itemFill;
                itemValueText.text = playerControls.equippedItem.itemFill.ToString("F1");
            }
            else
            {
                itemSlider.gameObject.SetActive(false);
            }

            //cashText.text = "" + GameManager.instance.playerStatsSO.cash;
            //keyItemText.text = "";
        }

        //Escape
        if(Input.GetButtonDown("Cancel") && platformerHUDPanel.activeSelf == true)
        {   
            confirmMenu.ActivateMenu //Activates confirmNewGameMenu & setting up its Listeners
            (   
                "Return back to the main menu? You'll continue to the previous save point you encountered.",
                () => //confirmButton.onClick method
                {   
                    AudioManager.instance.bgmSource.volume = 0;
                    AudioManager.instance.PlayBGM(audioClip);
                    DataPersistenceManager.instance.useEncryption = true;
                    loadMenu.SceneTransition("ChapterMenuScene", GameManager.instance.scenesUnlockedSO.sceneName, 
                        GameManager.instance.playerStatsSO.xPosition, GameManager.instance.playerStatsSO.yPosition);
                },
                () => //cancelButton.onClick method
                {   
                    playerControls.isInputActive = true;
                    confirmMenu.DeactivateMenu();
                }
            );
        }

        if(confirmMenu.isActiveAndEnabled)
        {
            playerControls.isInputActive = false;
        }
    }

    public void ActivateMenu()
    {
        platformerHUDPanel.SetActive(true);
        energySlider.maxValue = GameManager.instance.playerStatsSO.maxEnergy;

        cashSlider.value = GameManager.instance.playerStatsSO.cash;
        cashValueText.text = GameManager.instance.playerStatsSO.cash.ToString();

        energySlider.value = GameManager.instance.playerStatsSO.energy;
        energyValueText.text = "" + GameManager.instance.playerStatsSO.energy + "/" + GameManager.instance.playerStatsSO.maxEnergy;

        dashSlider.maxValue = 4.5f;
        dashSlider.value = playerControls.dashFill;
        dashValueText.text = "" + (int)playerControls.dashCooldown;

        if (playerControls.equippedItem != null)
        {
            itemSlider.gameObject.SetActive(true);
            itemSlider.maxValue = playerControls.equippedItem.itemMaxFill;
            itemSlider.value = playerControls.equippedItem.itemFill;
            itemValueText.text = playerControls.equippedItem.itemFill.ToString();
        }
        else
        {
            itemSlider.gameObject.SetActive(false);
        }

        //cashText.text = "" + GameManager.instance.playerStatsSO.cash;
        //keyItemText.text = "";
    }

    public void DeactivateMenu()
    {
        platformerHUDPanel.SetActive(false);
    }

    public void LoadData(GameData gameData) //Loads the GameData to know what to show in the UI
    {
        /*
        foreach(KeyValuePair<string, bool> pair in GameManager.instance.coinsCollected)
        {
            if(pair.Value) //If pair or the coin Value is true, increment the coins
            {
                coin++;
            }
        }
        */
    }

    public void SaveData(GameData data)
    {

    }

    public void OnBackButtonClick()
    {   
        Debug.Log("OnBackButtonClick");
        //loadMenu.SceneTransition("ChapterMenuScene", GameManager.instance.scenesUnlockedSO.sceneName, GameManager.instance.playerStatsSO.xPosition, GameManager.instance.playerStatsSO.yPosition);
    }


}