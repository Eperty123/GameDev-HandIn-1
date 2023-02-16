using UnityEngine;

/*
 * References
 * https://forum.unity.com/threads/why-does-rigidbody-3d-not-have-a-gravity-scale.440415/
 */

public class MovementScriptCD : MonoBehaviour
{
    [Header("Player Options")]
    public bool AllowJump = true;
    public float JumpStrength = 5f;
    public float MovementSpeed = 10f;
    public float gravityScale = 1.0f;
    public static float globalGravity = -9.81f;
    public Rigidbody Rigidbody;

    Vector3 movementVector;
    Vector3 gravityVector;

    private void Start()
    {
        Rigidbody = GetComponent<Rigidbody>();
        Setup();
    }

    private void Setup()
    {
        if (Rigidbody != null)
        {
            Rigidbody.freezeRotation = true;
            Rigidbody.useGravity = false;
        }
    }

    private void Update()
    {
        HandleMovement();
        HandleJump();
    }

    private void FixedUpdate()
    {
        HandleGravity();
    }

    void HandleMovement()
    {
        // Y
        float horizontal = Input.GetAxis("Horizontal");
        // X
        float vertical = Input.GetAxis("Vertical");

        movementVector = new Vector3(horizontal, 0, vertical) * MovementSpeed;
        Rigidbody.velocity = movementVector;
    }

    void HandleJump()
    {
        if (Input.GetButton("Jump"))
            Rigidbody.AddForce(Vector3.up * JumpStrength, ForceMode.Impulse);
    }

    void HandleGravity()
    {
        gravityVector = globalGravity * gravityScale * Vector3.up;
        Rigidbody.AddForce(gravityVector, ForceMode.Acceleration);
    }
}
