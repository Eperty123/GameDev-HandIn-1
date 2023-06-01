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
    public float RotationDampingSpeed = .1f;
    public float AnimationDampTime = .1f;
    public float GravityScale = 1.0f;
    public static string MainCameraTag = "MainCamera";
    public static float GlobalGravity = -9.81f; // Unity's default gravity value. See Project Settings -> Physics -> Gravity.

    [Header("Player Components")]
    public Rigidbody Rigidbody;
    public Transform Camera;

    Vector3 movementVector;
    Vector3 gravityVector;
    float turnSmoothVector;
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

        Camera = GameObject.FindGameObjectWithTag(MainCameraTag).transform;
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
            animator.SetFloat("Forward", movementVector.magnitude, AnimationDampTime, Time.deltaTime);
            animator.SetFloat("Sideward", Mathf.Atan2(movementVector.x, movementVector.z), AnimationDampTime, Time.deltaTime);
        }
    }

    void HandleInput()
    {
        // Y
        float horizontal = Input.GetAxis("Horizontal");
        // X
        float vertical = Input.GetAxis("Vertical");

        movementVector = new Vector3(horizontal, 0, vertical).normalized;
    }

    void HandleMovement()
    {
        if (movementVector.magnitude > .1f)
        {
            float targetAngle = Mathf.Atan2(movementVector.x, movementVector.z) * Mathf.Rad2Deg + Camera.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVector, RotationDampingSpeed);

            var rotation = Quaternion.Euler(0f, angle, 0f);
            var moveDirection = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            Rigidbody.rotation = rotation;
            Rigidbody.velocity = moveDirection * MovementSpeed;
        }
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
}
