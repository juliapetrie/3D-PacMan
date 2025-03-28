

using UnityEngine;
using UnityEngine.AI;
using System.Collections;



public class GhostController : MonoBehaviour
{

    public Transform blinky;
    public Transform pacman;
    public Transform bottomLeftCorner;
    private NavMeshAgent agent;
    private float distanceThreshold = 2f;

    private bool isReturningHome;

    [SerializeField] private Transform ClydeScatterTarget;
    [SerializeField] private Transform BlinkyScatterTarget;
    [SerializeField] private Transform InkyScatterTarget;
    [SerializeField] private Transform PinkyScatterTarget;

    [SerializeField] private Transform homePosition;
    [SerializeField] private float respawnTime = 5f;


    private bool isFrightened = false;
    private float frightenedTime = 0f;
    public float frightenedSpeed = 4f;
    private float eatenSpeed = 5f;
    private float normalSpeed = 3f;

    private bool isInScatterMode = true;
    private float stateTimer = 0f;
    public float scatterTime = 4f;
    public float chaseTime = 10f;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        stateTimer = scatterTime;
        if (agent == null)
        {
            Debug.LogError("NavMeshAgent not found on " + gameObject.name);
            return;
        }
    }

    void Update()
    {
        if (isReturningHome)
        {
            return;
        }
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
            HandleState();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (isFrightened && other.CompareTag("Pacman"))
        {
            Debug.Log("Pac-Man caught the ghost!");
            returnHome();
        }
    }


    public void returnHome()
    {
        isReturningHome = true;
        //add the ui (only eyes to be seen when travelling home)
        agent.speed = eatenSpeed;
        agent.SetDestination(homePosition.position);
        StartCoroutine(RespawnAfterDelay());

    }

    private IEnumerator RespawnAfterDelay()
    {
        yield return new WaitForSeconds(respawnTime);
        isReturningHome = false;
        ExitFrightenedState();
    }

    private void HandleState()
    {
        stateTimer -= Time.deltaTime;

        if (stateTimer <= 0f)
        {

            SwitchState();
        }
        if (isInScatterMode)
        {
            Scatter();
        }
        else
        {
            Chase();
        }
    }

    private void SwitchState()
    {
        isInScatterMode = !isInScatterMode;

        if (isInScatterMode)
        {
            stateTimer = scatterTime;
        }
        else
        {
            stateTimer = chaseTime;

        }
    }

    private void Scatter()
    {
        if (agent.name == "Clyde")
        {
            agent.SetDestination(ClydeScatterTarget.position);
        }
        if (agent.name == "Blinky")
        {
            agent.SetDestination(BlinkyScatterTarget.position);
            Debug.Log("Blinky is in scatter mode");
        }
        if (agent.name == "Blinky")
        {
            if (agent.isActiveAndEnabled && agent.isOnNavMesh && agent.CalculatePath(BlinkyScatterTarget.position, new NavMeshPath()))
            {
                agent.SetDestination(BlinkyScatterTarget.position);
                Debug.Log("Blinky is in scatter mode");
            }
            else
            {
                Debug.LogWarning("No valid path found for Blinky to Scatter Target.");
            }
        }
        if (agent.name == "Pinky")
        {
            agent.SetDestination(PinkyScatterTarget.position);
        }
        if (agent.name == "Inky")
        {
            if (agent.isActiveAndEnabled && agent.isOnNavMesh && agent.CalculatePath(InkyScatterTarget.position, new NavMeshPath()))
            {
                agent.SetDestination(InkyScatterTarget.position);
                Debug.Log("inky is in scatter mode");
            }
            else
            {
                Debug.LogWarning("No valid path found for inky to Scatter Target.");
            }
            
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
        Vector3 pinkyTarget = pacman.position + (pacman.forward * 4);
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
        Debug.Log("Blinky is in chase mode");


    }

    void ClydeChase()
    {
        if (agent == null || pacman == null || bottomLeftCorner == null) return;

        float distanceToPacman = Vector3.Distance(transform.position, pacman.position);

        if (distanceToPacman < distanceThreshold)
        {
            agent.SetDestination(bottomLeftCorner.position);
        }
        else
        {
            agent.SetDestination(pacman.position);
        }
    }



}
