using UnityEngine;
using System.Collections;

public class fruitCollection : MonoBehaviour
{
    private int fruit = 0;
    public float sprintDuration = 2f;
    [SerializeField] private PlayerController playerController;

    private void Start()
    {
        playerController = GetComponent<PlayerController>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.transform.tag == "Fruit")
        {
            fruit++;
            Debug.Log(fruit);
            Destroy(other.gameObject);
            StartCoroutine(GiveSprintPowerup());
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
