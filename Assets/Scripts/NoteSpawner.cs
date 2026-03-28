using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteSpawner : MonoBehaviour
{
    public GameObject notePrefab;
    public Transform[] lanes;
    public float beatInterval = 3.0f;
    private float timer;

    void Start()
    {
        beatInterval = GameConfig.BeatInterval;
    }

    void Update()
    {
        if (GameManager.instance.notesSpawned < GameManager.instance.totalNotesInLevel)
        {
            timer += Time.deltaTime;

            if (timer >= beatInterval)
            {
                SpawnNote();
                timer = 0;
            }
        }
    }

    void SpawnNote()
    {
        
        int randomIndex = Random.Range(0, lanes.Length);

        float spawnX = lanes[randomIndex].position.x;

        Vector3 spawnPos = new Vector3(spawnX, 6f, 0f);

        GameObject newNote = Instantiate(notePrefab, spawnPos, Quaternion.identity);

        NoteObject noteScript = newNote.GetComponent<NoteObject>();
        if (noteScript != null)
        {
            noteScript.speed = GameConfig.NoteSpeed;
        }

        GameManager.instance.notesSpawned++;
    }
}
