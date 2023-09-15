using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Hurtbox : MonoBehaviour
{
    [SerializeField] PlayerControls playerControls;
    private float invincibleTimer;
    private float flashTimer;
    private Color invincibleColor;

    private bool isPlayerInvincible = false;

    [SerializeField] private AudioClip hurtBoxClip;
    [SerializeField] private AudioClip playerHurtClip;
    [SerializeField] private AudioClip playerShockClip;

    void Start()
    {
        invincibleTimer = 1;
    }

    private void Update()
    {
        if (flashTimer >= 0f && playerControls != null)
        {
            playerControls.transform.parent = null; //For MovingPlatform
            flashTimer -= Time.deltaTime; //Decrements flashTimer until it reaches 0 to stop HurtAnimation
            HurtAnimation();
        }
    }

    private void OnCollisionEnter2D(Collision2D other) 
    {
        if (other.gameObject.tag == "Player")
        {   
            AudioManager.instance.PlaySFX(hurtBoxClip);
            this.gameObject.SetActive(false);
        }

        PlayerHurtCollision2D(other);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {   
            AudioManager.instance.PlaySFX(hurtBoxClip);
            this.gameObject.SetActive(false);
        }

        PlayerHurtCollider2D(other);
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        PlayerHurtCollider2D(other);
    }

    private void PlayerHurtCollision2D(Collision2D otherCollision)
    {   
        if (isPlayerInvincible)
        {
            return;
        }
        else if (otherCollision.gameObject.tag == "EnemyLv1")
        {   
            AudioManager.instance.PlaySFX(playerShockClip);
            flashTimer = invincibleTimer;
            GameManager.instance.playerStatsSO.energy = GameManager.instance.playerStatsSO.energy - 1;
            DataPersistenceManager.instance.SaveGame();
            StartCoroutine(PlayerInvincibleCoroutine());
        }
        else if (otherCollision.gameObject.tag == "EnemyLv2")
        {   
            AudioManager.instance.PlaySFX(playerShockClip);
            flashTimer = invincibleTimer;
            GameManager.instance.playerStatsSO.energy = GameManager.instance.playerStatsSO.energy - 2;
            DataPersistenceManager.instance.SaveGame();
            StartCoroutine(PlayerInvincibleCoroutine());
        }
        else if (otherCollision.gameObject.tag == "EnemyLv3")
        {   
            AudioManager.instance.PlaySFX(playerShockClip);
            flashTimer = invincibleTimer;
            GameManager.instance.playerStatsSO.energy = GameManager.instance.playerStatsSO.energy - 3;
            DataPersistenceManager.instance.SaveGame();
            StartCoroutine(PlayerInvincibleCoroutine());
        }
        else if (otherCollision.gameObject.tag == "EnemyLv4")
        {   
            AudioManager.instance.PlaySFX(playerShockClip);
            flashTimer = invincibleTimer;
            GameManager.instance.playerStatsSO.energy = GameManager.instance.playerStatsSO.energy - 4;
            DataPersistenceManager.instance.SaveGame();
            StartCoroutine(PlayerInvincibleCoroutine());
        }
        else if (otherCollision.gameObject.tag == "EnemyLv5")
        {   
            AudioManager.instance.PlaySFX(playerShockClip);
            flashTimer = invincibleTimer;
            GameManager.instance.playerStatsSO.energy = GameManager.instance.playerStatsSO.energy - 5;
            DataPersistenceManager.instance.SaveGame();
            StartCoroutine(PlayerInvincibleCoroutine());
        }
        else if (otherCollision.gameObject.tag == "EnemyLv6")
        {   
            AudioManager.instance.PlaySFX(playerShockClip);
            flashTimer = invincibleTimer;
            GameManager.instance.playerStatsSO.energy = GameManager.instance.playerStatsSO.energy - 6;
            DataPersistenceManager.instance.SaveGame();
            StartCoroutine(PlayerInvincibleCoroutine());
        }
    }

    private void PlayerHurtCollider2D(Collider2D otherCollider)
    {   
        //AudioManager.instance.PlaySFX(playerShockClip);

        if (isPlayerInvincible)
        {
            return;
        }
        else if (otherCollider.gameObject.tag == "EnemyLv1")
        {   
            AudioManager.instance.PlaySFX(playerHurtClip);
            flashTimer = invincibleTimer;
            GameManager.instance.playerStatsSO.energy = GameManager.instance.playerStatsSO.energy - 1;
            DataPersistenceManager.instance.SaveGame();
            StartCoroutine(PlayerInvincibleCoroutine());
        }
        else if (otherCollider.gameObject.tag == "EnemyLv2")
        {   
            AudioManager.instance.PlaySFX(playerHurtClip);
            flashTimer = invincibleTimer;
            GameManager.instance.playerStatsSO.energy = GameManager.instance.playerStatsSO.energy - 2;
            DataPersistenceManager.instance.SaveGame();
            StartCoroutine(PlayerInvincibleCoroutine());
        }
        else if (otherCollider.gameObject.tag == "EnemyLv3")
        {   
            AudioManager.instance.PlaySFX(playerHurtClip);
            flashTimer = invincibleTimer;
            GameManager.instance.playerStatsSO.energy = GameManager.instance.playerStatsSO.energy - 3;
            DataPersistenceManager.instance.SaveGame();
            StartCoroutine(PlayerInvincibleCoroutine());
        }
        else if (otherCollider.gameObject.tag == "EnemyLv4")
        {   
            AudioManager.instance.PlaySFX(playerHurtClip);
            flashTimer = invincibleTimer;
            GameManager.instance.playerStatsSO.energy = GameManager.instance.playerStatsSO.energy - 4;
            DataPersistenceManager.instance.SaveGame();
            StartCoroutine(PlayerInvincibleCoroutine());
        }
        else if (otherCollider.gameObject.tag == "EnemyLv5")
        {   
            AudioManager.instance.PlaySFX(playerHurtClip);
            flashTimer = invincibleTimer;
            GameManager.instance.playerStatsSO.energy = GameManager.instance.playerStatsSO.energy - 5;
            DataPersistenceManager.instance.SaveGame();
            StartCoroutine(PlayerInvincibleCoroutine());
        }
        else if (otherCollider.gameObject.tag == "EnemyLv6")
        {   
            AudioManager.instance.PlaySFX(playerHurtClip);
            flashTimer = invincibleTimer;
            GameManager.instance.playerStatsSO.energy = GameManager.instance.playerStatsSO.energy - 6;
            DataPersistenceManager.instance.SaveGame();
            StartCoroutine(PlayerInvincibleCoroutine());
        }
    }

    private IEnumerator PlayerInvincibleCoroutine()
    {
        isPlayerInvincible = true;
        //Debug.Log("isPlayerInvincible: " + isPlayerInvincible);

        yield return new WaitForSeconds(invincibleTimer);
        isPlayerInvincible = false;
        //Debug.Log("isPlayerInvincible: " + isPlayerInvincible);
    }

    private void HurtAnimation()
    {
        float alpha = 0f;
        for (float f = 0.95f; f > 0f; f -= 0.03f) //Decrements f by 0.05 each loop
        {
            if (flashTimer > (invincibleTimer * f))
            {
                playerControls.gameObject.GetComponent<SpriteRenderer>().color = new Color(playerControls.gameObject.GetComponent<SpriteRenderer>().color.r,
                    playerControls.gameObject.GetComponent<SpriteRenderer>().color.g, playerControls.gameObject.GetComponent<SpriteRenderer>().color.b, alpha);

                //Toggles if alpha of a SpriteRenderer is 0 (Tranparent) or 1 (Non-Transparent), creating a flash animation
                if (alpha == 0)
                {
                    alpha = 1;
                }
                else if (alpha == 1)
                {
                    alpha = 0;
                }
            }
            else //if flashTimer <= 0, reset the SpriteRenderers
            {
                playerControls.gameObject.GetComponent<SpriteRenderer>().color = new Color(playerControls.gameObject.GetComponent<SpriteRenderer>().color.r,
                    playerControls.gameObject.GetComponent<SpriteRenderer>().color.g, playerControls.gameObject.GetComponent<SpriteRenderer>().color.b, 1);
            }
        }
    }


    private void OnTriggerExit2D(Collider2D other)
    {

    }

}
