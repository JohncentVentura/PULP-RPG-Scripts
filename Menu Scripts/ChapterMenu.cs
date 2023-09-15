using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class ChapterMenu : Menu
{
    [Header("Menu Navigation")]
    [SerializeField] private ConfirmMenu confirmMenu;
    [SerializeField] private LoadMenu loadMenu;

    [Header("Chapter Menu UI")]
    [SerializeField] private Image chapterImage;
    [SerializeField] private TextMeshProUGUI chapterText;
    [SerializeField] private Button nextChapterButton;
    [SerializeField] private Button prevChapterButton;
    [SerializeField] private Button playButton;
    [SerializeField] private TextMeshProUGUI playButtonText;
    [SerializeField] private Button backButton;

    [Header("Sprites")]
    [SerializeField] private Sprite chapter1Sprite;
    [SerializeField] private Sprite chapter2Sprite;
    [SerializeField] private Sprite chapter3Sprite;
    [SerializeField] private Sprite chapter4Sprite;
    [SerializeField] private Sprite chapter5Sprite;
    [SerializeField] private Sprite chapter6Sprite;

    //private string[] scenesArray;
    //private string[] chaptersArray;

    private int sceneId = 1;
    private string sceneName;

    [SerializeField] private AudioClip clickAudioClip;

    private void Start()
    {
        SetFirstSelectedButton(playButton);
        Debug.Log("Start ChapterMenu");
    }

    private void Awake()
    {
        //scenesArray = GameManager.instance.scenesArray;
        //chaptersArray = GameManager.instance.chaptersArray;
        ActivateMenu();
    }

    public void OnNextChapterClick()
    {   
        AudioManager.instance.PlaySFX(clickAudioClip);

        sceneId++;

        try
        {
            if (!GameManager.instance.IsSceneUnlock(GameManager.instance.scenesArray[sceneId + 1]))
            {
                nextChapterButton.interactable = false; //If next element from the array is still locked
                if(GameManager.instance.playerStatsSO.gamepadCount == 1) playButton.Select();
            }
        }
        catch (Exception e)
        {
            Debug.LogException(e);
        }

        if (sceneId == GameManager.instance.scenesArray.Length - 1)
        {
            nextChapterButton.interactable = false; //If next element from the array is none since we're in the last element of the array
            //Debug.Log("sceneId == scenesArray.Length");
        }

        sceneName = GameManager.instance.scenesArray[sceneId];
        SetChapterImage();
        chapterText.text = GameManager.instance.chaptersArray[sceneId];
        SetPlayButtonText();
        prevChapterButton.interactable = true;

        //Debug.Log("OnNext() sceneId: " + sceneId + ", sceneChapter: " + sceneChapter + ", sceneName: " + sceneName);
    }

    public void OnPrevChapterClick()
    {   
        AudioManager.instance.PlaySFX(clickAudioClip);

        sceneId--;

        if (sceneId == 1)
        {
            prevChapterButton.interactable = false;
            if(GameManager.instance.playerStatsSO.gamepadCount == 1) playButton.Select();
        }

        sceneName = GameManager.instance.scenesArray[sceneId];
        SetChapterImage();
        chapterText.text = GameManager.instance.chaptersArray[sceneId];
        SetPlayButtonText();
        nextChapterButton.interactable = true;

        //Debug.Log("OnPrev() sceneId: " + sceneId + ", sceneChapter: " + sceneChapter + ", sceneName: " + sceneName);
    }

    public void OnPlayClick()
    {   
        AudioManager.instance.PlaySFX(clickAudioClip);

        if (GameManager.instance.scenesUnlockedSO.sceneName == sceneName) //Continues playing the current scene
        {   
            loadMenu.SceneTransition(
                GameManager.instance.scenesUnlockedSO.sceneName,
                GameManager.instance.scenesUnlockedSO.sceneName,
                GameManager.instance.playerStatsSO.xPosition,
                GameManager.instance.playerStatsSO.yPosition
            );
        }
        else if (GameManager.instance.scenesUnlockedSO.sceneName != sceneName) //Ask if replaying a previous chapter
        {
            confirmMenu.ActivateMenu //Activates confirmNewGameMenu & setting up its Listeners
            (
                "Replaying a chapter will lose progress from the current chapter. Are you sure?",
                () => //confirmButton.onClick method
                {   
                    GameManager.instance.saveVariablesKey = "";
                    GameManager.instance.isReplayingChapter = true;
                    
                    loadMenu.SceneTransition( //The replayed scene will become the current scene
                        sceneName,
                        sceneName,
                        0, //GameManager.instance.playerStatsSO.xPosition
                        0 //GameManager.instance.playerStatsSO.yPosition
                    );
                },
                () => //cancelButton.onClick method
                {
                    confirmMenu.DeactivateMenu();
                }
            );
        }
    }

    public void OnBackClick()
    {   
        AudioManager.instance.PlaySFX(clickAudioClip);
        //DeactivateMenu();
        //saveSlotsMenu.ActivateMenu(true);
        loadMenu.SceneTransition(
            "StartMenuScene",
            GameManager.instance.scenesUnlockedSO.sceneName,
            GameManager.instance.playerStatsSO.xPosition,
            GameManager.instance.playerStatsSO.yPosition
        );
    }

    public void ActivateMenu()
    {   
        this.gameObject.SetActive(true);

        //Debug.Log("GameManager.instance.scenesUnlockedSO.sceneName: "+GameManager.instance.scenesUnlockedSO.sceneName);

        for (int i = 1; i < GameManager.instance.chaptersArray.Length; i++)
        {   
            //Debug.Log("i = "+i);
            //Debug.Log("GameManager.instance.scenesArray[i] "+GameManager.instance.scenesArray[i]);
            if (GameManager.instance.scenesUnlockedSO.sceneName == GameManager.instance.scenesArray[i])
            {
                sceneId = i;
                sceneName = GameManager.instance.scenesArray[sceneId];
                SetChapterImage();
                chapterText.text = GameManager.instance.chaptersArray[sceneId];
                SetPlayButtonText();
                break; //breaks the for loop
            }
        }

        //nextChapterButton.interactable
        try
        {
            if (!GameManager.instance.IsSceneUnlock(GameManager.instance.scenesArray[sceneId + 1]))
            {
                nextChapterButton.interactable = false;
            }
        }
        catch (Exception e)
        {
            Debug.LogException(e);
        }

        if (sceneId == GameManager.instance.scenesArray.Length - 1)
        {
            nextChapterButton.interactable = false;
        }

        //prevChapterButton.interactable
        if (sceneId == 1)
        {
            prevChapterButton.interactable = false;
        }

        //Debug.Log("ActivateMenu() sceneId: " + sceneId + ", sceneName: " + sceneName);
    }

    public void DeactivateMenu()
    {
        this.gameObject.SetActive(false);
    }

    private void SetChapterImage()
    {
        if (sceneId == 1)
        {
            chapterImage.sprite = chapter1Sprite;
        }
        else if (sceneId == 2)
        {
            chapterImage.sprite = chapter2Sprite;
        }
        else if (sceneId == 3)
        {
            chapterImage.sprite = chapter3Sprite;
        }
        else if (sceneId == 4)
        {
            chapterImage.sprite = chapter4Sprite;
        }
        else if (sceneId == 5)
        {
            chapterImage.sprite = chapter5Sprite;
        }
        else if (sceneId == 6)
        {
            chapterImage.sprite = chapter6Sprite;
        }
    }

    private void SetPlayButtonText()
    {
        if (GameManager.instance.scenesUnlockedSO.sceneName == sceneName)
        {
            playButtonText.text = "Play";
        }
        else if (GameManager.instance.scenesUnlockedSO.sceneName != sceneName)
        {
            playButtonText.text = "Replay";
        }
    }

}
