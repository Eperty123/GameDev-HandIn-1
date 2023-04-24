using Assets.Scripts.Carlo;
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
    public float RotationSpeed = 10f;
    public float AnimationDampTime = .1f;
    public float GravityScale = 1.0f;
    public static string MainCameraTag = "MainCamera";
    public static float GlobalGravity = -9.81f; // Unity's default gravity value. See Project Settings -> Physics -> Gravity.

    [Header("Player Components")]
    public Rigidbody Rigidbody;
    public ThirdPersonCameraCD Camera;

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

        Camera = GameObject.FindGameObjectWithTag(MainCameraTag).GetComponent<ThirdPersonCameraCD>();
    }

    private void Update()
    {
        HandleAnimation();
        HandleInput();
        HandleJump();
    }

    private void FixedUpdate()
    {
        // Physics based stuff needs to be in FixedUpdate().
        HandleMovement();
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

    void HandleInput()
    {
        // Y
        float horizontal = Input.GetAxis("Horizontal");
        // X
        float vertical = Input.GetAxis("Vertical");

        movementVector = new Vector3(horizontal, 0, vertical) * MovementSpeed;
        //movementVector = transform.TransformDirection(movementVector);

        // limit the input
        if (movementVector.magnitude > 1f)
            movementVector.Normalize();
    }

    void HandleMovement()
    {

        Rigidbody.velocity = movementVector;
        Rotate(movementVector, RotationSpeed);
        //RotateToDirection(Camera.transform.forward, RotationSpeed);

        // TODO: Fix rotation orientation for movement.
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
        gravityVector = GlobalGravity * GravityScale * Vector3.up;
        Rigidbody.AddForce(gravityVector, ForceMode.Acceleration);
    }

    public virtual void RotateToDirection(Vector3 direction, float rotationSpeed)
    {
        direction.y = 0f;
        Vector3 desiredForward = Vector3.RotateTowards(transform.forward, direction.normalized, rotationSpeed * Time.deltaTime, .1f);
        Quaternion _newRotation = Quaternion.LookRotation(desiredForward);
        transform.rotation = _newRotation;
    }

    public void Rotate(Vector3 direction, float angularSpeed, bool onlyLateral = true)
    {
        if (onlyLateral)
            direction = Vector3.ProjectOnPlane(direction, transform.up);

        if (direction.sqrMagnitude < 0.0001f)
            return;

        var targetRotation = Quaternion.LookRotation(direction, transform.up);
        var newRotation = Quaternion.Slerp(Rigidbody.rotation, targetRotation,
            angularSpeed * Time.deltaTime);

        Rigidbody.MoveRotation(newRotation);
    }
}
