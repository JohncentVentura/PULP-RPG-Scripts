using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SaveSlot : MonoBehaviour
{
    [Header("ProfileID")] //Each saveslot has a ProfileId associated with it
    [SerializeField] private string profileId = "";

    [Header("SaveSlot UI")]
    [SerializeField] private GameObject noDataTexts;
    [SerializeField] private GameObject hasDataTexts;
    [SerializeField] private TextMeshProUGUI completeText;
    [SerializeField] private TextMeshProUGUI coinText;

    private Button saveSlotButton;
    public bool hasData { get; private set; } = false; //If this SaveSlot has data or not

    [Header("SaveSlot UI")]
    private Image image;
    [SerializeField] private Sprite chapter1Sprite;
    [SerializeField] private Sprite chapter2Sprite;
    [SerializeField] private Sprite chapter3Sprite;
    [SerializeField] private Sprite chapter4Sprite;
    [SerializeField] private Sprite chapter5Sprite;
    [SerializeField] private Sprite chapter6Sprite;

    private void Awake()
    {   
        image = GetComponent<Image>();
        saveSlotButton = this.GetComponent<Button>();
    }

    public void SetData(GameData gameData)
    {
        if (gameData == null) //If there is no GameData on this SaveSlot
        {
            hasData = false;
            noDataTexts.SetActive(true);
            hasDataTexts.SetActive(false);
        }
        else //If there is a GameData on this SaveSlot
        {
            hasData = true;
            noDataTexts.SetActive(false);
            hasDataTexts.SetActive(true);

            //Progress
            DataPersistenceManager.instance.ChangeSelectedProfileId(profileId);
            completeText.text = GetChapterDetails();
            coinText.text = GameManager.instance.playerStatsSO.cash.ToString();
        }
    }

    public string GetProfileId() //Returns a profileId that corresponds to this SaveSlot
    {
        return this.profileId;
    }

    public void SetInteractable(bool interactable)
    {
        saveSlotButton.interactable = interactable;
    }

    private string GetChapterDetails()
    {
        string chapterName = "";

        if(GameManager.instance.scenesUnlockedSO.sceneName == "Chapter1Scene" && GameManager.instance.scenesUnlockedSO.unlockChapter1Scene)
        {
            chapterName = "Chapter 1 : Highway";
            image.sprite = chapter1Sprite;
        }
        else if(GameManager.instance.scenesUnlockedSO.sceneName == "Chapter2Scene" && GameManager.instance.scenesUnlockedSO.unlockChapter2Scene)
        {
            chapterName = "Chapter 2 : Laboratory";
            image.sprite = chapter2Sprite;
        }
        else if(GameManager.instance.scenesUnlockedSO.sceneName == "Chapter3Scene" && GameManager.instance.scenesUnlockedSO.unlockChapter3Scene)
        {
            chapterName = "Chapter 3 : Hallway";
            image.sprite = chapter3Sprite;
        }
        else if(GameManager.instance.scenesUnlockedSO.sceneName == "Chapter4Scene" && GameManager.instance.scenesUnlockedSO.unlockChapter4Scene)
        {
            chapterName = "Chapter 4 : Town";
            image.sprite = chapter4Sprite;
        }
        else if(GameManager.instance.scenesUnlockedSO.sceneName == "Chapter5Scene" && GameManager.instance.scenesUnlockedSO.unlockChapter5Scene)
        {
            chapterName = "Chapter 5 : Library";
            image.sprite = chapter5Sprite;
        }
        else if(GameManager.instance.scenesUnlockedSO.sceneName == "Chapter6Scene" && GameManager.instance.scenesUnlockedSO.unlockChapter6Scene)
        {
            chapterName = "Chapter 6 : Capstone Defense";
            image.sprite = chapter6Sprite;
        }
        
        return chapterName;
    }
}
