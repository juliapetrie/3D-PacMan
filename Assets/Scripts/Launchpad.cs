using System.Collections;
using UnityEngine;

public class Launchpad : MonoBehaviour
{
    [SerializeField] public Transform launchDestination;
    [SerializeField] public float launchHeight = 5f;
    [SerializeField] public float cooldownTime = 2f;
    //private int triggerCount = 0;


    private void OnTriggerEnter(Collider other)
    {
        //triggerCount++;
        //Debug.Log($"Triggered {triggerCount} times at {Time.time}");
        if (other.CompareTag("Pacman"))
        {
            Rigidbody rb = other.GetComponent<Rigidbody>();
            PlayerController playerController = other.GetComponent<PlayerController>();
            Debug.Log("triggered");
            if (rb != null && playerController != null)
            {
                Debug.Log("launched");
                playerController.DisableMovement();

                Vector3 launchVelocity = CalculateLaunchVelocity(other.transform.position, launchDestination.position, launchHeight);
                rb.linearVelocity = Vector3.zero;
                rb.AddForce(launchVelocity, ForceMode.VelocityChange);

                float totalTime = GetTotalFlightTime(launchVelocity.y);
                playerController.Invoke("EnableMovement", totalTime);

                StartCoroutine(ResetCollider());
            }
        }
    }


    private Vector3 CalculateLaunchVelocity(Vector3 start, Vector3 target, float height)
    {
        float gravity = Mathf.Abs(Physics.gravity.y);
        Vector3 displacement = target - start;
        Vector3 horizontalDisplacement = new Vector3(displacement.x, 0, displacement.z);
        float horizontalDistance = horizontalDisplacement.magnitude;
        float verticalDistance = displacement.y;

        float verticalVelocity = Mathf.Sqrt(2 * gravity * height);

        float timeToPeak = verticalVelocity / gravity;
        float timeToFall = Mathf.Sqrt(2 * Mathf.Max(height - verticalDistance, 0) / gravity);
        float totalTime = timeToPeak + timeToFall;

        float horizontalVelocity = horizontalDistance / totalTime;

        Vector3 launchVelocity = horizontalDisplacement.normalized * horizontalVelocity + Vector3.up * verticalVelocity;
        return launchVelocity;
    }

    private float GetTotalFlightTime(float verticalVelocity)
    {
        float gravity = Mathf.Abs(Physics.gravity.y);
        return (2 * verticalVelocity / gravity);
    }

    private IEnumerator ResetCollider()
    {
        Collider col = GetComponent<BoxCollider>();
        col.enabled = false;
        yield return new WaitForSeconds(cooldownTime);
        col.enabled = true;
    }

}