using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;
    public AudioSource backgroundMusic;
    public AudioSource sfxSource;

    public AudioClip backgroundMusicClip;
    public AudioClip hitSound;
    public AudioClip hoverSound;
    public AudioClip clickSound;
    public AudioClip winSound;
    public AudioClip loseSound;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        if (backgroundMusic != null && backgroundMusicClip != null)
        {
            backgroundMusic.clip = backgroundMusicClip;
            backgroundMusic.loop = true;
            backgroundMusic.Play();
        }
    }

    public void PlaySFX(AudioClip clip)
    {
        if (sfxSource != null && clip != null)
        {
            sfxSource.PlayOneShot(clip);
        }
    }


}
