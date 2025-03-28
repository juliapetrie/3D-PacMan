using UnityEngine;

public class Launchpad : MonoBehaviour
{    

    [SerializeField] public Transform launchDestination;
    [SerializeField] public float launchHeight = 5f;
  
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("triggered");
        Debug.Log(other.transform.tag);
        if (other.transform.tag == "Pacman")
        {
            Debug.Log("launch");
            Rigidbody rb = other.GetComponent<Rigidbody>();
            PlayerController playerController = other.GetComponent<PlayerController>();

            if (rb != null && playerController != null)
            {
                playerController.DisableMovement();

                Vector3 launchVelocity = CalculateLaunchVelocity(other.transform.position, launchDestination.position, launchHeight);
                rb.linearVelocity = Vector3.zero;
                rb.AddForce(launchVelocity, ForceMode.VelocityChange);

                playerController.Invoke("EnableMovement", Mathf.Abs(2 * launchVelocity.y / Physics.gravity.y));
            }
        }
    }


    private Vector3 CalculateLaunchVelocity(Vector3 start, Vector3 target, float height)
    {
        float gravity = Mathf.Abs(Physics.gravity.y);

        //calculate the distance between the two points
        Vector3 displacement = target - start;
        Vector3 horizontalDisplacement = new Vector3(displacement.x, 0, displacement.z);
        float horizontalDistance = horizontalDisplacement.magnitude;
        float verticalDistance = displacement.y;

        //apply equation to calculate time
        float timeToPeak = Mathf.Sqrt(2 * height / gravity);
        float timeFromPeakToTarget = Mathf.Sqrt(2 * (height - verticalDistance) / gravity);
        float totalTime = timeToPeak + timeFromPeakToTarget;

        //find the velocity required
        float horizontalVelocity = horizontalDistance / totalTime;
        float verticalVelocity = Mathf.Sqrt(2 * gravity * height);

        //return the velocity as a vector
        Vector3 launchVelocity = horizontalDisplacement.normalized * horizontalVelocity + Vector3.up * verticalVelocity;
        return launchVelocity;
    }
}