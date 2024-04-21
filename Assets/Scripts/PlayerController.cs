using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed;
    public float groundDrag;
    public float jumpForce;
    public float jumpCooldown;
    public float airMultiplier;
    bool readyToJump;

    public int punchDamage = 10;

    // [Header("Keybinds")]
    // public KeyCode jumpKey = KeyCode.Space;
    // public KeyCode attackKey = KeyCode.Mouse0;
    // public KeyCode blockKey = KeyCode.Mouse1;
    // public KeyCode runKey = KeyCode.LeftShift;

    // Name of the input axis/button for attack
    public string attackInput = "Attack";
    public string blockInput = "Block";
    public string runInput = "Run";
    public string jumpInput = "Jump";

    [Header("Ground Check")]
    public float playerHeight;
    public LayerMask whatIsGround;
    bool grounded;

    public Transform orientation;

    Rigidbody rb;
    Animator animator;

    Vector3 moveDirection; // Declare moveDirection as a member variable

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        rb.freezeRotation = true;

        readyToJump = true;
    }

    private void Update()
    {
        // Ground check
        grounded = Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f + 0.3f, whatIsGround);

        // Movement input
        float horizontalInput = Input.GetAxisRaw("Horizontal");
        float verticalInput = Input.GetAxisRaw("Vertical");
        moveDirection = Vector3.ClampMagnitude(orientation.forward * verticalInput + orientation.right * horizontalInput, 1f);

        // Speed control
        bool isRunning = Input.GetButton(runInput);
        SpeedControl(isRunning);

        // Jump input
        if (Input.GetButtonDown(jumpInput) && readyToJump && grounded)
        {
            readyToJump = false;
            Jump();
            Invoke(nameof(ResetJump), jumpCooldown);
        }

        // Attack input
        if (Input.GetButtonDown(attackInput))
        {
            Attack();
        }

        // Block input
        if (Input.GetButtonDown(blockInput))
        {
            SetBlocking();
        }

        // Animations
        float speedMultiplier = isRunning ? 0.6f : 0.4f;
        animator.SetFloat("Speed", moveDirection.magnitude * speedMultiplier);
    }

    private void FixedUpdate()
    {
        MovePlayer();
    }

    private void MovePlayer()
    {
        // Get the forward and right vectors of the camera
        Vector3 cameraForward = Camera.main.transform.forward;
        Vector3 cameraRight = Camera.main.transform.right;

        // Project the camera vectors onto the horizontal plane (remove the vertical component)
        cameraForward.y = 0f;
        cameraRight.y = 0f;
        cameraForward.Normalize();
        cameraRight.Normalize();

        // Calculate movement direction based on camera orientation
        Vector3 moveInput = new Vector3(Input.GetAxisRaw("Horizontal"), 0f, Input.GetAxisRaw("Vertical"));
        moveDirection = cameraForward * moveInput.z + cameraRight * moveInput.x;

        // Apply movement force
        rb.AddForce(moveDirection.normalized * moveSpeed * 10f, ForceMode.Force);
    }

    private void SpeedControl(bool running)
    {
        float multiplier = running ? 2f : 1f;
        Vector3 flatVelocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);
        float maxSpeed = moveSpeed * multiplier;

        if (flatVelocity.magnitude > maxSpeed)
        {
            rb.velocity = flatVelocity.normalized * maxSpeed + Vector3.up * rb.velocity.y;
        }
    }

    private void Jump()
    {
        rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);
        rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
    }

    private void ResetJump()
    {
        readyToJump = true;
    }

    private void Attack()
    {
        animator.SetTrigger("Punching");
    }

    [ContextMenu("Capoeira")]
    private void Capoeira()
    {
        animator.SetTrigger("Capoeira");
    }


    private void SetBlocking()
    {
        animator.SetTrigger("Blocking");
    }

    // private void OnTriggerEnter(Collider other)
    // {
    //     Debug.Log("Collider Called: " + other.tag);

    //     if (other.CompareTag("Enemy"))
    //     {
    //         Debug.Log("WE ARE HERE");
    //         EnemyHealth enemyHealth = other.GetComponent<EnemyHealth>();

    //         Debug.Log("enemyHealth:" + enemyHealth);

    //         if (enemyHealth != null)
    //         {
    //             Debug.Log("Enemy Taking Damage");
    //             enemyHealth.TakeDamage(punchDamage);
    //         }
    //     }
    // }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("This Collider Called: " + this.tag);
        Debug.Log("Collider Called: " + other.tag);

        if (other.CompareTag("Enemy"))
        {
            Debug.Log("WE ARE HERE");
            
            // Access EnemyHealth component from the root GameObject
            EnemyHealth enemyHealth = other.transform.root.GetComponent<EnemyHealth>();

            Debug.Log("enemyHealth:" + enemyHealth);

            if (enemyHealth != null)
            {
                Debug.Log("Enemy Taking Damage");
                enemyHealth.TakeDamage(punchDamage);
            }
        }
    }
}
