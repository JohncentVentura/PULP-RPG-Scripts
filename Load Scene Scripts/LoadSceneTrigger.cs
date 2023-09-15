using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadSceneTrigger : MonoBehaviour
{   
    [SerializeField] private PlayerControls playerControls;
    [SerializeField] private LoadMenu loadMenu;

    [Header("Scene Transition")]
    [SerializeField] private string sceneName;
    [SerializeField] private float xPosition;
    [SerializeField] private float yPosition;
    private Animator animator;
    private bool stopTriggerEnterLoop;

    public AudioClip defaultBGM;

    private void Start()
    {
        animator = GetComponent<Animator>();
        stopTriggerEnterLoop = false;
    }

    private void Update()
    {
        animator.Play("LoadSceneTrigger");
    }

    private void OnTriggerEnter2D(Collider2D other)
    {   
        if (other.gameObject.tag == "Player" && !loadMenu.isLoadSceneTriggered && !stopTriggerEnterLoop)
        {   
            DataPersistenceManager.instance.useEncryption = true;
            playerControls.moveSpeed = 0;
            loadMenu.isFadingIn = true;
            stopTriggerEnterLoop = true;
            Debug.Log("LoadSceneTrigger isFadingIn");
        }
    } 

    private void OnTriggerStay2D(Collider2D other)
    {   
        if (other.gameObject.tag == "Player" && loadMenu.isLoadSceneTriggered)
        {   
            playerControls.moveSpeed = 0;
            GameManager.instance.SetIsSceneUnlock(sceneName, true);
            loadMenu.SceneTransition("ChapterMenuScene", sceneName, xPosition, yPosition);
            loadMenu.isLoadSceneTriggered = false;
            AudioManager.instance.PlayBGM(defaultBGM);
        }
    }

}