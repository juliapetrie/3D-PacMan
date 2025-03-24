using UnityEngine;

public class PowerPellet : MonoBehaviour
{

    public float frightenedDuration = 7f;
    public GhostManager ghostManager;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Pacman"))
        {
            if (ghostManager != null)
            {
                ghostManager.FrightenAllGhosts(frightenedDuration);
            }

            Destroy(gameObject);
        }
    }
}
