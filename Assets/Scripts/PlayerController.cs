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
    private Vector3 currentDirection = Vector3.zero; //current movement direction
    private Vector3 bufferedDirection = Vector3.zero; //turn forgiveness input buffer (intended direction)
    
    private float bufferTimer = 0f;
    public bool hasSprintPowerup = false;
    //private CimenachineCamera camera;


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

            bufferTimer = 0f;
        }
        else if (bufferTimer > 0f)
        {

            bufferTimer -= Time.fixedDeltaTime;

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
        Vector3 direction = desiredVelocity.normalized;
        float distance = desiredVelocity.magnitude * Time.fixedDeltaTime + 0.5f;

        Debug.DrawRay(rb.position, direction * distance, Color.red, 10f);

        RaycastHit hit;
        if (Physics.Raycast(rb.position, direction, out hit, distance))
        {
            Debug.Log("Obstacle detected");

            Vector3 slideDirection = Vector3.ProjectOnPlane(desiredVelocity, hit.normal).normalized;
            Vector3 slideMovement = slideDirection * desiredVelocity.magnitude * Time.fixedDeltaTime;
        }
        else
        {
            Debug.Log("No obstacle");
            Vector3 movement = desiredVelocity * Time.fixedDeltaTime;
            rb.MovePosition(rb.position + movement);
        }
    }
}
