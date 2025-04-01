using UnityEngine;
using UnityEngine.AI;
using System.Collections;

// This handles switching from chase to scatter, the frightented behaviour, navigation with NavMash, stuck detection, and destination validation
public abstract class GhostBehaviour : MonoBehaviour
{
    protected NavMeshAgent agent;
    protected Transform pacman;

    // Timer for counting down how long until state switch
    protected float stateTimer = 0f;

    // Duration for which the ghost stays in scatter mode.
    public float scatterTime = 4f;

    // Duration for which the ghost stays in chase mode.
    public float chaseTime = 10f;

    protected bool isInScatterMode = true;

    // Chat insisted I make this read only outside of this class or it's children when I asked it to review, so I guess sure?
    public bool isFrightened { get; protected set; } = false;

    // Initially not frightened
    public float frightenedTime = 0f;

    // Speed at which ghost moves when frightened.
    public float frightenedSpeed = 3f;

    // Normal movement speed for the ghost.
    public float normalSpeed = 2f;

    // Timer for checking if stuck
    private float stuckTimer = 0f;

    // Record position from previous check
    private Vector3 lastPos;

    protected virtual void Start()
    {

        agent = GetComponent<NavMeshAgent>();
        pacman = GameObject.FindGameObjectWithTag("Pacman").transform;

        // Initialize timer
        stateTimer = scatterTime;

        // Use built in method (idk how it works)
        agent.obstacleAvoidanceType = ObstacleAvoidanceType.HighQualityObstacleAvoidance;

        // 0.3 meter radius (based on level dimensions ~23x23 , lane width 1) 
        agent.radius = 0.3f;
    }

    protected virtual void Update()
    {
        // Decrease state timer by elapsed time since last frame.
        stateTimer -= Time.deltaTime;

        // When the timer runs out, switch the ghost's state
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

        // If it doesn't move for too long, assume it's stuck
        stuckTimer += Time.deltaTime;
        if (stuckTimer > 1f)  // Check second
        {
            // Compare current position with the last recorded position, if the difference is less than 0.1 units, probably stuck
            if (Vector3.Distance(transform.position, lastPos) < 0.1f)
            {
                // Tries to find nearby valid point within radius of 2
                if (NavMesh.SamplePosition(transform.position + Random.insideUnitSphere * 2f,
                                           out NavMeshHit hit, 2f, NavMesh.AllAreas))
                {
                    // Set the new destination
                    agent.SetDestination(hit.position);

                    // Debug
                    Debug.Log($"{gameObject.name} was stuck and got a new destination.");
                }
            }
            // Update lastPos
            lastPos = transform.position;
            // Reset timer.
            stuckTimer = 0f;
        }
    }

    // Self explanatory
    public void StartFrightenedState(float duration)
    {
        isFrightened = true;
        frightenedTime = duration;
        agent.speed = frightenedSpeed;
        Debug.Log("Ghost is now frightened for " + duration + " seconds!");
    }

    public void ExitFrightenedState()
    {
        isFrightened = false;
        agent.speed = normalSpeed;
        Debug.Log("Ghost is no longer frightened!");
    }

    // Sends ghost to home position
    public void returnHome(Vector3 homePosition, float respawnTime)
    {
        agent.speed = 5f;
        agent.SetDestination(homePosition);
        StartCoroutine(RespawnAfterDelay(respawnTime));
    }

    protected IEnumerator RespawnAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        ExitFrightenedState();
    }

    // Run away in direction indicated
    public void RunAwayFromPacMan(Vector3 directionAwayFromPacMan)
    {
        // Add direction vector to the current position.
        agent.SetDestination(transform.position + directionAwayFromPacMan);
    }

    protected virtual void SwitchState()
    {
        isInScatterMode = !isInScatterMode;
        // Reset timer based on state
        stateTimer = isInScatterMode ? scatterTime : chaseTime;
    }

    // Tries to find path. If unsuccessful, attempts to find random nearby point
    protected void SetValidatedDestination(Vector3 targetPos)
    {
        NavMeshPath path = new NavMeshPath();
        // Uses path only if it is complete
        if (agent.CalculatePath(targetPos, path) && path.status == NavMeshPathStatus.PathComplete)
        {
            agent.SetDestination(targetPos);
        }
        else
        {
            // If it's not complete, find nearby point
            if (NavMesh.SamplePosition(targetPos, out NavMeshHit hit, 2f, NavMesh.AllAreas))
            {
                agent.SetDestination(hit.position);
            }
        }
    }
    // Abstract method for scatter (each ghost implements own behaviour)
    protected abstract void Scatter();

    // Abstract method for chase (each ghost implements own behaviour)
    protected abstract void Chase();
}
