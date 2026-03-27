using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class NoteLane : MonoBehaviour
{
    public KeyCode keyToPress;
    public Transform indicator;
    public TextMeshProUGUI laneFeedbackText;

    public Color perfectColor = Color.cyan;
    public Color greatColor = Color.green;
    public Color goodColor = Color.yellow;
    public Color missColor = Color.grey;

    private List<NoteObject> notesInLane = new List<NoteObject>();

    void Start()
    {
        if (laneFeedbackText != null) laneFeedbackText.text = " ";
    }

    void Update()
    {
        if (Input.GetKeyDown(keyToPress) && notesInLane.Count > 0)
        {
            NoteObject note = notesInLane[0];
            float distance = Mathf.Abs(note.transform.position.y - indicator.position.y);

            if (distance < 0.2f) ProcessHit("Perfect", perfectColor);
            else if (distance < 0.5f) ProcessHit("Great", greatColor);
            else if (distance < 0.8f) ProcessHit("Good", goodColor);

            notesInLane.RemoveAt(0);
            Destroy(note.gameObject);
        }
    }
    void ProcessHit(string rating, Color col)
    {
        GameManager.instance.RegisterHit(rating);
        ShowVisualFeedback(rating, col);
    }
    public void ShowVisualFeedback(string msg, Color col)
    {
        if (laneFeedbackText == null) return;

        laneFeedbackText.text = msg;
        laneFeedbackText.color = col;

        CancelInvoke("ClearText");
        Invoke("ClearText", 0.4f);
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
            ShowVisualFeedback("Miss", missColor);
            Destroy(other.gameObject);
        }
    }
    void ClearText()
    {
        if (laneFeedbackText != null)
        {
            laneFeedbackText.text = " ";
        }
    }

   


}
