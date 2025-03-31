
using System.Collections.Generic;
using UnityEngine;

public class GhostManager : MonoBehaviour
{
    public static GhostManager Instance { get; private set; }

    public List<GhostController> ghosts;
    public CountdownController countDownController;
    public Transform pacmanstart;
    public PlayerController pacman;





    private void Awake()
    {
        
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void FrightenAllGhosts(float duration)
    {
        foreach (GhostController ghost in ghosts)
        {
            ghost.StartFrightenedState(duration);
        }
    }
}

