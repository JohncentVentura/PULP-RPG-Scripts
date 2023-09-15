using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    [Header("Graphics")]
    SpriteRenderer spriteRenderer;
    [SerializeField] private GameObject visualCueInteract;

    [Header("Ink JSON")]
    [SerializeField] private TextAsset inkJSON;
    private bool isPlayerDetected;
    //bool isVisible = true;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        visualCueInteract.SetActive(false);
        isPlayerDetected = false;
        enabled = false;
    }

    private void Update()
    {
        if (isPlayerDetected && !DialogueManager.instance.isDialoguePlaying)
        {
            visualCueInteract.SetActive(true);

            if (Input.GetButtonDown("Jump"))
            {
                DialogueManager.instance.EnterDialogueMode(inkJSON);
            }
        }
        else
        {
            visualCueInteract.SetActive(false);
        }

        /*
        if (Vector3.Distance(Camera.main.transform.position, transform.position) < 13)
        {
            if (!isVisible)
            {
                spriteRenderer.enabled = true;
                isVisible = true;
                Debug.Log("Visible "+this);
            }
        }
        else if (isVisible)
        {
            spriteRenderer.enabled = false;
            isVisible = false;
            Debug.Log("Invisible "+this);
        }
        */
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            isPlayerDetected = true;
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
        enabled = true;
    }

    void OnBecameInvisible()
    {
        enabled = false;
    }

}