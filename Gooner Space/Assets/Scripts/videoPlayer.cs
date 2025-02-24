using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;
using System.Collections.Generic;
using System;
using UnityEngine.UI;
using UnityEngine.Video;
using System.Collections;



public class VideoManager : MonoBehaviour
{
    public static VideoManager instance;

    public VideoPlayer videoPlayer;
    public RawImage videoDisplay;
    public GameObject videoPanel;

    private void Awake()
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

        videoPanel.SetActive(false);
    }

    public void ShowGameOverVideo(string videoFileName)
    {
        if (videoPlayer == null || videoDisplay == null || videoPanel == null)
        {
            Debug.LogError("VideoManager has not been initialized properly!");
            return;
        }

        string videoPath = System.IO.Path.Combine(Application.streamingAssetsPath, videoFileName);
        videoPlayer.url = videoPath;

        videoPanel.SetActive(true);
        videoPlayer.Play();

        videoPlayer.targetTexture = new RenderTexture(1920, 1080, 24);
        videoDisplay.texture = videoPlayer.targetTexture;

        StartCoroutine(WaitForVideoFinish());
    }

    private IEnumerator WaitForVideoFinish()
    {
        yield return new WaitUntil(() => videoPlayer.isPrepared);
        yield return new WaitForSeconds((float)videoPlayer.length);

        LoadGameOverScene();
    }

    private void LoadGameOverScene()
    {
        Debug.Log("Video finished, loading the Game Over scene.");
        SceneManager.LoadScene("MeyerDeath");
    }
}


