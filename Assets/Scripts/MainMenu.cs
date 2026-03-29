using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;
using System.IO;

public class MainMenu : MonoBehaviour
{
    private static bool hasPlayedIntro = false;

    [Header("Intro Settings")]
    public GameObject introObject;
    public VideoPlayer introVideo;
    public GameObject menuButtons;


    private bool isIntroPlaying = false;

    public GameObject controlsPanel;

    public void SelectLevel1() => StartLevel(5, 4.0f, 20, 0.5f);
    public void SelectLevel2() => StartLevel(8, 5f, 34, 0.3f);
    public void SelectLevel3() => StartLevel(10, 6.8f, 50, 0.3f);

    void Start()
    {
        if (hasPlayedIntro)
        {
            SkipToMenu();
            return;
        }

        SetupAndPlayIntro();
    }

    void SetupAndPlayIntro()
    {
        isIntroPlaying = true;
        hasPlayedIntro = true;

        if (introObject) introObject.SetActive(true);
        if (menuButtons) menuButtons.SetActive(false);

        if (introVideo != null)
        {
            introVideo.enabled = true;

            string videoName = "Intro Animation.mp4";
            string videoPath = Path.Combine(Application.streamingAssetsPath, videoName);

            introVideo.source = VideoSource.Url;
            introVideo.url = videoPath;

            introVideo.Play();
            introVideo.loopPointReached += (vp) => EndIntro();
        }

        StartCoroutine(AutoEndIntro());
    }

    void SkipToMenu()
    {
        isIntroPlaying = false;
        if (introVideo != null) introVideo.enabled = false;
        if (introObject) introObject.SetActive(false);
        if (menuButtons) menuButtons.SetActive(true);

        if (AudioManager.instance != null)
        {
            AudioManager.instance.StartBackgroundMusic();
        }
    }

    void Update()
    {
        if (isIntroPlaying && Input.anyKeyDown)
        {
            EndIntro();
        }
    }

    public void StartLevel(int donuts, float speed, int totalNotes, float interval)
    {
        GameConfig.DonutsNeeded = donuts;
        GameConfig.NoteSpeed = speed;
        GameConfig.TotalNotes = totalNotes;
        GameConfig.BeatInterval = interval;

        SceneManager.LoadScene("GameScene");
    }

    public void OpenControls() => controlsPanel.SetActive(true);
    public void CloseControls() => controlsPanel.SetActive(false);

    IEnumerator AutoEndIntro()
    {
        yield return new WaitForSeconds(18f);
        if (isIntroPlaying) EndIntro();
    }

    void EndIntro()
    {
        if (!isIntroPlaying) return; 

        isIntroPlaying = false;

        if (introVideo != null) introVideo.Stop(); 
        if (introObject) introObject.SetActive(false);
        if (menuButtons) menuButtons.SetActive(true);

        if (AudioManager.instance != null)
        {
            AudioManager.instance.StartBackgroundMusic();
        }
    }
}
