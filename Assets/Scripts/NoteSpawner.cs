using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteSpawner : MonoBehaviour
{
    public GameObject notePrefab;
    public Transform[] lanes;
    public float beatInterval = 1.0f;
    private float timer;

    void Update()
    {
        if (GameManager.instance.notesSpawned < GameManager.instance.totalNotesInLevel)
        {
            timer += Time.deltaTime;

            if (timer >= beatInterval / GameManager.instance.currentSpeed)
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
        Instantiate(notePrefab, spawnPos, Quaternion.identity);

        GameManager.instance.notesSpawned++;
    }
}
