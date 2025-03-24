using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public abstract class GhostBehaviour : MonoBehaviour
{
    
    protected NavMeshAgent agent;
    protected Transform pacman;
    protected float stateTimer = 0f;
    public float scatterTime = 4f;
    public float chaseTime = 10f;
    protected bool isInScatterMode = true;
   
    public bool isFrightened { get; protected set; } = false;
    public float frightenedTime = 0f;
    public float frightenedSpeed = 3f;
    public float normalSpeed = 2f;

    protected virtual void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        pacman = GameObject.FindGameObjectWithTag("Pacman").transform;
        stateTimer = scatterTime;
    }

    protected virtual void Update()
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

    public void RunAwayFromPacMan(Vector3 directionAwayFromPacMan)
    {
        agent.SetDestination(transform.position + directionAwayFromPacMan);
    }

    protected virtual void SwitchState()
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

    protected abstract void Scatter();
    protected abstract void Chase();


}
