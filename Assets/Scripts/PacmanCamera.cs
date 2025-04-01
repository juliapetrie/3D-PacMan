using UnityEngine;
public class StablePacmanCamera : MonoBehaviour
{
    public Transform target;
    [SerializeField] private Vector3 offset = new Vector3(0, 10, -8);
    [SerializeField] private float viewportPosY = 0.6f;
    [SerializeField] private float smoothSpeed = 0.1f;
    [SerializeField] private bool lockRotation = true;
    [SerializeField] private float xFollowFactor = 0.05f;
    [Header("Z Boundaries")]
    [SerializeField] private float minZ = -10f;
    [SerializeField] private float maxZ = 20f;
    private Vector3 offsetAdjusted;
    private Camera cameraComponent;
    private Quaternion initialRotation;
    private void Awake()
    {

        offset = new Vector3(0, 15, 0);
        viewportPosY = 0.6f;
        minZ = -8f;
        maxZ = 20f;
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
        // Calculate initial position based on viewport position
        CalculateOffsetAdjustment();
    }
    private void CalculateOffsetAdjustment()
    {
        if (target == null || cameraComponent == null)
            return;
        offsetAdjusted = offset;
        // Apply the camera's position and rotation
        Vector3 testPosition = target.position + offset;
        Quaternion testRotation = lockRotation ? initialRotation : Quaternion.LookRotation(target.position - testPosition);
        // Create a temporary camera to calculate viewport position
        GameObject tempGO = new GameObject("TempCameraCalculator");
        tempGO.transform.position = testPosition;
        tempGO.transform.rotation = testRotation;
        Camera tempCam = tempGO.AddComponent<Camera>();
        tempCam.fieldOfView = cameraComponent.fieldOfView;
        // Calculate where target appears in viewport
        Vector3 viewportPoint = tempCam.WorldToViewportPoint(target.position);
        // Calculate adjustment needed 
        float currentViewportY = viewportPoint.y;
        float viewportAdjustment = viewportPosY - currentViewportY;
        // Convert viewport adjustment to world space adjustment
        offsetAdjusted.z -= viewportAdjustment * 10f;
        // Clean up
        GameObject.DestroyImmediate(tempGO);
    }
    private void LateUpdate()
    {
        if (target == null || cameraComponent == null)
            return;
        // Use the adjusted offset for camera positioning
        Vector3 desiredPosition = target.position + offsetAdjusted;
        Vector3 currentPos = cameraComponent.transform.position;
        float newX = Mathf.Lerp(currentPos.x, desiredPosition.x, xFollowFactor);
        float newY = Mathf.Lerp(currentPos.y, desiredPosition.y, smoothSpeed);
        float newZ = Mathf.Lerp(currentPos.z, desiredPosition.z, smoothSpeed);
        // Clamp Z to stay within map
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
    // Allow recalculating the offset
    public void RecalculateOffset()
    {
        CalculateOffsetAdjustment();
    }
}