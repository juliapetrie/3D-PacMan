using UnityEngine;

[ExecuteInEditMode]
public class GridAligner : MonoBehaviour
{
    public float targetSpacing = 2f;
    public float alignmentThreshold = 0.1f;

    void Start()
    {
        SnapToGrid();
    }

    void SnapToGrid()
    {
        Transform[] children = new Transform[transform.childCount];
        for (int i = 0; i < transform.childCount; i++)
            children[i] = transform.GetChild(i);

        // Align by rows (Z)
        foreach (Transform t in children)
        {
            foreach (Transform other in children)
            {
                if (t == other) continue;

                // Check if they're in same row (Z)
                if (Mathf.Abs(t.localPosition.z - other.localPosition.z) < alignmentThreshold)
                {
                    float row = Mathf.Round(t.localPosition.x / targetSpacing);
                    t.localPosition = new Vector3(row * targetSpacing, t.localPosition.y, other.localPosition.z);
                    break;
                }
            }
        }

        // Align by columns (X)
        foreach (Transform t in children)
        {
            foreach (Transform other in children)
            {
                if (t == other) continue;

                // Check if they're in same column (X)
                if (Mathf.Abs(t.localPosition.x - other.localPosition.x) < alignmentThreshold)
                {
                    float col = Mathf.Round(t.localPosition.z / targetSpacing);
                    t.localPosition = new Vector3(other.localPosition.x, t.localPosition.y, col * targetSpacing);
                    break;
                }
            }
        }
    }
}
