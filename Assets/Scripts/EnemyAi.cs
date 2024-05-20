
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;

public class EnemyAi : MonoBehaviour
{
    public NavMeshAgent agent;

    public Transform player;

    Animator animator;

    public int punchDamage = 10;

    public LayerMask whatIsGround, whatIsPlayer;

    public float health;

    //Patroling
    public Vector3 walkPoint;
    bool walkPointSet;
    public float walkPointRange;

    //Attacking
    public float timeBetweenAttacks;
    bool alreadyAttacked;
    //public GameObject projectile;

    //States
    public float sightRange, attackRange;
    public bool playerInSightRange, playerInAttackRange;

    public BoardSquareProperties.Colour currentSquareColour;

    private Slider healthSlider; // Reference to the player's transform

    private void Awake()
    {
        // player = GameObject.Find("PlayerObj").transform;
        agent = GetComponent<NavMeshAgent>();

        animator = GetComponent<Animator>();

        healthSlider = this.GetComponentInParent<Slider>();
    }

    private void Update()
    {
        //Check for sight and attack range
        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);

        if (!playerInSightRange && !playerInAttackRange) Patroling();
        if (playerInSightRange && !playerInAttackRange) ChasePlayer();
        if (playerInAttackRange && playerInSightRange) AttackPlayer();
    }

    private void Patroling()
    {
        if (!walkPointSet) {
            SearchWalkPoint();
        }

        if (walkPointSet) {
            agent.SetDestination(walkPoint);
        }

        Vector3 distanceToWalkPoint = transform.position - walkPoint;

        // Walkpoint reached
        if (distanceToWalkPoint.magnitude < 1f) {
            walkPointSet = false;
        }
    }

    private Vector3 GetPosition() {

        //update the position
        Vector3 position = new Vector3(transform.position.x, transform.position.y, transform.position.z);

        return position;
    }

    private void SearchWalkPoint()
    {
        //Calculate random point in range
        float randomZ = Random.Range(-walkPointRange, walkPointRange);
        float randomX = Random.Range(-walkPointRange, walkPointRange);

        Vector3 position = GetPosition();

        walkPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);

        if (Physics.Raycast(walkPoint, -transform.up, 2f, whatIsGround)) {
            walkPointSet = true;
        }
    }

    private void ChasePlayer()
    {
        Debug.Log("Error NAV Mesh : " + agent.isActiveAndEnabled );

        agent.SetDestination(player.position);
        // Debug.Log("Chasing Player");
        animator.SetTrigger("Chase");
    }

    private void AttackPlayer()
    {
        //Make sure enemy doesn't move
        //agent.SetDestination(transform.position);

        transform.LookAt(player);

        Debug.Log("Setting Attack Trigger:" + animator);
        animator.SetTrigger("Attack");

        if (!alreadyAttacked)
        {
            ///Attack code here
            // Rigidbody rb = Instantiate(projectile, transform.position, Quaternion.identity).GetComponent<Rigidbody>();
            // rb.AddForce(transform.forward * 32f, ForceMode.Impulse);
            // rb.AddForce(transform.up * 8f, ForceMode.Impulse);
            ///End of attack code

            alreadyAttacked = true;
            Invoke(nameof(ResetAttack), timeBetweenAttacks);
        }
    }

    private void ResetAttack()
    {
        alreadyAttacked = false;
    }

    private void DestroyEnemy()
    {
        Destroy(gameObject);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, sightRange);
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("EnemyPawnAi OnTriggerEnter this" + this +  " other: " + other + " this.currentSquareColour:" + this.currentSquareColour);   

        if (other.CompareTag("PlayerDamageColliderTag"))
        {
            /*
            // Access EnemyHealth component from the root GameObject
            PlayerHealth playerHealth = other.transform.root.GetComponent<PlayerHealth>();

            // Debug.Log("playerHealth:" + playerHealth);

            if (playerHealth != null)
            {
                Debug.Log("Player Taking Damage punchDamage: " + punchDamage);
                playerHealth.TakeDamage(punchDamage);
            }
            */

            // Access EnemyHealth component from the root GameObject
            // EnemyHealth enemyHealth = other.transform.root.GetComponent<EnemyHealth>();
            // EnemyPawnAi enemy = other.GetComponentInParent<EnemyPawnAi>();
            EnemyHealth enemyHealth = this.GetComponent<EnemyHealth>();
            // enemyHealth.healthSlider = this.healthSlider;

            Debug.Log("EnemyPornAI OnTriggerEnter: " + this + " this.currentSquareColour: " + this.currentSquareColour);

            // if (enemyHealth != null && this.currentSquareColour == BoardSquareProperties.Colour.White)
            if (enemyHealth != null)
            {
                // enemyHealth.healthSlider = 
                Debug.Log("Enemy Taking Damage " + "punchDamage: " + punchDamage);
                enemyHealth.TakeDamage(punchDamage);
            }
        }
    }
}
