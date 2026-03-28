using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class NoteLane : MonoBehaviour
{
    public KeyCode keyToPress;
    public Transform indicator;
    public SpriteRenderer feedbackRenderer;

    public Sprite perfectSprite;
    public Sprite greatSprite;
    public Sprite goodSprite;
    public Sprite missSprite;

    private List<NoteObject> notesInLane = new List<NoteObject>();

    void Start()
    {
        if (feedbackRenderer != null) feedbackRenderer.sprite = null;
    }

    void Update()
    {
        if (Input.GetKeyDown(keyToPress) && notesInLane.Count > 0)
        {
            NoteObject note = notesInLane[0];
            float distance = Mathf.Abs(note.transform.position.y - indicator.position.y);

            if (distance < 0.2f) ProcessHit("Perfect", perfectSprite);
            else if (distance < 0.5f) ProcessHit("Great", greatSprite);
            else if (distance < 0.8f) ProcessHit("Good", goodSprite);

            notesInLane.RemoveAt(0);
            Destroy(note.gameObject);
        }
    }
    void ProcessHit(string rating, Sprite hitSprite)
    {
        GameManager.instance.RegisterHit(rating);
        ShowFeedback(hitSprite);
    }
    public void ShowFeedback(Sprite s)
    {
        if (feedbackRenderer == null) return;

        feedbackRenderer.sprite = s;

        CancelInvoke("ClearFeedback");
        Invoke("ClearFeedback", 0.4f);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Note")) notesInLane.Add(other.GetComponent<NoteObject>());
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Note") && notesInLane.Contains(other.GetComponent<NoteObject>()))
        {
            notesInLane.Remove(other.GetComponent<NoteObject>());
            GameManager.instance.NoteMissed();
            ShowFeedback(missSprite);
            Destroy(other.gameObject);
        }
    }

    void ClearFeedback()
    {
        if (feedbackRenderer != null) feedbackRenderer.sprite = null;
    }


}
