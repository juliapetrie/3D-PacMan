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
    private float bufferTimer = 0f;
    public bool hasSprintPowerup = false;

    private Vector3 currentDirection = Vector3.zero; //current movement direction
    private Vector3 bufferedDirection = Vector3.zero; //turn forgiveness input buffer (intended direction)
    private Vector3 initialDirection = Vector3.zero; //initial direction of the player

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
            bufferedDirection = currentDirection;
            //bufferTimer = 0f;
        }
        else if (bufferTimer > 0f) //try to move user with the buffered direction
        {
            //bufferTimer -= Time.fixedDeltaTime;
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

        BoxCollider boxCollider = GetComponentInChildren<BoxCollider>();
        if (boxCollider == null) return;

        Vector3 boundsExtents = boxCollider.bounds.extents;

        Vector3 perpendicular = new Vector3(desiredDirection.z, 0f, -desiredDirection.x).normalized;

        Vector3 leftEdge = rb.position - perpendicular * (boundsExtents.x - 0.01f);
        Vector3 rightEdge = rb.position + perpendicular * (boundsExtents.x - 0.01f);

        //Debug.DrawRay(rb.position, desiredDirection * distance, Color.red, 10f);
        Debug.DrawRay(leftEdge, desiredDirection * distance, Color.red, 10f);
        Debug.DrawRay(rightEdge, desiredDirection * distance, Color.red, 10f);

        RaycastHit hit;
        bool leftHit = Physics.Raycast(leftEdge, desiredDirection, out hit, distance);
        bool rightHit = Physics.Raycast(rightEdge, desiredDirection, out hit, distance);
        bool centerHit = Physics.Raycast(rb.position, desiredDirection, out hit, distance);

        if (leftHit || rightHit || centerHit)
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
            bufferedDirection = Vector3.zero;
            currentDirection = transform.forward;
        }
    }
}
