using System;
using UnityEditor.ShaderGraph;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private InputManager inputManager;
    [SerializeField] private float normalSpeed = 5f;
    [SerializeField] private float sprintSpeed = 10f;
    [SerializeField] private float pelletSpeed = 7f;

    private Rigidbody rb;
    public bool hasSprintPowerup = false;
    public bool hasPelletPowerup = false;
    private bool canMove = true;

    private Vector3 intendedDirection = Vector3.zero; //intended movement direction
    private Vector3 initialDirection = Vector3.zero; //initial direction of the player object

    private void Awake()
    {
        inputManager.OnMove.AddListener(MovePlayer);
        rb = GetComponent<Rigidbody>();

        rb.freezeRotation = true;
    }

    private void FixedUpdate()
    {
        if (canMove)
        {
            float speed = normalSpeed;
            if (hasPelletPowerup) speed = pelletSpeed;
            else if (hasSprintPowerup) speed = sprintSpeed;

            Vector3 movement = intendedDirection * speed;
            Vector3 initialMovement = initialDirection * speed;

            if (intendedDirection != Vector3.zero)
            {
                MoveWithRaycast(movement, initialMovement);
            }
            else
            {
                rb.linearVelocity = Vector3.zero;
            }
        }
    }

    public void DisableMovement()
    {
        canMove = false;
        if(!canMove)
        {
            rb.linearVelocity = Vector3.zero;
        }
    }

    public void EnableMovement()
    {
        canMove = true;
    }

    private void MovePlayer(Vector2 direction)
    {
        if(direction != Vector2.zero)
        {
            intendedDirection = new Vector3(direction.x, 0f, direction.y).normalized;

            initialDirection = transform.forward.normalized; //store initial direction
            //bufferedDirection = currentDirection; //store intended direction
        }

    }

    private void MoveWithRaycast(Vector3 desiredVelocity, Vector3 initialVelocity)
    {
        /*
         * get intended direction using desiredVelocity
         * get distance of the raycast
         * shoot a raycast, detect wall
         * if wall: keep moving in players initial direction
         * if no wall: move in players intended direction, reset direction buffer.
         */
        Vector3 desiredDirection = desiredVelocity.normalized;
        float distance = desiredVelocity.magnitude * Time.fixedDeltaTime + 0.5f;

        BoxCollider boxCollider = GetComponentInChildren<BoxCollider>();
        if (boxCollider == null) return;

        //make sure the rays are being cast perpendicular to the characters rotation
        Vector3 perpendicular = new Vector3(desiredDirection.z, 0f, -desiredDirection.x).normalized;

        //set rays to the left and right of the hitbox, but 1 ish pixel in to avoid side walls
        Vector3 boundsExtents = boxCollider.bounds.extents;
        Vector3 leftEdge = rb.position - perpendicular * (boundsExtents.x - 0.01f);
        Vector3 rightEdge = rb.position + perpendicular * (boundsExtents.x - 0.01f);

        Debug.DrawRay(leftEdge, desiredDirection * distance, Color.red, 10f);
        Debug.DrawRay(rightEdge, desiredDirection * distance, Color.red, 10f);

        RaycastHit hit;
        bool leftHit = Physics.Raycast(leftEdge, desiredDirection, out hit, distance);
        bool rightHit = Physics.Raycast(rightEdge, desiredDirection, out hit, distance);

        if (leftHit || rightHit)
        {
            //obstacle
            Debug.Log("Obstacle detected");
            Vector3 movement = initialVelocity * Time.fixedDeltaTime;
            rb.MovePosition(rb.position + movement);
        }
        else
        {
            //no obstacle
            Vector3 movement = desiredVelocity * Time.fixedDeltaTime;
            rb.MovePosition(rb.position + movement);
            transform.rotation = Quaternion.LookRotation(intendedDirection);
            intendedDirection = transform.forward;
        }           
    }
}
