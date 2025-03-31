using UnityEngine;

public class StablePacmanCamera : MonoBehaviour
{
    public Transform target;

    [SerializeField] private Vector3 offset = new Vector3(0, 10, -8);
    [SerializeField] private float smoothSpeed = 0.1f;
    [SerializeField] private bool lockRotation = true;
    [SerializeField] private float xFollowFactor = 0.05f;

    [Header("Z Boundaries")]
    [SerializeField] private float minZ = -10f;
    [SerializeField] private float maxZ = 20f;

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

        initialRotation = cameraComponent.transform.rotation;
    }

    private void LateUpdate()
    {
        if (target == null || cameraComponent == null)
            return;

        Vector3 desiredPosition = target.position + offset;
        Vector3 currentPos = cameraComponent.transform.position;

        float newX = Mathf.Lerp(currentPos.x, desiredPosition.x, xFollowFactor);
        float newY = Mathf.Lerp(currentPos.y, desiredPosition.y, smoothSpeed);
        float newZ = Mathf.Lerp(currentPos.z, desiredPosition.z, smoothSpeed);

        // Clamp Z to stay within map - solves problem of not seeing lower map once you reach the top
        newZ = Mathf.Clamp(newZ, minZ, maxZ);

        cameraComponent.transform.position = new Vector3(newX, newY, newZ);

        if (lockRotation)
        {
            cameraComponent.transform.rotation = initialRotation;
        }
        else
        {
            cameraComponent.transform.LookAt(target);
        }
    }
}


