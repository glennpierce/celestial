using UnityEngine;
using System.Collections;

public static class TransformExtensions
{
    // This method extends Transform and allows you to call it on any Transform object.
    public static GameObject FindWithTagInParents(this Transform childTransform, string tag)
    {
        Transform current = childTransform;

        // Loop until you either find a tagged parent or there are no more parents.
        while (current != null)
        {
            // Debug.LogError("current: " + current + "  tag:" + tag);

            // Check if the current GameObject has the specified tag.
            if (current.CompareTag(tag))
            {
                return current.gameObject;
            }

            // Move to the next parent in the hierarchy.
            current = current.parent;
        }

        // Return null if no GameObject with the specified tag was found.
        return null;
    }
}


public class PlayerController : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed;
    public float groundDrag;
    public float jumpForce;
    public float jumpCooldown;
    public float airMultiplier;
    bool readyToJump;

    public int punchDamage = 1;

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
    PlayerHealth playerHealth;

    Vector3 moveDirection; // Declare moveDirection as a member variable

    bool dead = false;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        rb.freezeRotation = true;

        readyToJump = true;

        playerHealth = this.GetComponent<PlayerHealth>();

        // Debug.Log("playerHealth: " + playerHealth);
        // Subscribe to the OnPlayerDeath event
        playerHealth.OnPlayerDeath += HandlePlayerDeath;
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
        // if (Input.GetButtonDown(blockInput))
        // {
        //     SetBlocking();
        // }

        // Animations
        float speedMultiplier = isRunning ? 0.6f : 0.4f;
        animator.SetFloat("Speed", moveDirection.magnitude * speedMultiplier);
    }

    private void FixedUpdate()
    {
        MovePlayer();
    }

    private void HandlePlayerDeath()
    {
        // Handle player death here (e.g., game over logic)
        Debug.Log("Player has died!");

        animator.SetTrigger("Death");

        this.dead = true;
        //animator.enabled = false;
    }

    public void DisableAnimator()
    {
        animator.enabled = false;
    }

    private void MovePlayer()
    {
        if (dead) {
            return;
        }

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
        if (dead) {
            return;
        }

        rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);
        rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
         animator.SetTrigger("Jump");
    }

    private void ResetJump()
    {
        readyToJump = true;
    }

    private void Attack()
    {
        if (dead) {
            return;
        }

        // animator.SetTrigger("LeftPunch");

        // Generate a random number (0 or 1) to determine left or right punch
        int randomIndex = Random.Range(0, 2);

        // Trigger the corresponding animation based on the random number
        if (randomIndex == 0)
        {
            //Debug.Log("Left Punch");
            //Debug.LogError("animator.SetTrigger Left Punch");
            animator.SetTrigger("LeftPunch");
        }
        else
        {
            //Debug.Log("Right Punch");
            //Debug.LogError("animator.SetTrigger Right Punch");
            animator.SetTrigger("RightPunch");
        }

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

    private void OnTriggerEnter(Collider other)
    {
        /*
        // Debug.Log("This Collider Called: " + this.tag);
        Debug.Log("Player Collider Called: " + other.tag);

        if (other.CompareTag("EnemyPawn"))
        {
            // Access EnemyHealth component from the root GameObject
            // EnemyHealth enemyHealth = other.transform.root.GetComponent<EnemyHealth>();
            EnemyPawnAi enemy = other.GetComponentInParent<EnemyPawnAi>();
            EnemyHealth enemyHealth = other.GetComponentInParent<EnemyHealth>();

            Debug.Log("other: " + other + " enemyHealth HERE :" + enemyHealth + " enemy.currentSquareColour: " + enemy.currentSquareColour);

            if (enemyHealth != null && enemy.currentSquareColour == BoardSquareProperties.Colour.White)
            {
                // enemyHealth.healthSlider = 
                Debug.Log("Enemy Taking Damage " + "punchDamage: " + punchDamage);
                enemyHealth.TakeDamage(punchDamage);
            }
        }

        */


        // Debug.Log("Player OnTrigger other: " + other);

        if (other.CompareTag("EnemyGivingDamageArea"))
        {
            // PlayerHealth playerHealth = this.GetComponent<PlayerHealth>();

            // Debug.Log("playerHealth:" + playerHealth);

            if (playerHealth != null)
            {
                playerHealth.TakeDamage(punchDamage);

                // // if enenenemy.currentSquareColour == BoardSquareProperties.Colour.White)
                // GameObject pawnEnemy = other.transform.FindWithTagInParents("EnemyPawn");
                // if (pawnEnemy != null)
                // {
                //     EnemyAi enemy = pawnEnemy.GetComponentInParent<EnemyAi>();
                //     if (enemy != null)
                //     {
                //         Debug.Log("SQUARE: " + enemy.currentSquareColour);
                //     }

                //      // Debug.LogError("pawnEnemy JJJJJJJJJJJJJJJJJJJJJJJJJ");
                //      //if (pawnEnemy)
                // }
                // else {
                //     //Debug.Log("Player Taking Damage punchDamage: " + punchDamage);
                //     playerHealth.TakeDamage(punchDamage);
                // }   
            }
        }

        // if (other.CompareTag("EnemyPawnHead"))
        // {
        //     // PlayerHealth playerHealth = this.GetComponent<PlayerHealth>();

        //     // Debug.Log("playerHealth:" + playerHealth);

        //     if (playerHealth != null)
        //     {
        //         //Debug.Log("Player Taking Damage punchDamage: " + punchDamage);
        //         playerHealth.TakeDamage(punchDamage);
        //     }
        // }

        // if (other.CompareTag("NM1"))
        // {
        //     // PlayerHealth playerHealth = this.GetComponent<PlayerHealth>();

        //     // Debug.Log("playerHealth:" + playerHealth);

        //     if (playerHealth != null)
        //     {
        //         //Debug.Log("Player Taking Damage punchDamage: " + punchDamage);
        //         playerHealth.TakeDamage(punchDamage);
        //     }
        // }

        // if (other.CompareTag("Boss"))
        // {
        //     // PlayerHealth playerHealth = this.GetComponent<PlayerHealth>();

        //     // Debug.Log("playerHealth:" + playerHealth);

        //     if (playerHealth != null)
        //     {
        //         //Debug.Log("Player Taking Damage punchDamage: " + punchDamage);
        //         playerHealth.TakeDamage(punchDamage);
        //     }
        // }


    }
}
