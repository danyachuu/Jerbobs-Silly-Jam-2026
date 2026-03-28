using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject controlsPanel;

    public void SelectLevel1() => StartLevel(5, 4.0f, 20, 0.5f);
    public void SelectLevel2() => StartLevel(7, 5f, 28, 0.4f);
    public void SelectLevel3() => StartLevel(12, 7f, 50, 0.3f);


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
}
