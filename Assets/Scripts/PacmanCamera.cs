using UnityEngine;

public class StablePacmanCamera : MonoBehaviour
{
    public Transform target;

    [SerializeField] private Vector3 offset = new Vector3(0, 10, -8);
    [SerializeField] private float smoothSpeed = 0.1f;
    [SerializeField] private bool lockRotation = true;
    [SerializeField] private float xFollowFactor = 0.05f;


    private Camera cameraComponent;
    private Quaternion initialRotation;

    private void Awake()
    {
        cameraComponent = Camera.main;

        if (target == null)
        {
            GameObject player = GameObject.Find("Player");
            if (player != null)
            {
                target = player.transform;
            }
            else
            {
                Debug.LogWarning("StablePacmanCamera: No player object found!");
            }
        }

        // Store the initial camera rotation
        initialRotation = cameraComponent.transform.rotation;
    }

    private void LateUpdate()
    {
        if (target == null || cameraComponent == null)
            return;

        // Only follow position, not rotation
        Vector3 desiredPosition = target.position + offset;
        // Vector3 smoothedPosition = Vector3.Lerp(cameraComponent.transform.position, desiredPosition, smoothSpeed);
        Vector3 currentPos = cameraComponent.transform.position;
        float newX = Mathf.Lerp(currentPos.x, desiredPosition.x, xFollowFactor);
        float newY = Mathf.Lerp(currentPos.y, desiredPosition.y, smoothSpeed);
        float newZ = Mathf.Lerp(currentPos.z, desiredPosition.z, smoothSpeed);

        cameraComponent.transform.position = new Vector3(newX, newY, newZ);
        // cameraComponent.transform.position = smoothedPosition;

        if (lockRotation)
        {
            // Use fixed rotation instead of LookAt
            cameraComponent.transform.rotation = initialRotation;
        }
        else
        {
            // Usual LookAt behavior (hopefully never activated)
            cameraComponent.transform.LookAt(target);
        }
    }
}