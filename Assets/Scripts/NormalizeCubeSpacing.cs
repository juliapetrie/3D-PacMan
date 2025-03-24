using UnityEngine;

[ExecuteInEditMode]
public class UniformSpacingScaler : MonoBehaviour
{
    public float desiredSpacing = 2f;

    void Start()
    {
        if (transform.childCount < 2) return;

        // Get current average spacing
        float totalDistance = 0f;
        int count = 0;

        // We'll measure average X or Z spacing between neighbors
        foreach (Transform a in transform)
        {
            foreach (Transform b in transform)
            {
                if (a == b) continue;
                float dist = Vector3.Distance(a.localPosition, b.localPosition);
                totalDistance += dist;
                count++;
            }
        }

        float avgSpacing = totalDistance / count;
        float scaleFactor = desiredSpacing / avgSpacing;

        // Apply scale factor to all positions
        foreach (Transform child in transform)
        {
            child.localPosition *= scaleFactor;
        }
    }
}
