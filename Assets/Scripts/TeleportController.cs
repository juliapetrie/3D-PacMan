using UnityEngine;
using System.Collections;

public class TeleportController : MonoBehaviour
{
    [SerializeField] public Transform leftWall;
    [SerializeField] public Transform rightWall;


    private bool canTeleport = true;
    private Collider playerCollider; // Declare the playerCollider variable

    void Start()
    {
        playerCollider = GetComponent<Collider>(); // Initialize playerCollider
    }

    private IEnumerator TeleportCooldown(Collider other)
    {
        canTeleport = false;
        playerCollider.enabled = false; // Disable collider
        other.enabled = false;
        yield return new WaitForSeconds(0.25f); // Wait briefly
        playerCollider.enabled = true; // Re-enable collider
        other.enabled = true;
        canTeleport = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("triggered");
        if (canTeleport && other.CompareTag("leftTeleportWall"))
        {
            Vector3 newPosition = new Vector3(rightWall.position.x - 0.8f, transform.position.y, rightWall.position.z);
            transform.position = newPosition;
            StartCoroutine(TeleportCooldown(other));
        }else if (canTeleport && other.CompareTag("rightTeleportWall"))
        {
            Vector3 newPosition = new Vector3(leftWall.position.x + 0.8f, transform.position.y, leftWall.position.z);
            transform.position = newPosition;
            StartCoroutine(TeleportCooldown(other));
        }
    }
}
