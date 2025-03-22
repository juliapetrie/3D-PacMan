using UnityEditor.ShaderGraph;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private InputManager inputManager;
    [SerializeField] private float maxSpeed;

    private Rigidbody rb;
    //private CimenachineCamera camera; 

    private void Awake()
    {
        inputManager.OnMove.AddListener(MovePlayer);
        rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        Vector3 velocity = rb.linearVelocity;
        Vector2 horizontalVelocity = new Vector2(velocity.x, velocity.z);

        rb.linearVelocity = new Vector3(horizontalVelocity.x, velocity.y, horizontalVelocity.y);
    }

    private void MovePlayer(Vector2 dirn)
    {
        Vector3 direction = new Vector3(dirn.x, 0f, dirn.y);

        rb.linearVelocity = direction * maxSpeed;
     

    }
}
