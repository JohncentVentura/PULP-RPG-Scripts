using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverTrigger : MonoBehaviour
{

    public AudioClip audioClip;

    private void OnTriggerEnter2D(Collider2D other) 
    {
        if (other.gameObject.tag == "Player")
        {   
            AudioManager.instance.PlaySFX(audioClip);
            GameManager.instance.playerStatsSO.energy = 0;
        }
    }
}
