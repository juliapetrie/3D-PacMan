using System.Collections.Generic;
using UnityEngine;

public class GhostManager : MonoBehaviour
{

    public List<GhostController> ghosts;

    public void FrightenAllGhosts(float duration)
    {
        foreach (GhostController ghost in ghosts)
        {
            ghost.StartFrightenedState(duration);
        }
    }
}
