using UnityEditor.VersionControl;
using UnityEngine;
using UnityEngine.AI;

public class GhostController : MonoBehaviour
{
    public Transform blinky;
    public Transform pacman;              
    public Transform bottomLeftCorner;  
    private NavMeshAgent agent;
    private float distanceThreshold = 8f;


    private bool isFrightened = false;
    private float frightenedTime = 0f;
    public float frightenedSpeed = 3f;
    private float normalSpeed = 2;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        
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

    void Chase()
    {
        if (agent.name == "Clyde")
        {
            ClydeChase();
        }
        if (agent.name == "Blinky")
        {
            BlinkyChase();
        }
        if (agent.name == "Pinky")
        {
            PinkyChase();
        }
        if (agent.name == "Inky")
        {
            InkyChase();
        }
    }

    void PinkyChase()
    {
        Vector3 pinkyTarget = pacman.position + (pacman.forward * 10);
        agent.SetDestination(pinkyTarget);

    }

    void InkyChase()
    {

        Vector3 inkyOffset = pacman.position + (pacman.forward * 5);
        Vector3 blinkyToInky = inkyOffset - blinky.position;
        Vector3 inkyTarget = inkyOffset + blinkyToInky;
        agent.SetDestination(inkyTarget);
    }

    void BlinkyChase()
    {
        agent.SetDestination(pacman.position);
    }

    void ClydeChase()
    {
        if (agent == null || pacman == null || bottomLeftCorner == null) return;

        float distanceToPacman = Vector3.Distance(transform.position, pacman.position);

        if (distanceToPacman < distanceThreshold)
        {
            Debug.Log("Clyde is running to the bottom left corner.");
            agent.SetDestination(bottomLeftCorner.position);
        }
        else
        {
            Debug.Log("Clyde is chasing Pac-Man.");
            agent.SetDestination(pacman.position);
        }
    }


    //public GhostType ghostType;

    //public Transform pacman;
    //private NavMeshAgent agent;
    //public Transform blinky;
    //public Transform bottomLeftCorner;

    //private bool isFrightened = false;
    //private float frightenedTime = 0f;
    //public float frightenedSpeed = 3f;
    //private float normalSpeed = 2;

    //public enum GhostType
    //{
    //    Blinky,
    //    Pinky,
    //    Inky,
    //    Clyde
    //}


    //void Start()
    //{
    //    agent = GetComponent<NavMeshAgent>();
    //    agent.speed = normalSpeed;
    //}

    //void Update()
    //{
    //    if (isFrightened)
    //    {
    //        RunAwayFromPacMan();
    //        frightenedTime -= Time.deltaTime;

    //        if (frightenedTime <= 0f)
    //        {
    //            ExitFrightenedState();
    //        }
    //    }
    //    else 
    //    {
    //        Chase();
    //    }


    //    if (!isFrightened)
    //    {
    //        agent.SetDestination(pacman.position);
    //    }
    //}

    //void Chase()
    //{


    //    switch (ghostType)
    //    {
    //        case GhostType.Blinky:
    //            //agent.SetDestination(pacman.position);
    //            break;

    //        case GhostType.Pinky:
    //            //Vector3 pinkyTarget = pacman.position + (pacman.forward * 10);
    //            //agent.SetDestination(pinkyTarget);
    //            break;

    //        case GhostType.Inky:
    //            //Vector3 inkyOffset = pacman.position + (pacman.forward * 5);
    //            //Vector3 blinkyToInky = inkyOffset - blinky.position;
    //            //Vector3 inkyTarget = inkyOffset + blinkyToInky;
    //            //agent.SetDestination(inkyTarget);
    //            break;

    //        case GhostType.Clyde:
    //            if (bottomLeftCorner == null)
    //            {
    //                Debug.LogError("Bottom Left Corner is not assigned for Clyde!");
    //                return;
    //            }

    //            float distanceToPacman = Vector3.Distance(transform.position, pacman.position);
    //            Debug.Log($"Clyde's Distance to Pac-Man: {distanceToPacman}");

    //            if (distanceToPacman < 8)
    //            {
    //                Debug.Log("Clyde is running to the bottom left corner.");
    //                agent.SetDestination(bottomLeftCorner.transform.position);
    //            }
    //            else
    //            {
    //                Debug.Log("Clyde is chasing Pac-Man.");
    //                agent.SetDestination(pacman.transform.position);
    //            }
    //            break;


    //    }
    //}

    //public void StartFrightenedState(float duration)
    //{
    //    isFrightened = true;
    //    frightenedTime = duration;
    //    agent.speed = frightenedSpeed;
    //    Debug.Log("Ghost is now frightened for " + duration + " seconds!");
    //}

    //void RunAwayFromPacMan()
    //{
    //    Vector3 directionAwayFromPacMan = transform.position - pacman.position;
    //    agent.SetDestination(transform.position + directionAwayFromPacMan);
    //}

    //private void ExitFrightenedState()
    //{
    //    isFrightened = false;
    //    agent.speed = normalSpeed;
    //    Debug.Log("Ghost is no longer frightened!");
    //}



}
