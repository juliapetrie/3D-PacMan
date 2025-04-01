using UnityEngine;
using System.Collections;

public class Teleport : MonoBehaviour
{
    [SerializeField] public Transform targetLocation;

    private bool canTeleport = true;

    private IEnumerator TeleportCooldown(Collider collider)
    {
        collider.enabled = false;
        canTeleport = false;
        yield return new WaitForSeconds(0.5f);
        canTeleport = true;
        collider.enabled = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Pacman"))
        {
            Vector3 newPosition = new Vector3(targetLocation.position.x, other.transform.position.y, targetLocation.position.z);
            other.transform.position = newPosition;
            StartCoroutine(TeleportCooldown(other));
        }
    }

    private void OnTriggerExit(Collider other)
    {
        Debug.Log("OnTriggerExit");
        if (other.CompareTag("Pacman"))
        {
            StopCoroutine(TeleportCooldown(other));
        }
    }
}
