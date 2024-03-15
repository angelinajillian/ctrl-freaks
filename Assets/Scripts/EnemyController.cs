using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StarterAssets;

public class EnemyController : MonoBehaviour
{
    private Rigidbody rb;
    [SerializeField] private int health = 3;
    [SerializeField] private float movementSpeed = 2.0f;
    [SerializeField] private GameObject xpOrbPrefab;
    [SerializeField] private int damageStat = 1;
    public Color originalColor;
    public Color hitColor;
    private GameObject player;

    private Animator animator;
    private bool isStunned = false; // flag for if enemy is stunned or not
    private bool isPunched = false; // flag for if enemy was punched

    //  private PlayerControllerExtended playerController;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        player = GameObject.FindGameObjectWithTag("Player");
        // playerController = player.GetComponent<PlayerControllerExtended>();
        originalColor = GetComponent<SpriteRenderer>().color;
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        // Stunned! prohibit any other actions!
        // prohibits movement only, need to change how enemy attacks to stop that
        if (isStunned) return;

        if (health <= 0)
        {
            StartCoroutine(Die());
        }

        MoveTowardsPlayer();
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Projectile"))
        {
            StartCoroutine(FlashWhite());
            health -= 2;
        }
        else if (collision.gameObject.CompareTag("AOEProjectile"))
        {
            StartCoroutine(FlashWhite());
            health -= 6;
            rb.AddForce(Vector3.up * 12.5f, ForceMode.Impulse); // Adjust the force as needed
        }
        else if (collision.gameObject.CompareTag("Wall") & isPunched)
        {
            Debug.Log("I hit the wall I'm stunned!");
            StartCoroutine(this.Stunned(2.5f));
        }

    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("KillPlane"))
        {
            Debug.Log("Enemy fell down hole and died");
            StartCoroutine(FlashWhite());
            health = 0;
        }

        if (other.CompareTag("Explosion"))
        {
            Debug.Log("Enemy caught in barrel explosion!");
            StartCoroutine(FlashWhite());
            health -= 8;
        }
    }

    public void Kicked(float dam, Vector3 direction, float forceOfKick)
    {
        Debug.Log("Ouch you kicked me!!");
        
        StartCoroutine(PunchedCo());
        health -= (int)dam;

        if (rb != null)
        {
            float scaledPower = movementSpeed - Mathf.Log10(movementSpeed) + 5;

            // scale kick based off force and enemy movement speed
            rb.AddForce(direction * scaledPower * forceOfKick, ForceMode.Impulse);
        }

    }

    IEnumerator Die()
    {
        isStunned = true; // confusing code but it won't cause stun animation, only stops enemy movement 
        animator.SetTrigger("die");

        yield return new WaitForSeconds(1.5f);

        Instantiate(xpOrbPrefab, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }

    IEnumerator Stunned(float duration)
    {
        isStunned = true;
        animator.SetBool("stunned", true);

        transform.position = transform.position; // stop movement?
        yield return new WaitForSeconds(2.5f);

        animator.SetBool("stunned", false);
        isStunned = false;
    }

    IEnumerator PunchedCo()
    {
        isPunched = true;
        
        // Stays "Punched" for 5s
        yield return new WaitForSeconds(5f);

        isPunched = false;
    }

    void MoveTowardsPlayer()
    {
        if (player != null)
        {
            // animator.SetBool("moving", true);
            // Vector3 directionToPlayer = player.transform.position - transform.position;
            // directionToPlayer.Normalize();
            // rb.MovePosition(rb.position + directionToPlayer * movementSpeed * Time.deltaTime);
            // transform.LookAt(player.transform);

            animator.SetBool("moving", true);

            Vector3 directionToPlayer = player.transform.position - transform.position;
            float distanceToPlayer = directionToPlayer.magnitude;

            if (distanceToPlayer <= 2.0f)
            {
                rb.velocity = Vector3.zero;
                // animator.SetBool("moving", false);
                transform.LookAt(player.transform);
                AttackIfInRange(3f);
                return;
            }

            directionToPlayer.Normalize();

            rb.MovePosition(rb.position + directionToPlayer * movementSpeed * Time.deltaTime);

            transform.LookAt(player.transform);
        }
        else
        {
            animator.SetBool("moving", false);
        }
    }

    public void AttackIfInRange(float attackRange)
    {
        Vector3 directionToPlayer = player.transform.position - transform.position;
        
        RaycastHit[] hits = Physics.RaycastAll(transform.position, directionToPlayer, attackRange);

        // RaycastHit hit;

        // if (Physics.Raycast(transform.position, directionToPlayer, out hit, attackRange))
        // {
        //     Debug.Log("Raycast hit: " + hit.collider.gameObject.name);
        //     PlayerControllerExtended playerController = hit.collider.gameObject.GetComponent<PlayerControllerExtended>();
            
        //     if (playerController != null)
        //     {
        //         Debug.Log("PlayerController found.");
        //         playerController.TakeDamage(damageStat);
        //     }
        // }

        foreach (RaycastHit hit in hits)
        {   
            PlayerControllerExtended playerController = hit.collider.GetComponent<PlayerControllerExtended>();
            
            if (playerController != null)
            {
                playerController.TakeDamage(damageStat);
                Debug.Log("Player Attacked");
            }
        }

            // Debug.Log("Attacking Player");
            // if (hit.collider.gameObject.CompareTag("Player"))
            // {
            //     playerController.TakeDamage(damageStat);
            //     Debug.Log("Player within attack range! Perform attack actions...");
            // }
        // }
    }


    IEnumerator FlashWhite()
    {
        GetComponent<SpriteRenderer>().color = hitColor;

        yield return new WaitForSeconds(0.4f);

        GetComponent<SpriteRenderer>().color = originalColor;
    }
}