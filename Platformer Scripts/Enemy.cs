using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{   
    public enum STATES
    {
        IdleState,
        MoveState,
        NormalAtkState,
        StopAtkState,
        SlainState
    }
    public STATES state;

    private Rigidbody2D body;
    private Animator animator;

    [SerializeField] private float moveSpeed; //Old: 0.2
    [SerializeField] private float chaseSpeed;
    [SerializeField] private float minInclusive; //For Random.Range, old: -0.4f
    [SerializeField] private float maxInclusive; //For Random.Range, old: -0.5f

    private Vector3 moveDirection = new Vector3(1, 0, 0);
    private Vector3 spawnPosition;
    private Vector3 targetPosition = Vector3.zero;
    private bool isAttacking = false;

    [SerializeField] string animationName;

    [SerializeField] private AudioClip audioClip;

    void Start()
    {
        body = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        animator.SetFloat("Direction", moveDirection.x);
        spawnPosition = transform.position;
        state = STATES.IdleState;
        StartCoroutine(MoveTimer());
    }

    void Update() //For State Transitions & Animations
    {   
        if (state == STATES.IdleState)
        {
            Idling();
        }
        else if (state == STATES.MoveState)
        {
            Moving();
        }
        else if (state == STATES.NormalAtkState)
        {
            NormalAtk();
        }
        else if (state == STATES.StopAtkState)
        {
            StopAtk();
        }
    }

    void FixedUpdate()
    {
        if (state == STATES.MoveState)
        {
            body.MovePosition(Vector2.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime));
        }
        else if (state == STATES.NormalAtkState)
        {
            body.MovePosition(Vector2.MoveTowards(transform.position, FindObjectOfType<PlayerControls>().transform.position, chaseSpeed * Time.deltaTime));
        }
        else if (state == STATES.StopAtkState)
        {
            body.MovePosition(Vector2.MoveTowards(transform.position, spawnPosition, moveSpeed * Time.deltaTime));
        }
        else
        {
            body.velocity = Vector2.zero * moveSpeed * Time.deltaTime;
        }
    }

    private IEnumerator MoveTimer()
    {
        while (true)
        {
            //Initializes the RNG with a new seed
            Random.InitState(System.DateTime.Now.Millisecond);

            //randomSec is the seconds for the MoveTimer to yield, so monsters won't have same RNG seed
            float randomSec = Random.Range(1f, 4f);
            yield return new WaitForSeconds(randomSec);
            int randomNum = Random.Range(0, 2);
            
            if (randomNum == 0 && !isAttacking) //state = STATES.IdleState;
            {
                state = STATES.IdleState;
            }
            else if (randomNum == 1 && !isAttacking) //state = STATES.MoveState;
            {
                moveDirection = new Vector2(Random.Range(minInclusive, maxInclusive), transform.position.y); //Old: Random.Range(-0.4f, 0.5f)
                targetPosition = moveDirection + spawnPosition;
                state = STATES.MoveState;
            }
        }
    }

    private void Idling()
    {   
        animator.SetFloat("Direction", moveDirection.x);
        
        if(animationName == "Dog1")
        {
            animator.Play("Dog1Idle");
        }
        else if(animationName == "Dog2")
        {
            animator.Play("Dog2Idle");
        }

    }

    private void Moving()
    {   
        animator.SetFloat("Direction", (targetPosition.x - transform.position.x));

        if(animationName == "Dog1")
        {
            animator.Play("Dog1Move");
        }
        else if(animationName == "Dog2")
        {
            animator.Play("Dog2Move");
        }

        if (transform.position == targetPosition)
        {
            state = STATES.IdleState;
        }
    }

    private void NormalAtk()
    {   
        animator.SetFloat("Direction", (FindObjectOfType<PlayerControls>().transform.position.x - transform.position.x));
        
        if(animationName == "Dog1")
        {
            animator.Play("Dog1Move");
        }
        else if(animationName == "Dog2")
        {
            animator.Play("Dog2Move");
        }
    }

    private void StopAtk()
    {   
        animator.SetFloat("Direction", (spawnPosition.x - transform.position.x));
        
        if(animationName == "Dog1")
        {
            animator.Play("Dog1Move");
        }
        else if(animationName == "Dog2")
        {
            animator.Play("Dog2Move");
        }

        if (transform.position == spawnPosition)
        {
            state = STATES.IdleState;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {   
            AudioManager.instance.PlaySFX(audioClip);
            isAttacking = true;
            state = STATES.NormalAtkState;
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            isAttacking = true;
            state = STATES.NormalAtkState;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            isAttacking = false;
            state = STATES.StopAtkState;
        }
    }
}
