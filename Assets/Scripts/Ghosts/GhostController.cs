using UnityEngine;
using UnityEngine.AI;

public class GhostController : MonoBehaviour
{
    public GhostType ghostType;

    public Transform pacman;
    private NavMeshAgent agent;
    public Transform blinky;

    private bool isFrightened = false;
    private float frightenedTime = 0f;
    public float frightenedSpeed = 3f;
    private float normalSpeed = 2;

    public enum GhostType
    {
        Blinky,
        Pinky,
        Inky,
        Clyde
    }


    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.speed = normalSpeed;
    }

    void Update()
    {
        if (isFrightened)
        {
            RunAwayFromPacMan();
            frightenedTime -= Time.deltaTime;

            if (frightenedTime <= 0f)
            {
                ExitFrightenedState();
            }
        }
        else 
        {
            Chase();
        }


        if (!isFrightened)
        {
            agent.SetDestination(pacman.position);
        }
    }

    void Chase()
    {
        switch (ghostType)
        {
            case GhostType.Blinky:
                agent.SetDestination(pacman.position);
                break;

            case GhostType.Pinky:
                Vector3 pinkyTarget = pacman.position + (pacman.forward * 10);
                agent.SetDestination(pinkyTarget);
                break;

            case GhostType.Inky:
                Vector3 inkyOffset = pacman.position + (pacman.forward * 5);
                Vector3 blinkyToInky = inkyOffset - blinky.position;
                Vector3 inkyTarget = inkyOffset + blinkyToInky;
                agent.SetDestination(inkyTarget);
                break;


        }
    }

    public void StartFrightenedState(float duration)
    {
        isFrightened = true;
        frightenedTime = duration;
        agent.speed = frightenedSpeed;
        Debug.Log("Ghost is now frightened for " + duration + " seconds!");
    }

    void RunAwayFromPacMan()
    {
        Vector3 directionAwayFromPacMan = transform.position - pacman.position;
        agent.SetDestination(transform.position + directionAwayFromPacMan);
    }

    private void ExitFrightenedState()
    {
        isFrightened = false;
        agent.speed = normalSpeed;
        Debug.Log("Ghost is no longer frightened!");
    }



}
