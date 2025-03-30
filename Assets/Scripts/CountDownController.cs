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
     [SerializeField] private GameObject[] ghosts; // setup to ghosts in inspector
     [SerializeField] private GameObject pauseButton; //setup 
    [SerializeField] private GameObject playButton;//setup


    private bool isLevel1;
    
    private void Start()
    {
        string currentScene = SceneManager.GetActiveScene().name;
        isLevel1 = (currentScene == "Merged Level 1 V1"); 
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
public void DisableGameplay()
{
     if (player != null)
    {
        // pause pacman
        var controller = player.GetComponent<PlayerController>();
        if (controller != null)
        {
            controller.enabled = false;
        }

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

public void PauseGameUI()
{
    DisableGameplay();

    if (pauseButton != null) pauseButton.SetActive(false);
    if (playButton != null) playButton.SetActive(true);
}


public void EnableGameplay()
{
//play pacman
     if (player != null)
    {
        var controller = player.GetComponent<PlayerController>();
        if (controller != null)
        {
            controller.enabled = true;
        }

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
public void ResumeGameUI()
{
    EnableGameplay();

    if (pauseButton != null) pauseButton.SetActive(true);
    if (playButton != null) playButton.SetActive(false);
}




}
