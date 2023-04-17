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
    public float RotationSpeed = 75f;
    public float AnimationDampTime = .1f;
    public float gravityScale = 1.0f;
    public static float globalGravity = -9.81f; // Unity's default gravity value. See Project Settings -> Physics -> Gravity.
    public Rigidbody Rigidbody;

    Vector3 movementVector;
    Vector3 gravityVector;
    Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
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
        HandleAnimation();
        HandleMovement();
        HandleJump();
    }

    private void FixedUpdate()
    {
        // Physics based stuff needs to be in FixedUpdate().
        HandleGravity();
    }

    void HandleAnimation()
    {
        if (animator != null)
        {
            animator.SetFloat("Forward", movementVector.z, AnimationDampTime, Time.deltaTime);
            animator.SetFloat("Sideward", movementVector.x, AnimationDampTime, Time.deltaTime);
        }
    }

    void HandleMovement()
    {
        // Y
        float horizontal = Input.GetAxis("Horizontal");
        // X
        float vertical = Input.GetAxis("Vertical");

        movementVector = new Vector3(horizontal, 0, vertical) * MovementSpeed;
        movementVector = transform.TransformDirection(movementVector);

        Rigidbody.velocity = movementVector;
        transform.Rotate(0, horizontal * RotationSpeed, 0);

        // TODO: Fix forward + backward inputs reversed when moving.
    }

    void HandleJump()
    {
        if (Input.GetButton("Jump"))
            Rigidbody.AddForce(Vector3.up * JumpStrength, ForceMode.Impulse);
    }

    void HandleGravity()
    {
        // Handle gravity manually. We will add constant force of 
        // the global gravity along with the gravity scale to simulate it.
        // Unity's gravity value can be found under Project Settings -> Physics -> Gravity.
        gravityVector = globalGravity * gravityScale * Vector3.up;
        Rigidbody.AddForce(gravityVector, ForceMode.Acceleration);
    }
}
