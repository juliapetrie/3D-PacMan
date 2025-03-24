using UnityEngine;

public class ClydeBehaviour : GhostBehaviour
{
    private float distanceThreshold = 8f;
    [SerializeField] Transform bottomLeftCorner;
    [SerializeField] Transform ClydeScatterTarget;

    protected override void Chase()
    {
        if (agent == null || pacman == null || bottomLeftCorner == null) return;

        float distanceToPacman = Vector3.Distance(transform.position, pacman.position);

        if (distanceToPacman < distanceThreshold)
        {
            agent.SetDestination(bottomLeftCorner.position);
        }
        else
        {
            agent.SetDestination(pacman.position);
        }
    }

    protected override void Scatter()
    {
        agent.SetDestination(ClydeScatterTarget.position);
    }

   
}
