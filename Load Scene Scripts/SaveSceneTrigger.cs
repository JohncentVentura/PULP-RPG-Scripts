using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveSceneTrigger : MonoBehaviour
{  
    [SerializeField] private PlayerControls playerControls;
    [SerializeField] private PlayerMenu playerMenu;
    [SerializeField] private GameObject visualCueInteract;
    private Animator animator;

    private bool isPlayerDetected;

    public AudioClip audioClip;

    private void Start()
    {
        animator = GetComponent<Animator>();
        visualCueInteract.SetActive(false);
        //enabled = false;
    }

    private void Update()
    {   
        animator.Play("SaveSceneTrigger");

        if (isPlayerDetected)
        {   
            visualCueInteract.SetActive(true);

            if (Input.GetButtonDown("Jump"))
            {
                playerMenu.ActivateMenu();
                playerMenu.gameObject.SetActive(true);
            }
        }
        else
        {
            visualCueInteract.SetActive(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {   
            if(playerControls.isInputActive)
            {
                AudioManager.instance.PlaySFX(audioClip);
            }
            isPlayerDetected = true;
            GameManager.instance.inventorySO.isItemUsed = false;
            //GameManager.instance.playerStatsSO.energy = GameManager.instance.playerStatsSO.maxEnergy;
            GameManager.instance.PrepareGameScene(GameManager.instance.scenesUnlockedSO.sceneName, playerControls.transform.position.x, playerControls.transform.position.y);
            //GameManager.instance.PrepareGameScene(GameManager.instance.scenesUnlockedSO.sceneName, this.gameObject.transform.position.x, this.gameObject.transform.position.y);
            DataPersistenceManager.instance.SaveGame();
            DataPersistenceManager.instance.LoadGame();
        }
    }



    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            isPlayerDetected = false;
        }
    }

    void OnBecameVisible()
    {
        //enabled = true;
    }

    void OnBecameInvisible()
    {
        //enabled = false;
    }
}
