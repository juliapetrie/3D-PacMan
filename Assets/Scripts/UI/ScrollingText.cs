using UnityEngine;

public class ScrollingText : MonoBehaviour
{
    public RectTransform textRect;
    public float scrollSpeed = 50f;

    private float textWidth;
    private float viewWidth;
    private bool initialized = false;

    void Start()
    {
        StartCoroutine(InitAfterFrame());
    }

    System.Collections.IEnumerator InitAfterFrame()
    {
        yield return null; // 1 frame delay

        textWidth = textRect.rect.width;
        viewWidth = ((RectTransform)textRect.parent).rect.width;

        // staggering for "looped" text effect between text A and text B
        textRect.anchoredPosition = new Vector2(-textWidth, textRect.anchoredPosition.y);

        initialized = true;
    }

    void Update()
    {
        if (!initialized) return;

        Vector2 pos = textRect.anchoredPosition;
        pos.x += scrollSpeed * Time.deltaTime;

        // Once scrolled fully reset left
        if (pos.x >= viewWidth)
        {
            pos.x = -textWidth;
        }

        textRect.anchoredPosition = pos;
    }
}
