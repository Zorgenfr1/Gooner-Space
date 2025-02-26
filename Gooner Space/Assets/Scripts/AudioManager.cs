using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;
using System.Collections.Generic;
using System;
using UnityEngine.UI;
using System.Threading.Tasks;

public class AudioManager : MonoBehaviour
{
    [Header("-----------Aduio Source---------")]
    [SerializeField] AudioSource musicSource;
    [SerializeField] AudioSource SFXSource;

    [Header("-----------Aduio clip---------")]
    public AudioClip mainMenuIntro;
    public AudioClip mainMenuLooped;
    public AudioClip shopIntro;
    public AudioClip shopLooped;
    public AudioClip gameBackgroundIntro;
    public AudioClip gameBackgroundLooped;
    public AudioClip buyItem;
    public AudioClip moving;
    public AudioClip mineExplosion;
    public AudioClip asteroidCollect;
    public AudioClip buttonConfirm;
    public AudioClip shopExit;
    public AudioClip computerBootUp;
    public AudioClip computerShutDown;
    public AudioClip gameOver;

    private static AudioManager instance;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    public void PlaySFX(AudioClip clip)
    {
        SFXSource.PlayOneShot(clip);
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        PlayMusicForScene(scene.name);
    }

    public void PlayMusicForScene(string sceneName)
    {
        if (musicSource.isPlaying)
            musicSource.Stop();

        switch (sceneName)
        {
            case "meyerStart":
                PlayBackgroundMusic(mainMenuIntro, mainMenuLooped);
                break;
            case "FrodeMaster":
                PlayBackgroundMusic(shopIntro, shopLooped);
                break;
            case "fluid test":
                PlayBackgroundMusic(gameBackgroundIntro, gameBackgroundLooped);
                break;
            case "MeyerDeath":
                musicSource.Stop();
                break;
            default:
                break;
        }
    }

    private async void PlayBackgroundMusic(AudioClip introClip, AudioClip loopClip)
    {
        if (introClip == null || loopClip == null)
        {
            return; 
        }

        musicSource.Stop();
        musicSource.clip = introClip;
        musicSource.loop = false;
        musicSource.Play();

        await Task.Delay((int)(introClip.length * 1000));

        musicSource.clip = loopClip;
        musicSource.loop = true;
        musicSource.Play();
    }
}