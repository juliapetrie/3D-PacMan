using UnityEngine;

public class BlinkyBehaviour : GhostBehaviour
{
    public Transform BlinkyScatterTarget;

    protected override void Chase()
    {
        // Validate and set destination to Pac-Man's position
        SetValidatedDestination(pacman.position);
    }

    protected override void Scatter()
    {
        // Validate and set destination to Blinky's scatter target
        SetValidatedDestination(BlinkyScatterTarget.position);
    }
}
