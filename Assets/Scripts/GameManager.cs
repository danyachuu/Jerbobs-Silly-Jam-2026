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
        if (notesSpawned >= totalNotesInLevel && notesProcessed >= notesSpawned)
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
        Debug.Log("LEVEL COMPLETE! Loading next...");
        StartCoroutine(ReturnToMenuAfterDelay());
        // For now, just reload or stop spawning
        // SceneManager.LoadScene("NextLevelName"); 
    }

    void FailLevel()
    {
        if (isGameOver) return;
        isGameOver = true;

        Debug.Log("FAILED! Returning to menu in 4 seconds...");
        StartCoroutine(ReturnToMenuAfterDelay());
    }

    IEnumerator ReturnToMenuAfterDelay()
    {

        yield return new WaitForSeconds(4f);
        SceneManager.LoadScene("MainMenu");
    }

}
