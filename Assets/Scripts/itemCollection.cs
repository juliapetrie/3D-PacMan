// using UnityEngine;
// using UnityEngine.Events;
// using System.Collections;

// public class itemCollection : MonoBehaviour
// {
//     public float sprintDuration = 2f;
//     public float pelletDuration = 8f;
//     public UnityEvent OnPelletCollected = new();
//     [SerializeField] private PlayerController playerController;
//     [SerializeField] private PelletCounterUI pelletCounterUI;

//     private void Start()
//     {
//         playerController = GetComponent<PlayerController>();
//     }

//     private void OnTriggerEnter(Collider other)
//     {

//         //disable the collider to avoid duplicate collisions
//         Collider collider = other.GetComponent<Collider>();
//         if (collider != null)
//         {
//             collider.enabled = false;
//         }

//         if (other.transform.tag == "Pellet")
//         {
//             Debug.Log("pellet collected");
//             Destroy(other.gameObject);
//             OnPelletCollected?.Invoke();
//             pelletCounterUI?.UpdateScore(); // Update score
//         }
//         else if (other.transform.tag == "Fruit")
//         {
//             Debug.Log("fruit collected");
//             Destroy(other.gameObject);
//             StartCoroutine(GiveSprintPowerup());
//         }
//         else if (other.transform.tag == "PowerPellet")
//         {
//             Debug.Log("power pellet collected");
//             Destroy(other.gameObject);
//             StartCoroutine(GivePelletPowerup());
//         }
//     }

//     // private IEnumerator GivePelletPowerup()
//     // {
//     //     if (playerController != null)
//     //     {
//     //         playerController.hasPelletPowerup = true;
//     //         yield return new WaitForSeconds(pelletDuration);
//     //         playerController.hasPelletPowerup = false;
//     //     }
//     // }

// private IEnumerator GivePelletPowerup()
// {
//     if (playerController != null)
//     {
//         playerController.hasPelletPowerup = true;

//         //  Start flashing effect
//         PacmanFlash pacmanFlash = playerController.GetComponent<PacmanFlash>();
//         if (pacmanFlash != null)
//             pacmanFlash.StartFlashing(pelletDuration);

//         Debug.Log("Pac-Man is now INVINCIBLE.");

//         yield return new WaitForSeconds(pelletDuration);

//         playerController.hasPelletPowerup = false;

//         if (pacmanFlash != null)
//             pacmanFlash.StopFlashing();

//         Debug.Log("Pac-Man is vulnerable again.");
//     }
// }

//     private IEnumerator GiveSprintPowerup()
//     {
//         if (playerController != null)
//         {
//             playerController.hasSprintPowerup = true;
//             yield return new WaitForSeconds(sprintDuration);
//             playerController.hasSprintPowerup = false;
//         }
//     }

// }

using UnityEngine;
using UnityEngine.Events;
using System.Collections;

public class itemCollection : MonoBehaviour
{
    public float sprintDuration = 2f;
    public float pelletDuration = 8f;
    public UnityEvent OnPelletCollected = new();
    
    [SerializeField] private PlayerController playerController;
    [SerializeField] private PelletCounterUI pelletCounterUI;
    [SerializeField] private GhostManager ghostManager;


    private void Start()
    {
        playerController = GetComponent<PlayerController>();
    }

    private void OnTriggerEnter(Collider other)
    {
        Collider collider = other.GetComponent<Collider>();
        if (collider != null)
        {
            collider.enabled = false;
        }

        if (other.CompareTag("Pellet"))
        {
            Debug.Log("pellet collected");
            Destroy(other.gameObject);
            OnPelletCollected?.Invoke();
            pelletCounterUI?.UpdateScore();
        }
        else if (other.CompareTag("Fruit"))
        {
            Debug.Log("fruit collected");
            Destroy(other.gameObject);
            StartCoroutine(GiveSprintPowerup());
        }
        else if (other.CompareTag("PowerPellet"))
        {
            Debug.Log("power pellet collected");
            Destroy(other.gameObject);
            if (ghostManager != null)
            {
                Debug.Log("ghost manager not null");
                ghostManager.FrightenAllGhosts(pelletDuration);
            }
            StartCoroutine(GivePelletPowerup());
        }
    }

    private IEnumerator GivePelletPowerup()
    {
        if (playerController != null)
        {
            playerController.hasPelletPowerup = true;

            //  flashing effect
            PacmanFlashingEffect flash = playerController.GetComponent<PacmanFlashingEffect>();
            if (flash != null)
                flash.StartFlashing(pelletDuration);

            Debug.Log("Pacman invincible.");

            yield return new WaitForSeconds(pelletDuration);

            playerController.hasPelletPowerup = false;

            if (flash != null)
                flash.StopFlashing();

            Debug.Log("pacman back to normal powers");
        }
    }

    private IEnumerator GiveSprintPowerup()
    {
        if (playerController != null)
        {
            playerController.hasSprintPowerup = true;
            yield return new WaitForSeconds(sprintDuration);
            playerController.hasSprintPowerup = false;
        }
    }
}

