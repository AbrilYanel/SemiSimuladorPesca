using System.Collections.Generic;
using UnityEngine;

public class FirstPersonMovement : MonoBehaviour
{
    public float speed = 5;

    [Header("Running")]
    public bool canRun = true;
    public bool IsRunning { get; private set; }
    public float runSpeed = 9;
    public KeyCode runningKey = KeyCode.LeftShift;

    [Header("Jumping")]
    public float jumpForce = 5f;
    public KeyCode jumpKey = KeyCode.Space;
    public bool isGrounded;
    private bool jumpRequest;

    Rigidbody rigidbody;
    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;

    /// <summary> Functions to override movement speed. Will use the last added override. </summary>
    public List<System.Func<float>> speedOverrides = new List<System.Func<float>>();

    void Awake()
    {
        // Get the rigidbody on this.
        rigidbody = GetComponent<Rigidbody>();
    }

    void Update()
    {
        // Check if the player is on the ground.
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        // Check for jump input.
        if (isGrounded && Input.GetKeyDown(jumpKey))
        {
            jumpRequest = true; // Set jumpRequest to true to handle it in FixedUpdate.
        }
    }

    void FixedUpdate()
    {
        // Update IsRunning from input.
        IsRunning = canRun && Input.GetKey(runningKey);

        // Get targetMovingSpeed.
        float targetMovingSpeed = IsRunning ? runSpeed : speed;
        if (speedOverrides.Count > 0)
        {
            targetMovingSpeed = speedOverrides[speedOverrides.Count - 1]();
        }

        // Get targetVelocity from input.
        Vector2 targetVelocity = new Vector2(Input.GetAxis("Horizontal") * targetMovingSpeed, Input.GetAxis("Vertical") * targetMovingSpeed);

        // Apply movement.
        rigidbody.velocity = transform.rotation * new Vector3(targetVelocity.x, rigidbody.velocity.y, targetVelocity.y);

        // Handle jump.
        if (jumpRequest)
        {
            rigidbody.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            jumpRequest = false; // Reset the jump request after jumping.
        }
    }
}