using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioTrigger : MonoBehaviour
{   
    [Header("AudioClips")]
    public AudioClip audioClipBGM;

    void Start()
    {

    }

    private void OnTriggerEnter2D(Collider2D other) 
    {
        if (other.gameObject.tag == "Player")
        {
            AudioManager.instance.PlayBGM(audioClipBGM);
        }
    }
}
