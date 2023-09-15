using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlipperyObject : MonoBehaviour
{
    [SerializeField] PlayerControls playerControls;

    void Start()
    {
        enabled = false;
    }


    void Update()
    {

    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            playerControls.moveSpeed = 9;
            playerControls.frictionAmount = 0f;
            playerControls.isWallSlippery = true;
        }
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            playerControls.moveSpeed = 6;
            playerControls.frictionAmount = 0.4f;
            playerControls.isWallSlippery = false;
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
