using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class NoteUI : MonoBehaviour
{
    private TextMeshProUGUI scoreText;

    void Awake()
    {
        scoreText = GetComponent<TextMeshProUGUI>();
    }

    void Update()
    {
        if (GameManager.instance != null)
        {
            scoreText.text = ("Donuts: " + GameManager.instance.donutsCollected + " / " + GameManager.instance.donutsNeeded);
        }
    }
}
