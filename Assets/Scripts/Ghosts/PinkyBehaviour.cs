using UnityEngine;

public class PinkyBehaviour : GhostBehaviour
{
    public Transform PinkyScatterTarget;
    protected override void Chase()
    {
        Vector3 pinkyTarget = pacman.position + (pacman.forward * 10);
        agent.SetDestination(pinkyTarget);
    }

    protected override void Scatter()
    {
        agent.SetDestination(PinkyScatterTarget.position);
    }

}
