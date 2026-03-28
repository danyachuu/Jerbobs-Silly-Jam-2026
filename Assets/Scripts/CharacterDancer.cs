using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterDancer : MonoBehaviour
{
    public static CharacterDancer instance;

    public SpriteRenderer momRenderer;

    [Header("Dance Sprites")]
    public Sprite defaultSprite;
    public Sprite leftSprite;
    public Sprite rightSprite;
    public Sprite upSprite;
    public Sprite downSprite;

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        if (momRenderer != null) momRenderer.sprite = defaultSprite;
    }

    public void Dance(KeyCode keyToPress)
    {
        if (momRenderer == null) return;

        CancelInvoke("ResetToDefault");

        switch (keyToPress)
        {
            case KeyCode.Q: momRenderer.sprite = leftSprite; break;
            case KeyCode.R: momRenderer.sprite = rightSprite; break;
            case KeyCode.W: momRenderer.sprite = upSprite; break;
            case KeyCode.E: momRenderer.sprite = downSprite; break;
            default: momRenderer.sprite = defaultSprite; break;
        }

        Invoke("ResetToDefault", 0.7f);
    }

    void ResetToDefault()
    {
        momRenderer.sprite = defaultSprite;
    }
}
