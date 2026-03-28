using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [Header("Level Stats")]
    public float currentSpeed = 5.0f;
    public int donutsCollected = 0;
    public int donutsNeeded;

    [Header("Note Tracking")]
    public int totalNotesInLevel = 20;
    public int notesSpawned = 0;
    public int notesProcessed = 0; 

    [Header("Combo Tracking")]
    public int perfectCount = 0;
    public int greatCount = 0;

    [Header("End Screens")]
    public GameObject winScreen;
    public GameObject failScreen;

    [Header("Animations")]
    public Animator billyAnimator;

    private bool isGameOver = false;

    private void Awake()
    {
        instance = this;
    }
    void Start()
    {
        donutsNeeded = GameConfig.DonutsNeeded;
        currentSpeed = GameConfig.NoteSpeed;
        totalNotesInLevel = GameConfig.TotalNotes;

        if (winScreen) winScreen.SetActive(false);
        if (failScreen) failScreen.SetActive(false);
    }

    public void RegisterHit(string rating)
    {
        if (isGameOver) return;
        notesProcessed++;

        if (rating == "Perfect")
        {
            perfectCount++;
            CheckCombo();
        }
        else if (rating == "Great")
        {
            greatCount++;
            CheckCombo();
        }
        else // good or missed
        {
            ResetSequence();
        }

        CheckLevelStatus();
    }

    void CheckCombo()
    {
        if (perfectCount >= 3)
        {
            AddDonut();
        }
        else if (perfectCount + greatCount >= 5)
        {
            AddDonut();
        }
    }

    void AddDonut()
    {
        donutsCollected++;
        ResetSequence();
        Debug.Log("DONUT OBTAINED! Total: " + donutsCollected);

    }

    public void NoteMissed()
    {
        if (isGameOver) return;
        notesProcessed++;
        ResetSequence();
        CheckLevelStatus();
    }

    void ResetSequence()
    {
        perfectCount = 0;
        greatCount = 0;
    }

    void CheckLevelStatus()
    {
        if (notesProcessed >= totalNotesInLevel && !isGameOver)
        {
            if (donutsCollected >= donutsNeeded)
            {
                WinLevel();
            }
            else
            {
                FailLevel();
            }
        }
    }

    void WinLevel()
    {
        if (isGameOver) return;
        isGameOver = true;

        if (AudioManager.instance != null && AudioManager.instance.backgroundMusic != null)
            AudioManager.instance.backgroundMusic.Stop();

        StartCoroutine(ShowEndScreenRoutine(true));
    }

    void FailLevel()
    {
        if (isGameOver) return;
        isGameOver = true;

        if (AudioManager.instance != null && AudioManager.instance.backgroundMusic != null)
            AudioManager.instance.backgroundMusic.Stop();

        StartCoroutine(ShowEndScreenRoutine(false));
    }

    IEnumerator ShowEndScreenRoutine(bool won)
    {

        yield return new WaitForSeconds(1f);
        if (billyAnimator != null)
        {
            if (won)
            {
                billyAnimator.SetTrigger("Win");
            }
            else
            {
                billyAnimator.SetTrigger("Fail");
            }
        }

        if (won)
        {
            if (winScreen) winScreen.SetActive(true);
            if (AudioManager.instance != null) AudioManager.instance.PlaySFX(AudioManager.instance.winSound);
        }
        else
        {
            if (failScreen) failScreen.SetActive(true);
            if (AudioManager.instance != null) AudioManager.instance.PlaySFX(AudioManager.instance.loseSound);
        }
        yield return new WaitForSeconds(4.0f);
        SceneManager.LoadScene("MainMenu");
    }

}
