using System.Runtime.ExceptionServices;
using UnityEditor.VersionControl;
using UnityEngine;
using UnityEngine.AI;
using System.Collections;
using System.Collections.Generic;


public class GhostController2 : MonoBehaviour
{
    [SerializeField] private Transform pacman;
    //public List<GhostBehaviour> ghosts;
    public Transform homePosition;
    public float respawnTime = 5f;
    public GameObject[] ghostObjects;

    public float frightenedDuration = 7f;

    private bool isReturningHome = false;

    private List<GhostBehaviour> ghosts = new List<GhostBehaviour>();

    private void Start()
    {


        // Add the GhostBehavior components to the ghosts list
        foreach (GameObject ghostObject in ghostObjects)
        {
            GhostBehaviour ghostBehavior = ghostObject.GetComponent<GhostBehaviour>();
            if (ghostBehavior != null)
            {
                ghosts.Add(ghostBehavior);
            }
        }
    }

    private void Update()
    {
        if (isReturningHome)
        {
            return;
        }

        foreach (var ghost in ghosts)
        {
            if (ghost.isFrightened)
            {
                Debug.Log($"{ghost.name} is frightened!");
                ghost.RunAwayFromPacMan(transform.position - pacman.position);
                ghost.frightenedTime -= Time.deltaTime;

                if (ghost.frightenedTime <= 0f)
                {
                    ghost.ExitFrightenedState();
                }
            }
        }
    }
    private void FrightenAllGhosts()
    {
        foreach (GhostBehaviour ghost in ghosts)
        {
            ghost.StartFrightenedState(frightenedDuration);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Pacman"))
        {
            foreach (var ghost in ghosts)
            {
                if (ghost.isFrightened)
                {
                    Debug.Log("Pac-Man caught the ghost!");
                    ghost.returnHome(homePosition.position, respawnTime);
                }
            }
        }
    }

    public void StartFrightenedState(float duration)
    {
        foreach (var ghost in ghosts)
        {
            ghost.StartFrightenedState(duration);
        }
    }
}



