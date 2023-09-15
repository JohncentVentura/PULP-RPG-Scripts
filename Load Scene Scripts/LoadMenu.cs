using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class LoadMenu : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] public GameObject loadMenuPanel;
    private Animator animator;
    [SerializeField] private TextMeshProUGUI chapterText;
    [SerializeField] private Image LoadMenuFill; //Fill from Slider

    private string nextSceneName = "";

    [HideInInspector] public bool isLoadSceneTriggered;
    [HideInInspector] public bool isFadingIn;

    [Header("Audios")]
    [SerializeField] private AudioClip defaultBGM;

    private void Start()
    {   
        //Debug.Log("nextSceneName: "+nextSceneName);
        if(nextSceneName == "")
        {
            loadMenuPanel.SetActive(false);
        }

        isLoadSceneTriggered = false;
        isFadingIn = false;
    }

    //loadSceneName is the scene we'll transition, saveSceneName is the scene the player is currently playing
    public void SceneTransition(string loadSceneName, string saveSceneName, float xPosition, float yPosition)
    {   
        loadMenuPanel.SetActive(true);
        animator = GetComponent<Animator>();
        nextSceneName = loadSceneName;
        GameManager.instance.PrepareGameScene(saveSceneName, xPosition, yPosition);
        SetGameScene();
        //LoadGameScene(); //TEMPORRARY: to load fast, uncomment SetGameScene(); to return to normal
    }

    private void SetGameScene()
    {
        if (nextSceneName == "StartMenuScene" || nextSceneName == "GenderMenuScene" || nextSceneName == "ChapterMenuScene")
        {   
            AudioManager.instance.bgmSource.volume = 1;
            animator.Play("ToDefault");
            chapterText.text = "PULP RPG";
        }
        else if (nextSceneName == GameManager.instance.scenesArray[1])
        {   
            StartCoroutine(AudioManager.instance.StartFade(AudioManager.instance.bgmSource, 1.2f, 0));
            animator.Play("ToChapter1");
            chapterText.text = GameManager.instance.chaptersArray[1];
        }
        else if (nextSceneName == GameManager.instance.scenesArray[2])
        {   
            StartCoroutine(AudioManager.instance.StartFade(AudioManager.instance.bgmSource, 1.2f, 0));
            animator.Play("ToChapter2");
            chapterText.text = GameManager.instance.chaptersArray[2];
        }
        else if (nextSceneName == GameManager.instance.scenesArray[3])
        {   
            StartCoroutine(AudioManager.instance.StartFade(AudioManager.instance.bgmSource, 1.2f, 0));
            animator.Play("ToChapter3");
            chapterText.text = GameManager.instance.chaptersArray[3];
        }
        else if (nextSceneName == GameManager.instance.scenesArray[4])
        {   
            StartCoroutine(AudioManager.instance.StartFade(AudioManager.instance.bgmSource, 1.2f, 0));
            animator.Play("ToChapter4");
            chapterText.text = GameManager.instance.chaptersArray[4];
        }
        else if (nextSceneName == GameManager.instance.scenesArray[5])
        {   
            StartCoroutine(AudioManager.instance.StartFade(AudioManager.instance.bgmSource, 1.2f, 0));
            animator.Play("ToChapter5");
            chapterText.text = GameManager.instance.chaptersArray[5];
        }
        else if (nextSceneName == GameManager.instance.scenesArray[6])
        {   
            StartCoroutine(AudioManager.instance.StartFade(AudioManager.instance.bgmSource, 1.2f, 0));
            animator.Play("ToChapter6");
            chapterText.text = GameManager.instance.chaptersArray[6];
        }

        Debug.Log("ChapterText: " + chapterText.text);
    }

    //Called as Events in animations
    private void LoadGameScene()
    {
        SceneManager.LoadScene(nextSceneName);

        /* IEnumerator Mode
        //AsyncOperation process whose execution can proceed independently or in the background
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneName);

        //If the operation is not completed
        while (!operation.isDone)
        {
            float progressValue = Mathf.Clamp01(operation.progress / 0.9f);

            LoadMenuFill.fillAmount = progressValue;

            yield return null;
        }
        */
    }

    public void ResetScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

}
