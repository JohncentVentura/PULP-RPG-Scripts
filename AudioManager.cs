using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance { get; private set; } //AudioManager is a singleton

    public AudioSource bgmSource, sfxSource;

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(this.gameObject); //If there is another instance of this class, destroy it because this class is a singleton
            return;
        }
        instance = this; //If there are no instance of this class, this becomes the first & only instance of this class
        DontDestroyOnLoad(this.gameObject); //The gameObject must be in the root of a scene, to persist when loading to other scenes
    }

    private void Start()
    {
    }

    public void PlayBGM(AudioClip audioClip)
    {
        bgmSource.clip = audioClip;
        bgmSource.Play();

        //bgmSource.PlayOneShot(audioClip);
    }

    public void PlaySFX(AudioClip audioClip)
    {
        sfxSource.PlayOneShot(audioClip);
    }

    public IEnumerator StartFade(AudioSource audioSource, float duration, float targetVolume)
    {
        float currentTime = 0;
        float start = audioSource.volume;
        while (currentTime < duration)
        {
            currentTime += Time.deltaTime;
            audioSource.volume = Mathf.Lerp(start, targetVolume, currentTime / duration);
            yield return null;
        }
        yield break;
    }

}