using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChapterHUD : MonoBehaviour
{
    [Header("Panels & Controls")]
    [SerializeField] private PlayerControls playerControls;
    [SerializeField] private PlatformerHUD platformerHUDPanel;
    [SerializeField] private GameObject chapterPanel;
    [SerializeField] private GameObject dialoguePanel;
    [SerializeField] private LoadMenu loadMenu;

    [Header("Ink JSON")]
    [SerializeField] private TextAsset inkJSON;

    [Header("Audio Sources")]
    //[SerializeField] private AudioSource bgmSource;
    public AudioClip bgm;

    [HideInInspector] public Animator animator;
    private bool isFirstPressed = true;
    [HideInInspector] public bool isGameOver;


    void Start()
    {   
        DataPersistenceManager.instance.useEncryption = false;
        //loadMenu.ResetScene(GameManager.instance.scenesUnlockedSO.sceneName); //So OnBecameVisible & OnBecameInvisible will work immediately
        
        isGameOver = false;

        playerControls.transform.parent = null;
        playerControls.moveInput = 0;
        playerControls.isInputActive = false;

        platformerHUDPanel.gameObject.SetActive(false);
        platformerHUDPanel.DeactivateMenu();

        chapterPanel.SetActive(true);

        //Default position of the Player means we're just starting a chapter
        if (GameManager.instance.playerStatsSO.xPosition == 0.0 && GameManager.instance.playerStatsSO.yPosition == 0.0)
        {
            animator = GetComponent<Animator>();
            animator.Play("FadeAll");
            StartCoroutine(DialogueCoroutine());
        }
        else
        { //Continues the loaded game without the DialogueCoroutine()
            animator = GetComponent<Animator>();
            animator.Play("FadeOut");
        }
    }

    void Update()
    {
        //Dialogue for Chaper Prologue
        if (Input.GetButtonDown("Jump") && isFirstPressed && dialoguePanel.activeSelf)
        {
            isFirstPressed = false;
            //DialogueManager.instance.EnterDialogueMode(inkJSON);
        }

        if (GameManager.instance.playerStatsSO.energy <= 0 && !isGameOver)
        {
            StartCoroutine(GameOverCoroutine(4));
            isGameOver = true;
        }

        //ChapterHUD Animations
        if(loadMenu.isFadingIn)
        {      
            if(Input.GetButtonDown("Jump")) return; //avoid looping fading in
            playerControls.isInputActive = false;
            animator.Play("FadeIn");
            Debug.Log("FadeIn");
            //loadMenu.isFadingIn = false;
        }
        else if (GameManager.instance.playerStatsSO.energy <= 0)
        {
            animator.Play("FadeIn");
        }
        else if (GameManager.instance.playerStatsSO.energy > 0 && isGameOver)
        {
            animator.Play("FadeOut");
            isGameOver = false;
        }
        else if (!dialoguePanel.activeSelf && !isFirstPressed)
        {
            animator.Play("FadeOut");
        }
    }

    private IEnumerator GameOverCoroutine(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        GameManager.instance.playerStatsSO.energy = GameManager.instance.playerStatsSO.maxEnergy;

        float tempCash = GameManager.instance.playerStatsSO.cash * 0.20f;
        int totalCash = GameManager.instance.playerStatsSO.cash - (int)tempCash;
        GameManager.instance.playerStatsSO.cash = totalCash;
        Debug.Log("totalCash: "+totalCash);
        
        DataPersistenceManager.instance.SaveGame();
        DataPersistenceManager.instance.LoadGame();
        loadMenu.ResetScene(GameManager.instance.scenesUnlockedSO.sceneName);
    }

    private IEnumerator DialogueCoroutine()
    {
        yield return new WaitForSeconds(2);
        DialogueManager.instance.EnterDialogueMode(inkJSON);
        DialogueManager.instance.isContinuePressedDown = true; //Skips the default DialoguePanel, and proceeds to the inkJSON
    }

    public void ActivateGameObjects() //Called in animator.Play("FadeOut");
    {   
        AudioManager.instance.bgmSource.volume = 1;
        AudioManager.instance.PlayBGM(bgm);

        playerControls.isInputActive = true;
        platformerHUDPanel.gameObject.SetActive(true);
        platformerHUDPanel.ActivateMenu();
        loadMenu.isLoadSceneTriggered = false;
    }

    public void DeactivateGameObjects() //Called in animator.Play("FadeIn");
    {   
        StartCoroutine(AudioManager.instance.StartFade(AudioManager.instance.bgmSource, 1f, 0));

        playerControls.transform.parent = null;
        playerControls.moveInput = 0;
        playerControls.isInputActive = false;

        platformerHUDPanel.gameObject.SetActive(false);
        platformerHUDPanel.DeactivateMenu();
    }

    public void LoadSceneTriggered() //Called in the last frame of animator.Play("FadeIn");
    {   
        Debug.Log("loadMenu.isLoadSceneTriggered");
        loadMenu.isLoadSceneTriggered = true;
    }

}