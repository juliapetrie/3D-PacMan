using System;
using UnityEditor.ShaderGraph;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private InputManager inputManager;
    [SerializeField] private float normalSpeed = 5f;
    [SerializeField] private float sprintSpeed = 10f;
    [SerializeField] private float bufferTime = 0.2f;

    private Rigidbody rb;
    private Vector3 currentDirection = Vector3.zero;
    //private CimenachineCamera camera;

    public bool hasSprintPowerup = false;

    //turn forgiveness input buffer
    private Vector3 bufferedDirection = Vector3.zero;
    private float bufferTimer = 0f;

    private void Awake()
    {
        inputManager.OnMove.AddListener(MovePlayer);
        rb = GetComponent<Rigidbody>();

        rb.freezeRotation = true;
    }

    private void FixedUpdate()
    {
        float speed = hasSprintPowerup ? sprintSpeed : normalSpeed;
        Vector3 movement = currentDirection * speed;

        if (currentDirection != Vector3.zero)
        {
            MoveWithSlide(movement);
            //Vector3 movement = currentDirection * speed * Time.fixedDeltaTime;
            //rb.MovePosition(rb.position + movement);

            bufferTimer = 0f;
        }
        else if (bufferTimer > 0f)
        {
            // Decrease the buffer timer
            bufferTimer -= Time.fixedDeltaTime;

            // Attempt to move using the buffered direction
            Vector3 bufferedMovement = bufferedDirection * speed;// * Time.fixedDeltaTime;
            //rb.MovePosition(rb.position + movement);
            MoveWithSlide(bufferedMovement);
        }
        else
        {
            rb.linearVelocity = Vector3.zero;
        }
    }

    private void MovePlayer(Vector2 direction)
    {
        if(direction != Vector2.zero)
        {
            currentDirection = new Vector3(direction.x, 0f, direction.y).normalized;

            bufferedDirection = currentDirection;
            bufferTimer = bufferTime;
        }

    }

    private void MoveWithSlide(Vector3 desiredVelocity)
    {
        // Calculate movement for this frame
        Vector3 movement = desiredVelocity * Time.fixedDeltaTime;
        Vector3 direction = movement.normalized;
        float distance = movement.magnitude + 0.1f; // Add a small buffer to the distance
        Debug.DrawRay(rb.position, direction * distance, Color.red, 0.1f);
        int layerMask = LayerMask.GetMask("Default");
        // Check for potential collisions
        RaycastHit hit;
        if (Physics.Raycast(rb.position, movement.normalized, out hit, movement.magnitude, layerMask))
        {
            Debug.Log("obstacle detected");
            // Adjust movement direction by sliding along the obstacle
            Vector3 slideDirection = Vector3.ProjectOnPlane(movement, hit.normal);
            rb.MovePosition(rb.position + slideDirection);
        }
        else
        {
            // No obstacle, move normally
            rb.MovePosition(rb.position + movement);
        }
    }
}
