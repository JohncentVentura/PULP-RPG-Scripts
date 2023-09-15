using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirCurrent : MonoBehaviour
{   
    [SerializeField] PlayerControls playerControls;
    public bool isPlayerDetected;
    [SerializeField] private float xDirection;
    [SerializeField] private float yDirection;

    private Animator animator;
    [SerializeField] private string animationName;

    void Start()
    {   
        animator = GetComponent<Animator>();
        isPlayerDetected = false; 
        enabled = false;
    }

    void Update()
    {
        if (isPlayerDetected)
        {   
            if (xDirection != 0)
            {   
                playerControls.isInputActive = false;

                if(xDirection >= 1) //Air current moves everything to the right
                {   
                    playerControls.moveInput = 1f;  
                    playerControls.rb.AddForce(0.15f * Vector2.right, ForceMode2D.Impulse);
                }
                else if(xDirection <= -1) //Air current moves everything to the left
                {   
                    playerControls.moveInput = -1f;
                    playerControls.rb.AddForce(-0.15f * Vector2.right, ForceMode2D.Impulse);
                }

            }

            if (yDirection != 0)
            {   
                //Air current moves everything upward
                if(yDirection >= 1) playerControls.rb.gravityScale = -1f;

                //Air current moves everything downward
                else if(yDirection <= -1) playerControls.rb.gravityScale = 4f;
            }
        }

        if(animationName == "ACN")
        {
            animator.Play("ACNorth");
        }
        else if(animationName == "ACE")
        {
            animator.Play("ACEast");
        }
        else if(animationName == "ACW")
        {
            animator.Play("ACWest");
        }
        else if(animationName == "ACS")
        {
            animator.Play("ACSouth");
        }
        
        Debug.Log("AirTigger");
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            //Debug.Log("AC Enter");
            isPlayerDetected = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            //Debug.Log("AC Exit");
            isPlayerDetected = false;
            playerControls.isInputActive = true;
            playerControls.rb.gravityScale = 2.2f;
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
