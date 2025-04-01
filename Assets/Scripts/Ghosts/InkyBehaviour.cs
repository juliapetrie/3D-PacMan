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
        SetValidatedDestination(inkyTarget);
    }

    protected override void Scatter()
    {
        SetValidatedDestination(InkyScatterTarget.position);
    }
}
