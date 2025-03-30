using UnityEngine;
using System.Collections;

public class PacmanFlashingEffect : MonoBehaviour
{
    [SerializeField] private Renderer pacmanRenderer;
    [SerializeField] private float flashInterval = 0.2f;

    private Coroutine flashCoroutine;

    public void StartFlashing(float duration)
    {
        if (flashCoroutine != null)
            StopCoroutine(flashCoroutine);

        flashCoroutine = StartCoroutine(FlashRoutine(duration));
    }

    public void StopFlashing()
    {
        if (flashCoroutine != null)
        {
            StopCoroutine(flashCoroutine);
            flashCoroutine = null;
        }

        if (pacmanRenderer != null)
            pacmanRenderer.enabled = true;
    }

    private IEnumerator FlashRoutine(float duration)
    {
        float timer = 0f;
        bool isVisible = true;

        while (timer < duration)
        {
            if (pacmanRenderer != null)
                pacmanRenderer.enabled = isVisible;

            isVisible = !isVisible;
            yield return new WaitForSeconds(flashInterval);
            timer += flashInterval;
        }

        if (pacmanRenderer != null)
            pacmanRenderer.enabled = true;
    }
}
