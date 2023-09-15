using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bird : MonoBehaviour
{
    private Animator animator;
    private Rigidbody2D body;
    private SpriteRenderer spriteRenderer;

    private float flyDirectionX;
    private float flyDirectionY;
    private Vector3 targetPosition;
    private Vector3 moveDirection;
    private Vector3 spawnPosition;

    private bool isPlayerDetected;

    [SerializeField] private AudioClip wingsFlapClip;

    // Start is called before the first frame update
    void Start()
    {   
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        body = GetComponent<Rigidbody2D>();
        flyDirectionY = 8;
        spawnPosition = transform.position;
        isPlayerDetected = false;

        if(spriteRenderer.flipX) //bird going to left
        {
            animator.SetFloat("Direction", -0.1f);
            flyDirectionX = -10f;
        }
        else if(!spriteRenderer.flipX) //bird going to right
        {   
            animator.SetFloat("Direction", 0.1f);
            flyDirectionX = 10f;
        }   

        enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(isPlayerDetected)
        {   
            animator.Play("BirdMove");
            moveDirection = new Vector2(flyDirectionX, flyDirectionY);
            animator.SetFloat("Direction", moveDirection.x);
            targetPosition = moveDirection + spawnPosition;
            body.MovePosition(Vector2.MoveTowards(transform.position, targetPosition, 8f * Time.deltaTime));
            StartCoroutine(DeleteCoroutine());
        }
        else if(!isPlayerDetected)
        {
            animator.Play("BirdIdle");
        }
    }

    private void OnTriggerEnter2D(Collider2D other) 
    {
        if(other.gameObject.tag == "Player")
        {   
            isPlayerDetected = true;
            AudioManager.instance.PlaySFX(wingsFlapClip);
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

    private IEnumerator DeleteCoroutine()
    {
        yield return new WaitForSeconds(2);
        this.gameObject.SetActive(false);
    }
}
