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
    public int donutsNeeded = 3;

    [Header("Note Tracking")]
    public int totalNotesInLevel = 20;
    public int notesSpawned = 0;
    public int notesProcessed = 0; 

    [Header("Combo Tracking")]
    public int perfectCount = 0;
    public int greatCount = 0;

    private void Awake()
    {
        instance = this;
    }

    public void RegisterHit(string rating)
    {
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

        if (donutsCollected >= donutsNeeded)
        {
            WinLevel();
        }
    }

    public void NoteMissed()
    {
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
        if (notesProcessed >= totalNotesInLevel && donutsCollected < donutsNeeded)
        {
            FailLevel();
        }
    }

    void WinLevel()
    {
        Debug.Log("LEVEL COMPLETE! Loading next...");
        // For now, just reload or stop spawning
        // SceneManager.LoadScene("NextLevelName"); 
    }

    void FailLevel()
    {
        Debug.Log("FAILED! Not enough donuts for the child.");
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

}
