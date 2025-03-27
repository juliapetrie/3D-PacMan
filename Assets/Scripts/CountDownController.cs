using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.AI;
using TMPro; 

public class CountdownController : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI countdownText; 
    [SerializeField] private GameObject countdownPanel; 
    [SerializeField] private float countdownDuration; 
     [SerializeField] private GameObject player; //setup to pacman in the inspector
     [SerializeField] private GameObject[] ghosts; // setup to ghost parent obj in inspector

    private bool isLevel1;
    
    private void Start()
    {
        string currentScene = SceneManager.GetActiveScene().name;
        isLevel1 = (currentScene == "Level 1"); 
         DisableGameplay();

        if (!isLevel1) // if not level 1 start countdown immediatly
        {
            StartCoroutine(StartCountdown());
        }
    }

    //if it is level 1 then start count down only after tutorial panel closes
    public void DelayedCountDown()
    {
        StartCoroutine(StartCountdown());
    }

    private IEnumerator StartCountdown()
    {
        if (countdownPanel != null)
            countdownPanel.SetActive(true);

         countdownText.text = "3";
        yield return new WaitForSeconds(countdownDuration);

        countdownText.text = "2";
        yield return new WaitForSeconds(countdownDuration);

        countdownText.text = "1";
        yield return new WaitForSeconds(countdownDuration);

        if (countdownPanel != null)
            countdownPanel.SetActive(false);

        EnableGameplay(); //game doesnt start until countdown is complete

        Debug.Log("Countdown finished");
    }
private void DisableGameplay()
{
    if (player != null)
    {
        player.SetActive(false); // player disabled
        Debug.Log("player paused");
    }

    if (ghosts != null)
    {
        foreach (GameObject ghost in ghosts)
        {
            if (ghost != null)
            {
                NavMeshAgent agent = ghost.GetComponent<NavMeshAgent>();
                if (agent != null)
                {
                    agent.isStopped = true; // ghosts stop but navmesh still active
                }
            }
        }
        Debug.Log("ghosts paused");
    }
}

private void EnableGameplay()
{
    if (player != null)
    {
        player.SetActive(true); //player un-paused
        Debug.Log("player active");
    }

    if (ghosts != null)
    {
        foreach (GameObject ghost in ghosts)
        {
            if (ghost != null)
            {
                NavMeshAgent agent = ghost.GetComponent<NavMeshAgent>();
                if (agent != null)
                {
                    agent.isStopped = false; // ghosts active
                }
            }
        }
        Debug.Log("ghosts active");
    }
}



}
