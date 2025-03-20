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
    private Vector3 initialDirection = Vector3.zero;
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
        //initialDirection = transform.forward;

        float speed = hasSprintPowerup ? sprintSpeed : normalSpeed;
        Vector3 movement = currentDirection * speed;
        Vector3 initialMovement = initialDirection * speed;

        if (currentDirection != Vector3.zero) //move the user normally
        {
            MoveWithRaycast(movement, initialMovement);
            bufferTimer = 0f;
        }
        else if (bufferTimer > 0f) //try to move user with the buffered direction
        {
            bufferTimer -= Time.fixedDeltaTime;
            Vector3 bufferedMovement = bufferedDirection * speed;
            MoveWithRaycast(bufferedMovement, initialMovement);
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

            initialDirection = transform.forward.normalized;
            bufferedDirection = currentDirection;
            bufferTimer = bufferTime;
        }

    }

    private void MoveWithRaycast(Vector3 desiredVelocity, Vector3 initialVelocity)
    {
        /*
         * get intended direction using desiredVelocity
         * get distance of the raycast
         * shoot a raycast, detect wall
         * if wall: keep moving in players initial direction
         * if no wall: move in players intended direction
         */
        Vector3 desiredDirection = desiredVelocity.normalized;
        float distance = desiredVelocity.magnitude * Time.fixedDeltaTime + 0.5f;

        Debug.DrawRay(rb.position, desiredDirection * distance, Color.red, 10f);

        RaycastHit hit;
        if (Physics.Raycast(rb.position, desiredDirection, out hit, distance))
        {
            //obstacle
            Debug.Log("Obstacle detected");
            Vector3 movement = initialVelocity * Time.fixedDeltaTime;
            rb.MovePosition(rb.position + movement);
        }
        else
        {
            //no obstacle
            //Debug.Log("No obstacle");
            Vector3 movement = desiredVelocity * Time.fixedDeltaTime;
            rb.MovePosition(rb.position + movement);
            transform.rotation = Quaternion.LookRotation(currentDirection);

        }
    }
}
