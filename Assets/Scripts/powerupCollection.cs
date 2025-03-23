using UnityEngine;
using System.Collections;

public class powerupCollection : MonoBehaviour
{
    public float sprintDuration = 2f;
    public float pelletDuration = 8f;
    [SerializeField] private PlayerController playerController;

    private void Start()
    {
        playerController = GetComponent<PlayerController>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.transform.tag == "Fruit")
        {
            Debug.Log("fruit collected");
            Destroy(other.gameObject);
            StartCoroutine(GiveSprintPowerup());
        }

        else if(other.transform.tag == "PowerPellet")
        {
            Debug.Log("power pellet collected");
            Destroy(other.gameObject);
            StartCoroutine(GivePelletPowerup());
        }
    }

    private IEnumerator GivePelletPowerup()
    {
        if (playerController != null)
        {
            playerController.hasPelletPowerup = true;
            yield return new WaitForSeconds(pelletDuration);
            playerController.hasPelletPowerup = false;
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
