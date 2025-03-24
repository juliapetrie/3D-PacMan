using UnityEngine;

public class BlinkyBehaviour : GhostBehaviour
{
    public Transform BlinkyScatterTarget;
    protected override void Chase()
    {
        agent.SetDestination(pacman.position);
    }

    protected override void Scatter()
    {
        agent.SetDestination(BlinkyScatterTarget.position);
    }
    

}
