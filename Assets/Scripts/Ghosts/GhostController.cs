using UnityEngine;
using UnityEngine.AI;

public class GhostController : MonoBehaviour
{
    public Transform pacman;  // Reference to Pac-Man's transform
    private NavMeshAgent agent;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();  // Get the NavMeshAgent attached to the ghost
    }

    void Update()
    {
        if (pacman != null)  // Make sure Pac-Man is assigned
        {
            // Make the ghost chase Pac-Man by setting its destination to Pac-Man's position
            agent.SetDestination(pacman.position);
        }
    }
}
