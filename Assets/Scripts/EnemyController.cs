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
    public Color originalColor;
    public Color hitColor;
    private GameObject player;


    void Start()
    {
        rb = GetComponent<Rigidbody>();
        player = GameObject.FindGameObjectWithTag("Player");
        originalColor = GetComponent<SpriteRenderer>().color;
    }

    void Update()
    {
        if (health <= 0)
        {
            Instantiate(xpOrbPrefab, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
        MoveTowardsPlayer();
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Projectile"))
        {
            StartCoroutine(FlashWhite());
            health -= 1;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("KillPlane"))
        {
            Debug.Log("Enemy fell down hole and died");
            health = 0;
        }

        if (other.CompareTag("Explosion"))
        {
            Debug.Log("Enemy caught in barrel explosion!");
            health -= 8;
        }
    }

    public void Kicked(float dam, Vector3 direction, float forceOfKick)
    {
        Debug.Log("Ouch you kicked me!!");
        // StartCoroutine(FlashWhite());
        health -= (int)dam;

        if (rb != null)
        {
            float scaledPower = movementSpeed - Mathf.Log10(movementSpeed);

            // scale kick based off force and enemy movement speed
            rb.AddForce(direction * scaledPower * forceOfKick, ForceMode.Impulse);
        }

    }

    void MoveTowardsPlayer()
    {
        if (player != null)
        {
            Vector3 directionToPlayer = player.transform.position - transform.position;

            directionToPlayer.Normalize();

            rb.MovePosition(rb.position + directionToPlayer * movementSpeed * Time.deltaTime);

            transform.LookAt(player.transform);
        }
    }

    IEnumerator FlashWhite()
    {
        GetComponent<SpriteRenderer>().color = hitColor;

        yield return new WaitForSeconds(0.4f);

        GetComponent<SpriteRenderer>().color = originalColor;
    }
}