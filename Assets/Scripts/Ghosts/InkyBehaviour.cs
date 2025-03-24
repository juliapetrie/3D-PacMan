using UnityEngine;

public class InkyBehaviour : GhostBehaviour
{
    [SerializeField] Transform blinky;
    [SerializeField] Transform InkyScatterTarget;
    protected override void Chase()
    {
        Vector3 inkyOffset = pacman.position + (pacman.forward * 5);
        Vector3 blinkyToInky = inkyOffset - blinky.position;
        Vector3 inkyTarget = inkyOffset + blinkyToInky;
        agent.SetDestination(inkyTarget);
    }

    protected override void Scatter()
    {
        agent.SetDestination(InkyScatterTarget.position);
    }
}
