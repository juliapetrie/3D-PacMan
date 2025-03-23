using UnityEngine;
using UnityEngine.AI;

public class GhostController : MonoBehaviour
{
    public Transform pacman;
    private NavMeshAgent agent;
    private bool isFrightened = false;
    private float frightenedTime = 0f;
    public float frightenedSpeed = 3f;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>(); 
    }

    void Update()
    {
        if (isFrightened)
        {
            frightenedTime -= Time.deltaTime;

            if (frightenedTime <= 0f)
            {
                isFrightened = false;
            }
            else
            {
                Vector3 directionAwayFromPacMan = transform.position - pacman.position;
                agent.SetDestination(transform.position + directionAwayFromPacMan);
                return;
            }
        }


        if (!isFrightened)
        {
            agent.SetDestination(pacman.position);
        }
    }

    public void StartFrightenedState(float duration)
    {
        isFrightened = true;
        frightenedTime = duration;
        agent.speed = frightenedSpeed;
        Debug.Log("Ghost is now frightened for " + duration + " seconds!");
    }
}
